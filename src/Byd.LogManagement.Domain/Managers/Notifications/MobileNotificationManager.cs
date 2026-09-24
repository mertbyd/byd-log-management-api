using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Managers;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Entities.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Byd.LogManagement.Models.Notifications;

namespace Byd.LogManagement.Managers.Notifications;

/// <summary>
/// Mobil gonderim planini, sonuc kaydini ve gecersiz cihaz temizligini yonetir.
/// </summary>
// islevi: Token sorgu kapsamini ve token/topic mesajlarini belirler, metni cihazin diline gore secer, her hedefin
// sonucunu gunluge yazar ve FCM'in artik gecersiz dedigi token'lari cihaz defterinden siler.
// sistemdeki gorevi: Dispatcher'i hedef ve dil kararindan, mobil gondericiyi sonuc kaydi ve token temizligi kuralindan
// uzak tutan domain sahibidir; basarisiz gonderim tekrar denenmez, sonucu burada kalir.
public class MobileNotificationManager(
    IMobileNotificationRepository repository,
    UserMobileTokenManager userMobileTokenManager,
    NotificationManager notificationManager) : BaseManager<MobileNotification, Guid>(repository)
{
    // Bir proje grubunda sorgulanacak alicilari belirler; tenant bildirimi varsa tum aktif token'lar gerekir.
    public virtual IReadOnlyCollection<Guid>? GetRecipientIds(IReadOnlyCollection<Notification> notifications)
    {
        ArgumentNullException.ThrowIfNull(notifications);
        return notifications.Any(notification => notification.Topic is null && notification.IsTenantNotification)
            ? null
            : [.. notifications
                .Where(notification => notification.Topic is null)
                .Select(notification => notification.RecipientUserId!.Value)
                .Distinct()];
    }

    // Bildirim satirini gonderilecek mobil mesajlara donusturur. Topic satiri tek mesaj olur; cihaz satirinda hedefler
    // cihazin diline gore gruplanir ve her dil kendi metniyle ayri mesaj olarak gider.
    public virtual IReadOnlyCollection<MobileNotificationMessageModel> CreateMessages(
        Notification notification,
        IReadOnlyDictionary<Guid, UserMobileToken> devicesByUserId)
    {
        ArgumentNullException.ThrowIfNull(notification);
        ArgumentNullException.ThrowIfNull(devicesByUserId);

        // Topic uyeleri onceden bilinmedigi icin sorulacak cihaz yoktur; metin istegin diliyle secilir.
        if (notification.Topic is not null)
            return [CreateMessage(notification, languageCode: null, isTopic: true, [notification.Topic])];

        return [.. DevicesOf(notification, devicesByUserId)
            .GroupBy(device => device.LanguageCode)
            .Select(devices => CreateMessage(
                notification,
                devices.Key,
                isTopic: false,
                [.. devices.Select(device => device.Token)]))];
    }

    // Proje grubundaki tum gonderim sonuclarini tek bulk yazida gunluge ekler ve gecersiz token'lari toplu siler.
    public virtual async Task RecordManyAsync(
        Guid firebaseProjectId,
        IReadOnlyCollection<MobileNotificationDispatchResultModel> deliveries,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(deliveries);
        var logs = deliveries.SelectMany(delivery => delivery.Results.Select(result => new MobileNotification(GuidGenerator.Create())
        {
            NotificationId = delivery.Notification.Id,
            Target = result.Target,
            IsSuccess = result.IsSuccess,
            StatusCode = result.StatusCode?.Truncate(NotificationConsts.MaxStatusCodeLength),
            ErrorMessage = result.ErrorMessage?.Truncate(NotificationConsts.MaxErrorMessageLength),
            ExecutionDuration = result.ExecutionDuration
        }));
        await Repository.InsertManyAsync(logs, cancellationToken: cancellationToken);

        var invalidTokens = deliveries
            .SelectMany(delivery => delivery.Results)
            .Where(result => result.IsTargetInvalid)
            .Select(result => result.Target);
        await userMobileTokenManager.RemoveByTokensAsync(
            firebaseProjectId,
            invalidTokens,
            cancellationToken);
    }

    // Projenin secret'i yoksa her hedef icin ayni basarisiz sonucu kurar.
    public virtual IReadOnlyCollection<MobileDeliveryResultModel> CreateMissingSecretResults(
        IEnumerable<string> targets)
        => [.. targets.Select(target => new MobileDeliveryResultModel
        {
            Target = target,
            ErrorMessage = NotificationConsts.MissingSecretStatusMessage
        })];

    // Hedefleri verilen dilin metniyle tek mobil send modeline kurar.
    private MobileNotificationMessageModel CreateMessage(
        Notification notification,
        string? languageCode,
        bool isTopic,
        IReadOnlyCollection<string> targets)
        => new()
        {
            Title = notification.Title,
            Body = notificationManager.TextOf(notification, languageCode),
            IsTopic = isTopic,
            Targets = targets
        };

    // Tenant bildirimini projedeki tum cihazlara, kullanici bildirimini yalniz alicinin cihazina yoneltir.
    private static IEnumerable<UserMobileToken> DevicesOf(
        Notification notification,
        IReadOnlyDictionary<Guid, UserMobileToken> devicesByUserId)
    {
        if (notification.IsTenantNotification)
            return devicesByUserId.Values;
        return devicesByUserId.TryGetValue(notification.RecipientUserId!.Value, out var device)
            ? [device]
            : [];
    }
}
