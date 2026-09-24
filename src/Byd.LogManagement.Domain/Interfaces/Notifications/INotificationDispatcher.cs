using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byd.LogManagement.Entities.Notifications;

namespace Byd.LogManagement.Interfaces.Notifications;

/// <summary>
/// Vadesi gelmis bildirim satirlarini kanallarinin gondericilerine dagitir.
/// </summary>
// islevi: Kanal is koduna gore gruplanmis satirlari tek cagriyla dagitir.
// sistemdeki gorevi: Olay isleyicisi ile zamanlanmis isciyi kanal ve tasima ayrintisindan ayirir.
public interface INotificationDispatcher
{
    // Her kanalin satirlarini o kanala kayitli gondericiye verir.
    Task DispatchAsync(
        IReadOnlyDictionary<int, List<Notification>> notificationsByChannelCode,
        CancellationToken cancellationToken = default);
}
