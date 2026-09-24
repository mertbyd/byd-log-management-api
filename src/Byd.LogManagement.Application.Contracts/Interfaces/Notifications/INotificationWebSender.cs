using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.LogManagement.Dtos.Notifications;

namespace Byd.LogManagement.Interfaces.Notifications;

/// <summary>
/// Web bildirimini tarayiciya gonderme sozlesmesidir.
/// </summary>
// islevi: Tenant bildirimini tenant'taki herkese, digerini alicisina iletir.
// sistemdeki gorevi: Gonderim akisinin tasima ayrintisini bilmeden cagirdigi sozlesmedir; SignalR gondericisi uygular.
public interface INotificationWebSender
{
    // Mesaji tenant icindeki alicisina iletir; yalniz tenant bildirimi tenant'taki herkese gider.
    Task SendAsync(Guid? tenantId, NotificationWebMessageDto message, CancellationToken cancellationToken = default);
}
