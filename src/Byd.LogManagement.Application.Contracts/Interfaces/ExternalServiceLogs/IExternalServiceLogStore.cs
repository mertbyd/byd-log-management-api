using System.Threading;
using System.Threading.Tasks;
using Byd.LogManagement.Etos.ExternalServiceLogs;

namespace Byd.LogManagement.Interfaces.ExternalServiceLogs;

/// <summary>
/// Yakalanan dis servis kaydinin nereye birakilacagini tanimlar.
/// </summary>
// islevi: Handler'in kurdugu dis servis log olayini tek cagriyla teslim alir.
// sistemdeki gorevi: Dis servis cagrisini yakalayan handler ile dagitik olay tasimasi arasindaki sinirdir.
public interface IExternalServiceLogStore
{
    // Dis servis log olayini merkezi log servisine teslim eder.
    Task SaveAsync(ExternalServiceLogRecordedEto externalServiceLog, CancellationToken cancellationToken = default);
}
