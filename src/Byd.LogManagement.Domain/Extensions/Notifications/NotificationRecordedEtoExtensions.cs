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
    // Olayin icerik alanlarini tek bir alici ve kanal icin yeni satira tasir; tenant bildiriminde alici, web satirinda
    // Firebase projesi verilmez.
    public static Notification ToNotification(
        this NotificationRecordedEto source,
        Guid notificationGroupId,
        Guid channelId,
        Guid? firebaseProjectId,
        NotificationRecipientEto? recipient)
    {
        return new Notification
        {
            TenantId = source.TenantId,
            OrganizationId = source.OrganizationId,
            NotificationGroupId = notificationGroupId,
            ChannelId = channelId,
            FirebaseProjectId = firebaseProjectId,
            SenderUserId = source.SenderUserId,
            RecipientUserId = recipient?.RecipientUserId,
            ExternalId = source.ExternalId,
            TenantName = source.TenantName,
            OrganizationName = source.OrganizationName,
            SenderUserName = source.SenderUserName,
            RecipientUserName = recipient?.RecipientUserName,
            ModuleName = source.ModuleName,
            Title = source.Title,
            // Topic mobil kanalin kavramidir; web satiri topic tasimaz.
            Topic = firebaseProjectId is null ? null : source.Topic,
            IsTenantNotification = source.IsTenantNotification,
            ScheduledTime = source.ScheduledTime,
            Data = source.Data
        };
    }
}
