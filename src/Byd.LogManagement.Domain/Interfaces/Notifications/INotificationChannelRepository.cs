using Byd.CoreExtensions.Interfaces;
using Byd.LogManagement.Entities.Notifications;

namespace Byd.LogManagement.Interfaces.Notifications;

/// <summary>
/// Bildirim kanali lookup kaydi icin extended repository sozlesmesini tanimlar.
/// </summary>
// islevi: Ekleme, filtreli liste ve sayim yeteneklerini tabandan alir; ek sorgu tanimlamaz.
// sistemdeki gorevi: Bildirim kanali lookup kullanim senaryolari ile EF Core uygulamasi arasindaki veri erisim siniridir.
public interface INotificationChannelRepository : IExtendedRepository<NotificationChannel>
{
}
