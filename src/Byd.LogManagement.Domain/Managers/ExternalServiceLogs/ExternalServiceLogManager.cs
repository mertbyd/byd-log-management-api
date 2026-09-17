using System;
using System.Threading.Tasks;
using Byd.CoreExtensions.Managers;
using Byd.LogManagement.Entities.ExternalServiceLogs;
using Byd.LogManagement.Interfaces.ExternalServiceLogs;

namespace Byd.LogManagement.Managers.ExternalServiceLogs;

/// <summary>
/// Dis servis cagri kaydinin yazilmasini yonetir.
/// </summary>
// islevi: Yakalanan cagri satirini merkezi tabloya ekler.
// sistemdeki gorevi: Tuketiciyi repository'den ve yazma kararindan uzak tutan domain sahibidir.
public class ExternalServiceLogManager : BaseManager<ExternalServiceLog, Guid>
{
    // ExternalServiceLog extended repository'sini ortak guard tabanina devreder.
    public ExternalServiceLogManager(IExternalServiceLogRepository repository)
        : base(repository)
    {
    }

    // Cagri satirini ekler; kimligi ve olusturma zamanini ABP verir.
    public virtual async Task HandleLogAsync(ExternalServiceLog externalServiceLog)
    {
        await Repository.InsertAsync(externalServiceLog);
    }
}
