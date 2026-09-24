using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Byd.LogManagement.Extensions.Notifications;
using Byd.LogManagement.Options;
using Byd.LogManagement.Workers.Notifications;
using Volo.Abp;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Modularity;

namespace Byd.LogManagement;

[DependsOn(
    typeof(LogManagementApplicationModule),
    typeof(AbpAspNetCoreSignalRModule))]
// islevi: Uretici uygulamalardan gelen olaylarin handler'larini, bildirim hub'ini secilen tasimayla ve ileri tarihli
// bildirim iscisini kaydeder.
// sistemdeki gorevi: Yalniz merkezi log host'unun referans ettigi tuketici modulu; handler Application'da olsaydi
// log paketini alan her uretici uygulama da olaya abone olurdu, hub orada olsaydi her uretici uygulamada acilirdi.
public class LogManagementCollectorModule : AbpModule
{
    // Host'un PreConfigure ile beyan ettigi tasimayi okur ve hub'i yalniz o tasimayla eslemeye ekler.
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var transportName = context.Services.ExecutePreConfiguredActions<NotificationWebOptions>().TransportName;

        Configure<AbpEndpointRouterOptions>(options =>
        {
            options.EndpointConfigureActions.Add(endpointContext =>
                endpointContext.Endpoints.MapNotificationWebEndpoints(transportName));
        });
    }

    // Ileri tarihli bildirim iscisini baslatir; isci yalniz merkezi log host'unda calisir.
    public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.AddBackgroundWorkerAsync<ScheduledNotificationWorker>();
    }
}
