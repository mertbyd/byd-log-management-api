using System.Threading.Tasks;
using Byd.LogManagement.Etos.ActivityLogs;

namespace Byd.LogManagement.Interfaces.ActivityLogs;

/// <summary>
/// Yakalanan etkinligin nereye birakilacagini tanimlar.
/// </summary>
// islevi: Filtrenin kurdugu etkinlik olayini tek cagriyla teslim alir.
// sistemdeki gorevi: Etkinligi yakalayan filtre ile tasima arasindaki sinirdir; filtre olayin nasil gittigini bilmez.
public interface IActivityLogStore
{
    // Etkinlik olayini merkezi log servisine teslim eder.
    Task SaveAsync(ActivityRecordedEto activityLog);
}
