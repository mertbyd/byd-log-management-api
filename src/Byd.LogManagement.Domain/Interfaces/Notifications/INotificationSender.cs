using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byd.LogManagement.Entities.Notifications;

namespace Byd.LogManagement.Interfaces.Notifications;

/// <summary>
/// Bir bildirim kanalinin satirlarini kendi tasima yoluyla iletir.
/// </summary>
// islevi: Gondericinin hizmet ettigi kanalin is kodunu bildirir ve o kanalin satirlarini iletir.
// sistemdeki gorevi: Dispatcher'in hangi gondericinin calisacagina karar verdigi tek sozlesmedir; yeni kanal,
// dispatcher degismeden bu sozlesmeyi uygulayan bir sinif eklenerek acilir.
public interface INotificationSender
{
    // Gondericinin hizmet ettigi bildirim kanalinin is kodu.
    int ChannelCode { get; }

    // Kanalin satirlarini iletir; gonderim sonucunu tutmak gerekiyorsa gonderici kendi gunlugune yazar.
    Task SendAsync(IReadOnlyCollection<Notification> notifications, CancellationToken cancellationToken = default);
}
