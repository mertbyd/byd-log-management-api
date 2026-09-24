using Byd.LogManagement.Dtos.Notifications;
using Byd.LogManagement.Entities.Notifications;
using Riok.Mapperly.Abstractions;

namespace Byd.LogManagement.Mappers.Notifications;

/// <summary>
/// Bildirim entity ve DTO eslemelerini tanimlar.
/// </summary>
// islevi: Bildirim satirini Manager'in istek diline gore sectigi metinle cevaba Mapperly tarafindan uretilen
// kodla donusturur.
// sistemdeki gorevi: Application katmanindaki bildirim cevap eslemesinin sahibidir.
[Mapper]
public partial class NotificationMapper
{
    // Bildirim satirini, Manager'in istek dilinde sectigi metinle tek cevapta esler.
    public partial NotificationDto Map(Notification source, string description);
}
