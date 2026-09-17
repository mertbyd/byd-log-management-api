using Byd.CoreExtensions.Interfaces;
using Byd.LogManagement.Entities.ExternalServiceLogs;

namespace Byd.LogManagement.Interfaces.ExternalServiceLogs;

/// <summary>
/// Dis servis kaydi icin extended repository sozlesmesini tanimlar.
/// </summary>
// islevi: Dis servis log ekleme, filtreli liste ve sayim yeteneklerini tabandan alir.
// sistemdeki gorevi: Dis servis log kullanim senaryolari ile EF Core uygulamasi arasindaki veri erisim siniridir.
public interface IExternalServiceLogRepository : IExtendedRepository<ExternalServiceLog>
{
}
