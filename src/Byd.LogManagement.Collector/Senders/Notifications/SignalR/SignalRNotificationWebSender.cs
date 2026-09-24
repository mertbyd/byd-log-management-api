using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Dtos.Notifications;
using Byd.LogManagement.Hubs.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Volo.Abp.DependencyInjection;

namespace Byd.LogManagement.Senders.Notifications.SignalR;

/// <summary>
/// Web bildirimini SignalR hub'i uzerinden iletir.
/// </summary>
// islevi: Tenant bildirimini tenant grubuna, digerini alicinin butun baglantilarina sabit olay adiyla gonderir.
// sistemdeki gorevi: INotificationWebSender sozlesmesinin SignalR uygulamasidir; baglantilari ve SSE tasimasini
// SignalR'in kendisi yonetir.
public class SignalRNotificationWebSender(IHubContext<NotificationWebHub> hubContext)
    : INotificationWebSender, ITransientDependency
{
    // Tenant bildirimini tenant'taki herkese, digerini alicinin butun sekmelerine iletir.
    public virtual Task SendAsync(Guid? tenantId, NotificationWebMessageDto message, CancellationToken cancellationToken = default)
    {
        var clients = message.IsTenantNotification
            ? hubContext.Clients.Group($"{NotificationConsts.TenantGroupPrefix}:{tenantId}")
            : hubContext.Clients.User($"{message.RecipientUserId}");
        return clients.SendAsync(NotificationConsts.WebMessageName, message, cancellationToken);
    }
}
