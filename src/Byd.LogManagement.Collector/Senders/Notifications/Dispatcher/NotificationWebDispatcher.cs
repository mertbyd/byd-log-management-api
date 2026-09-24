using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Byd.LogManagement.Entities.Notifications;
using Byd.LogManagement.Extensions.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Byd.LogManagement.Managers.Notifications;
using Volo.Abp.DependencyInjection;

namespace Byd.LogManagement.Senders.Notifications.Dispatcher;

/// <summary>
/// Bildirim satirlarinin ortak web gonderim akisini yurutur.
/// </summary>
// islevi: Metin secimini Manager'a, mesaj donusumunu extension'a, iletimi gondericiye devreder.
// sistemdeki gorevi: Handler ve worker'in commit sonrasinda kullandigi ortak gonderim sahibidir.
public class NotificationWebDispatcher(
    NotificationManager notificationManager,
    INotificationWebSender sender) : ITransientDependency
{
    // Verilen satirlari web mesajina cevirip SignalR ile alicilarina yollar.
    public virtual Task SendManyAsync(
        IEnumerable<Notification> notifications,
        CancellationToken cancellationToken = default)
        => Task.WhenAll(notifications.Select(notification =>
            sender.SendAsync(
                notification.TenantId,
                notification.ToWebMessage(notificationManager.Describe(notification)),
                cancellationToken)));
}
