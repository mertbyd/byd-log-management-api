using Byd.CoreExtensions.Interfaces;
using Byd.LogManagement.Entities.ActivityLogs;

namespace Byd.LogManagement.Interfaces.ActivityLogs;

/// <summary>
/// Etkinlik kaydi icin extended repository sozlesmesini tanimlar.
/// </summary>
// islevi: ActivityLog ekleme, filtreli liste ve sayim yeteneklerini tabandan alir; ek sorgu tanimlamaz.
// sistemdeki gorevi: Etkinlik kaydi kullanim senaryolari ile EF Core uygulamasi arasindaki veri erisim siniridir.
public interface IActivityLogRepository : IExtendedRepository<ActivityLog>
{
}
