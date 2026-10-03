using System.Threading;
using System.Threading.Tasks;
using Byd.LogManagement.Etos.ExternalServiceLogs;
using Byd.LogManagement.Interfaces.ExternalServiceLogs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace Byd.LogManagement.ExternalServiceLogs.Tracking;

/// <summary>
/// Dis servis cagri kaydini merkezi log servisine olay olarak gonderir.
/// </summary>
// islevi: Sarmalayicinin kurdugu cagri kaydini kendi calisma biriminde olay kutusuna yazip hemen tamamlar.
// sistemdeki gorevi: Uretici uygulamanin merkezi log veritabanina baglanmadan kayit biraktigi store'dur. Kayit,
// cagriyi yapan isin calisma biriminden ayri tutulur; is geri alinsa da cagrinin yapildigi gercegi kalir.
public class DistributedEventExternalServiceLogStore(
    IDistributedEventBus distributedEventBus,
    IUnitOfWorkManager unitOfWorkManager) : IExternalServiceLogStore, ITransientDependency
{
    // Cagri kaydini kendi calisma biriminde olay kutusuna yazar ve birimi hemen tamamlar.
    public virtual async Task SaveAsync(
        ExternalServiceLogRecordedEto externalServiceLog,
        CancellationToken cancellationToken = default)
    {
        using var unitOfWork = unitOfWorkManager.Begin(requiresNew: true);
        await distributedEventBus.PublishAsync(externalServiceLog);
        await unitOfWork.CompleteAsync(cancellationToken);
    }
}
