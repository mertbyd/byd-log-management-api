using Byd.LogManagement.Dtos.Notifications;
using Byd.LogManagement.Entities.Notifications;
using Byd.LogManagement.Etos.Notifications;

namespace Byd.LogManagement.Extensions.Notifications;

/// <summary>
/// Bildirim satirini ve canli akis olayini web istemcisine giden mesaja cevirir.
/// </summary>
// islevi: Satirin alanlarini web mesajina tasir; metni cagiranin cozdugu dilden alir.
// sistemdeki gorevi: Dispatcher'in gonderecegi web mesajini bildirim satirindan, canli akis handler'inin mesajini olaydan
// kurar.
public static class NotificationExtensions
{
    // Satiri, istemcinin dilinde cozulmus metinle birlikte web mesajina cevirir.
    public static NotificationWebMessageDto ToWebMessage(this Notification notification, string description) => new()
    {
        Id = notification.Id,
        NotificationGroupId = notification.NotificationGroupId,
        SenderUserId = notification.SenderUserId,
        SenderUserName = notification.SenderUserName,
        RecipientUserId = notification.RecipientUserId,
        RecipientUserName = notification.RecipientUserName,
        ExternalId = notification.ExternalId,
        ModuleName = notification.ModuleName,
        Title = notification.Title,
        IsTenantNotification = notification.IsTenantNotification,
        IsRead = notification.IsRead,
        ScheduledTime = notification.ScheduledTime,
        SentTime = notification.SentTime ?? notification.ScheduledTime,
        Description = description
    };

    // Canli akis olayini web istemcisine giden mesaja cevirir.
    public static NotificationStreamMessageDto ToStreamMessage(this NotificationStreamedEto stream) => new()
    {
        ModuleName = stream.ModuleName,
        Name = stream.Name,
        Payload = stream.Payload
    };
}
