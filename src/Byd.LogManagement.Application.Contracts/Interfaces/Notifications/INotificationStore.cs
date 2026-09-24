using System.Threading;
using System.Threading.Tasks;
using Byd.LogManagement.Etos.Notifications;

namespace Byd.LogManagement.Interfaces.Notifications;

/// <summary>
/// Bildirim olaylarinin merkezi servise birakilma yolunu tanimlar.
/// </summary>
// islevi: Bildirim olayini tasima katmanina teslim eder.
// sistemdeki gorevi: Yakalama noktasini tasima ayrintisindan ayiran tek sozlesmedir.
public interface INotificationStore
{
    // Bildirim istegini merkezi servise birakir.
    Task SaveAsync(NotificationRecordedEto notification, CancellationToken cancellationToken = default);
}
