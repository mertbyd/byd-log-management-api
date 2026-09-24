using System;
using Byd.LogManagement.Entities.Notifications;
using Byd.LogManagement.Etos.Notifications;

namespace Byd.LogManagement.Extensions.Notifications;

/// <summary>
/// Bildirim olayini kuyruk satirlarina donusturur.
/// </summary>
// islevi: NotificationRecordedEto alanlarini yeni bir Notification satirina elle tasir.
// sistemdeki gorevi: Olaydan satir kurmanin tek yeridir; lookup kodunu kimlige ceviren ve grup kimligini
// ureten Manager'dir, bu yuzden cozulmus degerler disaridan verilir.
// Gonderim ve okunma alanlari doldurulmaz: satir her zaman kuyrukta ve okunmamis dogar.
public static class NotificationRecordedEtoExtensions
{
    // Olayin icerik alanlarini tek bir alici icin yeni satira tasir; tenant bildiriminde alici verilmez.
    public static Notification ToNotification(
        this NotificationRecordedEto source,
        Guid notificationGroupId,
        Guid channelId,
        NotificationRecipientEto? recipient)
    {
        return new Notification
        {
            TenantId = source.TenantId,
            OrganizationId = source.OrganizationId,
            NotificationGroupId = notificationGroupId,
            ChannelId = channelId,
            SenderUserId = source.SenderUserId,
            RecipientUserId = recipient?.RecipientUserId,
            ExternalId = source.ExternalId,
            TenantName = source.TenantName,
            OrganizationName = source.OrganizationName,
            SenderUserName = source.SenderUserName,
            RecipientUserName = recipient?.RecipientUserName,
            ModuleName = source.ModuleName,
            Title = source.Title,
            IsTenantNotification = source.IsTenantNotification,
            ScheduledTime = source.ScheduledTime,
            Description = source.Description
        };
    }
}
