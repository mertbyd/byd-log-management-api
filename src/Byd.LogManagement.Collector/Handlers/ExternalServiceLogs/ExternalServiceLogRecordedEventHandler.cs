using System.Threading.Tasks;
using Byd.LogManagement.Etos.ExternalServiceLogs;
using Byd.LogManagement.Extensions.ExternalServiceLogs;
using Byd.LogManagement.Managers.ExternalServiceLogs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Byd.LogManagement.Handlers.ExternalServiceLogs;

/// <summary>
/// Gelen dis servis cagri olayini merkezi tabloya yazar.
/// </summary>
// islevi: Olayi ExternalServiceLog satirina cevirip Manager'a verir.
// sistemdeki gorevi: Uretici uygulamalardan gelen dis servis cagrilarinin merkezi log servisindeki tuketicisidir.
public class ExternalServiceLogRecordedEventHandler(ExternalServiceLogManager manager)
    : IDistributedEventHandler<ExternalServiceLogRecordedEto>, ITransientDependency
{
    // Olaydaki cagriyi merkezi satir olarak ekler; turetilen alanlari Manager doldurur.
    public virtual async Task HandleEventAsync(ExternalServiceLogRecordedEto eventData)
    {
        await manager.HandleLogAsync(eventData.ToExternalServiceLog());
    }
}
