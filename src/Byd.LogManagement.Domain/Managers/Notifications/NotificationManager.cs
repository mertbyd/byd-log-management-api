using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Managers;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Entities.Notifications;
using Byd.LogManagement.Etos.Notifications;
using Byd.LogManagement.Extensions.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Volo.Abp;

namespace Byd.LogManagement.Managers.Notifications;

/// <summary>
/// Bildirimin satirlara yazilmasini ve gonderim damgasini yonetir.
/// </summary>
// islevi: Olaydaki kanal kodunu lookup kimligine cevirir, tenant bildirimine tek satir, digerine alici basina satir
// uretir, vadesi gelmis web satirlarini gonderildi olarak damgalar ve istek dilinin hazir metnini secer.
// sistemdeki gorevi: Tuketiciyi lookup cozme, fan-out ve hangi satirin simdi gidecegi kararindan uzak tutan domain
// sahibidir.
public class NotificationManager(
    INotificationRepository repository,
    INotificationChannelRepository channelRepository) : BaseManager<Notification, Guid>(repository)
{
    // Olayi satirlara cevirip ekler ve simdi gonderilecek web satirlarini dondurur.
    // Vadesi gelmis web satiri gonderildi olarak dogar; ileri tarihli satir gonderilmemis olarak saklanir.
    public virtual async Task<List<Notification>> HandleRecordedAsync(
        NotificationRecordedEto eventData,
        CancellationToken cancellationToken = default)
    {
        var channelId = await ResolveChannelIdAsync(eventData.ChannelCode, cancellationToken);
        var notifications = BuildNotifications(eventData, channelId);
        var sent = eventData.ChannelCode == NotificationConsts.WebChannelCode ? MarkDueAsSent(notifications) : [];

        await Repository.InsertManyAsync(notifications, cancellationToken: cancellationToken);

        return sent;
    }

    // Istek dilinin hazir metnini secer; once tam kultur adina, sonra iki harfli dile bakar.
    public virtual string Describe(Notification notification)
    {
        var culture = CultureInfo.CurrentUICulture;
        return notification.Description.GetValueOrDefault(culture.Name)
            ?? notification.Description.GetValueOrDefault(culture.TwoLetterISOLanguageName)
            ?? string.Empty;
    }

    // Tenant bildirimine tek satir, digerine alici basina bir satir uretir; bos gelen alici listesi satir
    // uretmez, tenant'taki herkese gitmez. Hepsi ayni grup kimligini tasir, boylece grup guncelleme ve iptal
    // icerik esitligine degil kimlige bakar.
    private List<Notification> BuildNotifications(NotificationRecordedEto eventData, Guid channelId)
    {
        var notificationGroupId = GuidGenerator.Create();
        if (eventData.IsTenantNotification)
            return [eventData.ToNotification(notificationGroupId, channelId, recipient: null)];
        return
        [
            .. eventData.Recipients
                .DistinctBy(recipient => recipient.RecipientUserId)
                .Select(recipient => eventData.ToNotification(notificationGroupId, channelId, recipient))
        ];
    }

    // Vadesi gelmis satirlari ayni gonderim aniyla damgalar ve onlari dondurur.
    private List<Notification> MarkDueAsSent(List<Notification> notifications)
    {
        var sentTime = Clock.Now;
        var due = notifications.Where(notification => notification.ScheduledTime <= sentTime).ToList();
        foreach (var notification in due)
            notification.SentTime = sentTime;
        return due;
    }

    // Kanalin is kodunu lookup kimligine cevirir; kod tabloda yoksa lokalize NotFound firlatir.
    private async Task<Guid> ResolveChannelIdAsync(int channelCode, CancellationToken cancellationToken = default)
    {
        var channel = await channelRepository.FindAsync(
            entity => entity.Code == channelCode,
            cancellationToken: cancellationToken);
        return channel?.Id ?? throw new BusinessException(GeneralExceptionCodes.EntityNotFound);
    }
}
