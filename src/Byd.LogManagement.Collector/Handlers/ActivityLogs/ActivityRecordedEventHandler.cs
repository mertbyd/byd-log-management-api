using System.Threading.Tasks;
using Byd.LogManagement.Etos.ActivityLogs;
using Byd.LogManagement.Extensions.ActivityLogs;
using Byd.LogManagement.Interfaces.ActivityLogs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Byd.LogManagement.Handlers.ActivityLogs;

/// <summary>
/// Gelen etkinlik olayini merkezi tabloya yazar.
/// </summary>
// islevi: Olayi ActivityLog satirina cevirip ekler.
// sistemdeki gorevi: Uretici uygulamalardan gelen etkinliklerin merkezi log servisindeki tuketicisidir.
public class ActivityRecordedEventHandler(IActivityLogRepository repository)
    : IDistributedEventHandler<ActivityRecordedEto>, ITransientDependency
{
    // Olaydaki etkinligi merkezi satir olarak ekler; kimlik ve olusturma zamanini ABP verir.
    public virtual async Task HandleEventAsync(ActivityRecordedEto eventData)
    {
        await repository.InsertAsync(eventData.ToActivityLog());
    }
}
