using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Byd.LogManagement.Entities.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Volo.Abp.DependencyInjection;

namespace Byd.LogManagement.Senders.Notifications.Dispatcher;

/// <summary>
/// Vadesi gelmis bildirim satirlarini kanallarinin gondericilerine dagitir.
/// </summary>
// islevi: Kayitli her gondericiye, hizmet ettigi kanalin satirlarini verir ve gonderimleri paralel yurutur.
// sistemdeki gorevi: Handler ve worker'in kullandigi tek dagitim noktasidir; kanali da tasima yolunu da bilmez,
// yeni kanal eklendiginde degismez.
public class NotificationDispatcher(IEnumerable<INotificationSender> senders)
    : INotificationDispatcher, ITransientDependency
{
    // Satiri olan her kanali kendi gondericisine verir; gondericisi olmayan kanalin satirlari gonderilmez.
    public virtual Task DispatchAsync(
        IReadOnlyDictionary<int, List<Notification>> notificationsByChannelCode,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(notificationsByChannelCode);
        return Task.WhenAll(senders
            .Where(sender => notificationsByChannelCode.ContainsKey(sender.ChannelCode))
            .Select(sender => sender.SendAsync(notificationsByChannelCode[sender.ChannelCode], cancellationToken)));
    }
}
