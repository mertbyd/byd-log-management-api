using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Byd.FileManagement.Services.StoredFiles;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Entities.Notifications;
using Byd.LogManagement.Extensions.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Byd.LogManagement.Managers.Notifications;
using Byd.LogManagement.Models.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
// Bildirim satiri ile FCM'in bildirim govdesi ayni ada sahiptir; ikisi de bu sinifta gerektigi icin adlandirilir.
using Notification = Byd.LogManagement.Entities.Notifications.Notification;
using FirebaseNotification = FirebaseAdmin.Messaging.Notification;

namespace Byd.LogManagement.Senders.Notifications.Firebase;

/// <summary>
/// Mobil kanalin satirlarini Firebase uzerinden iletir.
/// </summary>
// islevi: Satirlari tenant ve Firebase projesine gore gruplar; grubun secret'ini ve cihaz token'larini bir kez okur,
// mesaji token veya topic hedefiyle FCM'e gonderir ve her hedefin sonucunu tek calisma biriminde gunluge yazar.
// sistemdeki gorevi: Mobil kanalin gonderim sozlesmesini uygulayan ve FCM'e dokunan tek siniftir; FCM tipleri buradan
// disari cikmaz, hedef ve kayit kararlari Manager'lara birakilir.
[ExposeServices(typeof(INotificationSender))]
public class FirebaseNotificationMobileSender(
    UserMobileTokenManager userMobileTokenManager,
    MobileNotificationManager mobileNotificationManager,
    FirebaseProjectManager firebaseProjectManager,
    IStoredFileAppService storedFileAppService,
    ICurrentTenant currentTenant,
    IUnitOfWorkManager unitOfWorkManager)
    : INotificationSender, ITransientDependency
{
    private static readonly Lock AppLock = new();

    // Gondericinin hizmet ettigi mobil kanalin is kodu.
    public virtual int ChannelCode => NotificationConsts.MobileChannelCode;

    // Satirlari tenant ve Firebase projesine gore gruplayip her grubu kendi baglaminda gonderir.
    public virtual Task SendAsync(
        IReadOnlyCollection<Notification> notifications,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(notifications);
        return Task.WhenAll(notifications
            .GroupBy(notification => (notification.TenantId, FirebaseProjectId: notification.FirebaseProjectId!.Value))
            .Select(group => SendProjectAsync(
                group.Key.TenantId,
                group.Key.FirebaseProjectId,
                [.. group],
                cancellationToken)));
    }

    // Projenin secret'ini, FCM baglantisini ve cihaz token'larini bir kez kurup sonuclari tek calisma biriminde yazar.
    // Baglanti gonderimlerden once kurulur: secret okunamiyorsa hic bildirim gitmeden, gunluksuz kalan gonderim olmadan durur.
    private async Task SendProjectAsync(
        Guid? tenantId,
        Guid firebaseProjectId,
        List<Notification> notifications,
        CancellationToken cancellationToken = default)
    {
        using var tenant = currentTenant.Change(tenantId);
        using var unitOfWork = unitOfWorkManager.Begin(requiresNew: true);
        var secret = await FindSecretAsync(firebaseProjectId, cancellationToken);
        var messaging = secret is null ? null : FirebaseMessaging.GetMessaging(GetOrCreateApp(secret));
        var devicesByUserId = await ResolveDevicesAsync(firebaseProjectId, notifications, cancellationToken);
        var deliveries = await Task.WhenAll(notifications.Select(notification =>
            SendNotificationAsync(notification, messaging, devicesByUserId, cancellationToken)));
        await mobileNotificationManager.RecordManyAsync(firebaseProjectId, deliveries, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    // Projenin secret kategorisindeki dosyalari okur; guncel olani Manager secer, hic secret yoksa bos doner.
    private async Task<FirebaseProjectSecretModel?> FindSecretAsync(
        Guid firebaseProjectId,
        CancellationToken cancellationToken = default)
    {
        var category = await firebaseProjectManager.GetSecretCategoryAsync(cancellationToken);
        var files = await storedFileAppService.GetListWithContentByExternalIdAndCategoryAsync(
            firebaseProjectId,
            category.Id);
        return firebaseProjectManager.FindLatestSecret(files.ToFirebaseProjectSecrets());
    }

    // Grubun ihtiyac duydugu aktif cihazlari kullanici kimligine gore indeksler; yalniz topic satiri varsa Manager bos
    // alici listesi verir ve cihaz hic sorgulanmaz. Cihazin dili metin seciminde kullanildigi icin kayit butunuyle tasinir.
    private async Task<IReadOnlyDictionary<Guid, UserMobileToken>> ResolveDevicesAsync(
        Guid firebaseProjectId,
        IReadOnlyCollection<Notification> notifications,
        CancellationToken cancellationToken = default)
    {
        var recipientIds = mobileNotificationManager.GetRecipientIds(notifications);
        if (recipientIds?.Count == 0)
            return new Dictionary<Guid, UserMobileToken>();

        var devices = await userMobileTokenManager.GetEnabledListAsync(
            firebaseProjectId,
            recipientIds,
            cancellationToken);
        return devices.ToDictionary(device => device.UserId);
    }

    // Satirin dil basina mesajlarini kurup baglanti varsa gonderir, secret yoksa hedef bazli basarisiz sonuc uretir.
    private async Task<MobileNotificationDispatchResultModel> SendNotificationAsync(
        Notification notification,
        FirebaseMessaging? messaging,
        IReadOnlyDictionary<Guid, UserMobileToken> devicesByUserId,
        CancellationToken cancellationToken = default)
    {
        List<MobileDeliveryResultModel> results = [];
        foreach (var message in mobileNotificationManager.CreateMessages(notification, devicesByUserId))
            results.AddRange(messaging is null
                ? mobileNotificationManager.CreateMissingSecretResults(message.Targets)
                : await SendToFirebaseAsync(messaging, message, cancellationToken));
        return new MobileNotificationDispatchResultModel
        {
            Notification = notification,
            Results = results
        };
    }

    // Topic mesajini tek cagriyla, token mesajini FCM'in kabul ettigi parca boyutunda gonderir.
    private static async Task<IReadOnlyCollection<MobileDeliveryResultModel>> SendToFirebaseAsync(
        FirebaseMessaging messaging,
        MobileNotificationMessageModel message,
        CancellationToken cancellationToken = default)
    {
        if (message.IsTopic)
            return [await SendTopicAsync(messaging, message, cancellationToken)];

        List<MobileDeliveryResultModel> results = [];
        foreach (var tokens in message.Targets.Chunk(NotificationConsts.MaxFirebaseTokensPerRequest))
            results.AddRange(await SendChunkAsync(messaging, message, tokens, cancellationToken));
        return results;
    }

    // Topic'e tek FCM cagrisi yapar; saglayici ya da kimlik dogrulama hatasini hedefin sonucuna cevirir.
    private static async Task<MobileDeliveryResultModel> SendTopicAsync(
        FirebaseMessaging messaging,
        MobileNotificationMessageModel input,
        CancellationToken cancellationToken = default)
    {
        var topic = input.Targets.Single();
        var message = new Message
        {
            Topic = topic,
            Notification = new FirebaseNotification { Title = input.Title, Body = input.Body }
        };
        var stopwatch = Stopwatch.StartNew();
        try
        {
            var messageId = await messaging.SendAsync(message, cancellationToken);
            return ResultOf(topic, stopwatch.Elapsed.TotalMilliseconds, messageId, failure: null);
        }
        catch (Exception exception) when (exception is FirebaseMessagingException or TokenResponseException)
        {
            return ResultOf(topic, stopwatch.Elapsed.TotalMilliseconds, messageId: null, exception);
        }
    }

    // Parcayi tek FCM istegiyle gonderip token bazli cevaplari; istek tumden reddedilirse parcanin tamamini sonuca cevirir.
    private static async Task<List<MobileDeliveryResultModel>> SendChunkAsync(
        FirebaseMessaging messaging,
        MobileNotificationMessageModel input,
        string[] tokens,
        CancellationToken cancellationToken = default)
    {
        // Kayit token hedeflemesi bilincli tercihtir: FCM'in topic aboneligi yalniz kayit token'i kabul ettigi icin
        // cihazlar token'la kaydedilir, gonderim de ayni kimlikle yapilir. FID'e gecis topic API'si FID destekleyince.
#pragma warning disable CS0618
        var message = new MulticastMessage
        {
            Tokens = tokens,
            Notification = new FirebaseNotification { Title = input.Title, Body = input.Body }
        };
#pragma warning restore CS0618
        var stopwatch = Stopwatch.StartNew();
        try
        {
            var response = await messaging.SendEachForMulticastAsync(message, cancellationToken);
            var duration = stopwatch.Elapsed.TotalMilliseconds;
            return [.. tokens.Zip(
                response.Responses,
                (token, result) => ResultOf(token, duration, result.MessageId, result.Exception))];
        }
        catch (Exception exception) when (exception is FirebaseMessagingException or TokenResponseException)
        {
            var duration = stopwatch.Elapsed.TotalMilliseconds;
            return [.. tokens.Select(token => ResultOf(token, duration, messageId: null, exception))];
        }
    }

    // Secret dosyasinin kimligiyle kayitli uygulamayi dondurur; yoksa secret iceriginden bir kez olusturur.
    // Secret yenilendiginde dosya kimligi degistigi icin yeni uygulama kurulur, eski credential kullanilmaz.
    private static FirebaseApp GetOrCreateApp(FirebaseProjectSecretModel secret)
    {
        var name = secret.Id.ToString("N");
        lock (AppLock)
        {
            return FirebaseApp.GetInstance(name) ?? FirebaseApp.Create(
                new AppOptions
                {
                    Credential = CredentialFactory
                        .FromJson<ServiceAccountCredential>(Encoding.UTF8.GetString(secret.Data))
                        .ToGoogleCredential()
                },
                name);
        }
    }

    // Hedefin cevabini tek noktada sonuca cevirir: basaride mesaj kimligi, hatada kod ve tek satirlik saglayici mesaji
    // gunluge yazilir; FCM'in artik tanimadigi hedef cihaz defterinden silinmek uzere isaretlenir.
    private static MobileDeliveryResultModel ResultOf(
        string target,
        double duration,
        string? messageId,
        Exception? failure)
        => new()
        {
            Target = target,
            IsSuccess = failure is null,
            StatusCode = failure is null ? null : CodeOf(failure),
            StatusMessage = failure is null
                ? messageId
                : failure.Message.Split('\n')[0].Trim(),
            ExecutionDuration = duration,
            IsTargetInvalid = (failure as FirebaseMessagingException)?.MessagingErrorCode
                is MessagingErrorCode.Unregistered or MessagingErrorCode.InvalidArgument
        };

    // FCM reddinin kodunu, kimlik dogrulama hatasini ise sabit kodu verir.
    private static string CodeOf(Exception failure)
        => failure is FirebaseMessagingException messagingFailure
            ? messagingFailure.MessagingErrorCode?.ToString() ?? messagingFailure.ErrorCode.ToString()
            : NotificationConsts.InvalidCredentialStatusCode;
}
