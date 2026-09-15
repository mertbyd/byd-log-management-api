using System.Threading.Tasks;
using Byd.LogManagement.Etos.ActivityLogs;
using Byd.LogManagement.Interfaces.ActivityLogs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Byd.LogManagement.ActivityLogs.Tracking;

/// <summary>
/// Etkinlik kaydini merkezi log servisine olay olarak gonderir.
/// </summary>
// islevi: Filtrenin kurdugu etkinligi dagitik olay olarak yayinlar.
// sistemdeki gorevi: Uretici uygulamanin merkezi log veritabanina baglanmadan kayit biraktigi store'dur.
public class DistributedEventActivityLogStore(IDistributedEventBus distributedEventBus)
    : IActivityLogStore, ITransientDependency
{
    // Etkinligi merkezi log servisine yayinlar.
    public Task SaveAsync(ActivityRecordedEto activityLog) => distributedEventBus.PublishAsync(activityLog);
}
