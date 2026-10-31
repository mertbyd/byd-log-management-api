using System.Threading;
using System.Threading.Tasks;
using Byd.LogManagement.Dtos.Notifications;

namespace Byd.LogManagement.Interfaces.Notifications;

/// <summary>
/// Uretici uygulamada bildirim isteginin yakalanma yolunu tanimlar.
/// </summary>
// islevi: Cagiranin verdigi bildirim kaydini ya da canli akisi baglam alanlariyla tamamlayip merkezi servise birakir.
// sistemdeki gorevi: Uretici uygulamanin bildirim istemek icin bildigi tek sozlesmedir.
public interface INotificationLogger
{
    // Bildirimi merkezi servise kuyruga aldirir.
    Task NotifyAsync(NotificationRecord record, CancellationToken cancellationToken = default);

    // Canli akisi kalici olmayan yoldan kapsamindaki web istemcilerine iletir; satir yazilmaz.
    Task StreamAsync(string name, object payload, CancellationToken cancellationToken = default);
}
