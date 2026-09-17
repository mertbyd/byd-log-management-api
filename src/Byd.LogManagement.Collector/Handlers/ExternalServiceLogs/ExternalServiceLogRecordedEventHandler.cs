using System.Threading.Tasks;
using Byd.LogManagement.Etos.ExternalServiceLogs;
using Byd.LogManagement.Extensions.ExternalServiceLogs;
using Byd.LogManagement.Interfaces.ExternalServiceLogs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Byd.LogManagement.Handlers.ExternalServiceLogs;

/// <summary>
/// Gelen dis servis cagri olayini merkezi tabloya yazar.
/// </summary>
// islevi: Olayi ExternalServiceLog satirina cevirip ekler.
// sistemdeki gorevi: Uretici uygulamalardan gelen dis servis cagrilarinin merkezi log servisindeki tuketicisidir.
public class ExternalServiceLogRecordedEventHandler(IExternalServiceLogRepository repository)
    : IDistributedEventHandler<ExternalServiceLogRecordedEto>, ITransientDependency
{
    // Olaydaki cagriyi merkezi satir olarak ekler; kimligi ve olusturma zamanini ABP verir.
    public virtual async Task HandleEventAsync(ExternalServiceLogRecordedEto eventData)
    {
        await repository.InsertAsync(eventData.ToExternalServiceLog());
    }
}
