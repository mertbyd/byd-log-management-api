using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Interfaces.Notifications;
using Byd.LogManagement.Managers.Notifications;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace Byd.LogManagement.Workers.Notifications;

/// <summary>
/// Vadesi gelen ileri tarihli web ve mobil bildirimlerini gonderir.
/// </summary>
// islevi: Her turda vadesi gelmis ve gonderilmemis web ve mobil satirlari damgalar, damga yazildiktan sonra kendi
// gondericilerine verir.
// sistemdeki gorevi: Ileri tarihli bildirimlerin tek gondericisidir; anlik bildirimi handler gonderir.
// mirastan gelen:
//   ABP AsyncPeriodicBackgroundWorkerBase -> StartAsync/StopAsync ve DoWorkAsync hatalarinin loglanmasi
//   (ayrica try/catch yazilmaz)
// burada yazilan: DoWorkAsync
public class ScheduledNotificationWorker : AsyncPeriodicBackgroundWorkerBase
{
    // Tarama araligini bildirim sabitinden kurar.
    public ScheduledNotificationWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory)
        : base(timer, serviceScopeFactory)
    {
        Timer.Period = (int)TimeSpan.FromSeconds(NotificationConsts.ScheduledScanPeriodSeconds).TotalMilliseconds;
    }
    // Vadesi gelen satirlari kendi calisma biriminde damgalar, sonra push eder; damgasiz satir istemciye gitmez.
    protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {
        var services = workerContext.ServiceProvider;
        var cancellationToken = workerContext.CancellationToken;
        var notificationManager = services.GetRequiredService<NotificationManager>();
        using var unitOfWork = services.GetRequiredService<IUnitOfWorkManager>().Begin();
        var due = await notificationManager.MarkDueAsSentAsync(cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        await services
            .GetRequiredService<INotificationDispatcher>()
            .DispatchAsync(due, cancellationToken);
    }
}
