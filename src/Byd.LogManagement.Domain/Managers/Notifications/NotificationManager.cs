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
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace Byd.LogManagement.Managers.Notifications;

/// <summary>
/// Bildirimin satirlara yazilmasini ve gonderim damgasini yonetir.
/// </summary>
// islevi: Olaydaki kanal kodlarini lookup kimligine, mobil kanal icin Firebase proje kodunu proje kimligine cevirir; her
// kanal icin tenant bildirimine tek satir, digerine alici basina satir uretir, vadesi gelmis satirlari gonderildi olarak
// damgalayip kanal koduna gore gruplar ve istek dilinin hazir metnini secer.
// sistemdeki gorevi: Tuketiciyi lookup cozme, fan-out ve hangi satirin simdi gidecegi kararindan uzak tutan domain
// sahibidir.
public class NotificationManager(
    INotificationRepository repository,
    INotificationChannelRepository channelRepository,
    FirebaseProjectManager firebaseProjectManager,
    IDataFilter<IMultiTenant> multiTenantFilter) : BaseManager<Notification, Guid>(repository)
{
    // Olayi kanal ve alici basina satirlara cevirip ekler ve simdi gonderilecek satirlari kanal koduna gore dondurur.
    // Vadesi gelmis satir gonderildi olarak dogar; ileri tarihli satir gonderilmemis olarak saklanir.
    public virtual async Task<IReadOnlyDictionary<int, List<Notification>>> HandleRecordedAsync(
        NotificationRecordedEto eventData,
        CancellationToken cancellationToken = default)
    {
        var channels = await ResolveChannelIdsAsync(eventData.ChannelCodes, cancellationToken);
        var firebaseProjectId = await ResolveFirebaseProjectIdAsync(eventData, cancellationToken);
        var notifications = BuildNotifications(eventData, channels, firebaseProjectId);
        var sent = MarkDueAsSent(notifications);

        await Repository.InsertManyAsync(notifications, cancellationToken: cancellationToken);

        return GroupByChannelCode(sent, channels);
    }

    // Butun tenant'larin vadesi gelmis ama henuz gonderilmemis web ve mobil satirlarini damgalar ve kanal koduna gore
    // dondurur. Tarama host baglaminda calistigi icin tenant filtresi yalniz bu tarama boyunca kapatilir.
    // Damgali satir bir daha alinmaz; ayni ileri tarihli bildirim iki kez gonderilmez.
    public virtual async Task<IReadOnlyDictionary<int, List<Notification>>> MarkDueAsSentAsync(
        CancellationToken cancellationToken = default)
    {
        var channels = await ResolveChannelIdsAsync(
            [NotificationConsts.WebChannelCode, NotificationConsts.MobileChannelCode],
            cancellationToken);
        List<Guid> channelIds = [.. channels.Values];
        var dueTime = Clock.Now;
        using var allTenants = multiTenantFilter.Disable();
        var unsent = await TryGetListByAsync(
            notification => channelIds.Contains(notification.ChannelId)
                && notification.SentTime == null
                && notification.ScheduledTime <= dueTime,
            cancellationToken: cancellationToken);
        var sent = MarkDueAsSent(unsent);

        await Repository.UpdateManyAsync(sent, cancellationToken: cancellationToken);

        return GroupByChannelCode(sent, channels);
    }

    // Verilen dilin hazir metnini secer; dil verilmezse istegin dili kullanilir. Once tam kultur adina, sonra iki
    // harfli dile bakilir; ikisi de yoksa bos doner. Mobil gonderim cihazin dilini, web gonderim istegin dilini verir.
    public virtual string TextOf(Notification notification, string? languageCode = null)
    {
        var language = languageCode.IsNullOrWhiteSpace() ? CultureInfo.CurrentUICulture.Name : languageCode;
        return notification.Data.GetValueOrDefault(language)
            ?? notification.Data.GetValueOrDefault(language.Split('-')[0])
            ?? string.Empty;
    }

    // Her kanal icin tenant bildirimine tek satir, digerine alici basina bir satir uretir; bos gelen alici listesi satir
    // uretmez, tenant'taki herkese gitmez. Firebase projesi yalniz mobil satira yazilir. Hepsi ayni grup kimligini tasir,
    // boylece grup guncelleme ve iptal icerik esitligine degil kimlige bakar.
    private List<Notification> BuildNotifications(
        NotificationRecordedEto eventData,
        Dictionary<int, Guid> channels,
        Guid? firebaseProjectId)
    {
        var notificationGroupId = GuidGenerator.Create();
        NotificationRecipientEto?[] recipients = eventData.IsTenantNotification
            ? [null]
            : [.. eventData.Recipients.DistinctBy(recipient => recipient.RecipientUserId)];
        return
        [
            .. from channel in channels
               from recipient in RecipientsOf(channel.Key, eventData, recipients)
               select eventData.ToNotification(
                   notificationGroupId,
                   channel.Value,
                   channel.Key == NotificationConsts.MobileChannelCode ? firebaseProjectId : null,
                   recipient)
        ];
    }

    // Mobil kanala topic verildiyse alici listesi yerine tek topic satiri uretilir; cihaz secimini FCM uyeligi yapar.
    private static NotificationRecipientEto?[] RecipientsOf(
        int channelCode,
        NotificationRecordedEto eventData,
        NotificationRecipientEto?[] recipients)
        => channelCode == NotificationConsts.MobileChannelCode && eventData.Topic is not null ? [null] : recipients;

    // Damgalanan satirlari kanal is koduna gore gruplar; satiri olmayan kanal sozlukte yer almaz.
    // Satirlarin kanal kimligi bu cagrida cozulen lookup'tan geldigi icin her kimligin kod karsiligi vardir.
    private static Dictionary<int, List<Notification>> GroupByChannelCode(
        List<Notification> notifications,
        Dictionary<int, Guid> channels)
    {
        var codesByChannelId = channels.ToDictionary(channel => channel.Value, channel => channel.Key);
        return notifications
            .GroupBy(notification => codesByChannelId[notification.ChannelId])
            .ToDictionary(group => group.Key, group => group.ToList());
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

    // Kanal is kodlarini tek sorguda lookup kimliklerine cevirir; kodlardan biri tabloda yoksa lokalize NotFound firlatir.
    private async Task<Dictionary<int, Guid>> ResolveChannelIdsAsync(
        IEnumerable<int> channelCodes,
        CancellationToken cancellationToken = default)
    {
        List<int?> codes = [.. channelCodes.Distinct().Select(code => (int?)code)];
        var channels = await channelRepository.GetListAsync(
            entity => codes.Contains(entity.Code),
            cancellationToken: cancellationToken);
        if (channels.Count != codes.Count)
            throw new BusinessException(GeneralExceptionCodes.EntityNotFound);
        return channels.ToDictionary(channel => channel.Code!.Value, channel => channel.Id);
    }

    // Mobil kanal istendiyse Firebase projesini olayin tenant'inda is koduyla cozer; mobil kanal yoksa proje aranmaz.
    // Olay isleyicisi host baglaminda calistigi icin arama olayin tenant'ina gecilerek yapilir.
    private async Task<Guid?> ResolveFirebaseProjectIdAsync(
        NotificationRecordedEto eventData,
        CancellationToken cancellationToken = default)
    {
        if (!eventData.ChannelCodes.Contains(NotificationConsts.MobileChannelCode))
            return null;
        if (string.IsNullOrWhiteSpace(eventData.FirebaseProjectKey))
            throw new BusinessException(GeneralExceptionCodes.RequiredValueMissing);

        using (CurrentTenant.Change(eventData.TenantId))
        {
            var project = await firebaseProjectManager.EnsureExistsByKeyAsync(
                eventData.FirebaseProjectKey,
                cancellationToken);
            return project.Id;
        }
    }
}
