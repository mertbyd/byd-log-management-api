using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Entities.Notifications;
using Byd.LogManagement.Extensions.Notifications;
using Byd.LogManagement.Hubs.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Byd.LogManagement.Managers.Notifications;
using Volo.Abp.DependencyInjection;

namespace Byd.LogManagement.Senders.Notifications.SignalR;

/// <summary>
/// Web kanalinin satirlarini SignalR hub'i uzerinden iletir.
/// </summary>
// islevi: Her satiri istemcinin dilindeki metinle web mesajina cevirir; tenant bildirimini tenant grubuna, digerini
// alicinin butun baglantilarina sabit olay adiyla gonderir.
// sistemdeki gorevi: Web kanalinin gonderim sozlesmesini uygulayan siniftir; baglantilari SignalR yonetir, gonderim
// gunlugu tutulmaz.
[ExposeServices(typeof(INotificationSender))]
public class SignalRNotificationWebSender(
    NotificationManager notificationManager,
    IHubContext<NotificationWebHub> hubContext)
    : INotificationSender, ITransientDependency
{
    // Gondericinin hizmet ettigi web kanalinin is kodu.
    public virtual int ChannelCode => NotificationConsts.WebChannelCode;

    // Kanalin butun satirlarini paralel iletir.
    public virtual Task SendAsync(
        IReadOnlyCollection<Notification> notifications,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(notifications);
        return Task.WhenAll(notifications.Select(notification => SendAsync(notification, cancellationToken)));
    }

    // Yayin bildirimini kapsamindaki herkese, digerini alicinin butun sekmelerine iletir.
    private async Task SendAsync(Notification notification, CancellationToken cancellationToken = default)
    {
        var message = notification.ToWebMessage(notificationManager.TextOf(notification));
        var clients = notification.IsTenantNotification
            ? hubContext.Clients.Group(BroadcastGroup(notification))
            : hubContext.Clients.User($"{notification.RecipientUserId}");
        await clients.SendAsync(NotificationConsts.WebMessageName, message, cancellationToken);
    }

    // Organizasyon kapsaminda olusan yayin organizasyonun grubuna, organizasyonsuz olusan yayin tenant grubuna gider.
    private static string BroadcastGroup(Notification notification)
        => notification.OrganizationId is null
            ? $"{NotificationConsts.TenantGroupPrefix}:{notification.TenantId}"
            : $"{NotificationConsts.OrganizationGroupPrefix}:{notification.OrganizationId}";
}
