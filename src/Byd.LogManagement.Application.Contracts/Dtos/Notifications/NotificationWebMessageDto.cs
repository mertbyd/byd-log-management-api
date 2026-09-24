using System;

namespace Byd.LogManagement.Dtos.Notifications;

/// <summary>
/// Web istemcisine anlik olarak iletilen tek bir bildirimin cevap alanlarini tasir.
/// </summary>
// islevi: Bildirimin kimligini, gondereni, alicisini, modulunu, basligini, zamanlarini ve metnini tasir.
// sistemdeki gorevi: SignalR hub'i uzerinden (SSE ya da WebSocket tasimasiyla) tarayiciya giden sozlesmedir;
// tenant bildirimi tenant'taki herkese, digeri yalniz alicisina gider.
public class NotificationWebMessageDto
{
    public Guid Id { get; set; }
    public Guid NotificationGroupId { get; set; }
    public Guid? SenderUserId { get; set; }
    public string? SenderUserName { get; set; }
    public Guid? RecipientUserId { get; set; }
    public string? RecipientUserName { get; set; }
    public Guid? ExternalId { get; set; }
    public string ModuleName { get; set; } = default!;
    public string Title { get; set; } = default!;
    public bool IsTenantNotification { get; set; }
    public DateTime ScheduledTime { get; set; }
    public DateTime SentTime { get; set; }
    public string Description { get; set; } = default!;
}
