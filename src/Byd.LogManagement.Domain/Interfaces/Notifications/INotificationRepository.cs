using Byd.CoreExtensions.Interfaces;
using Byd.LogManagement.Entities.Notifications;

namespace Byd.LogManagement.Interfaces.Notifications;

/// <summary>
/// Bildirim kaydi icin extended repository sozlesmesini tanimlar.
/// </summary>
// islevi: Ekleme, filtreli liste ve sayim yeteneklerini tabandan alir.
// sistemdeki gorevi: Bildirim kullanim senaryolari ile EF Core uygulamasi arasindaki veri erisim siniridir.
public interface INotificationRepository : IExtendedRepository<Notification>
{
}
