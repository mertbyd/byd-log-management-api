using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Byd.LogManagement.Etos.Notifications;
using Byd.LogManagement.Extensions.Notifications;
using Byd.LogManagement.Workers.Notifications;
using Volo.Abp;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Modularity;

namespace Byd.LogManagement;

[DependsOn(
    typeof(LogManagementApplicationModule),
    typeof(AbpAspNetCoreSignalRModule))]
// islevi: Uretici uygulamalardan gelen olaylarin handler'larini, WebSocket ve SSE ile acilan bildirim hub'ini, hub'in sorgu
// parametresiyle gelen token'ini ve ileri tarihli bildirim iscisini kaydeder; canli akis olayini inbox disinda tutar.
// sistemdeki gorevi: Yalniz merkezi log host'unun referans ettigi tuketici modulu; handler Application'da olsaydi
// log paketini alan her uretici uygulama da olaya abone olurdu, hub orada olsaydi her uretici uygulamada acilirdi.
public class LogManagementCollectorModule : AbpModule
{
    // Bildirim hub'ini WebSocket ve SSE tasimalariyla eslemeye ekler ve hub token'ini sorgu parametresinden okutur. Canli akis
    // kalici olmadigi icin inbox'a yazilmaz; olay dogrudan handler'a verilir.
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddNotificationHubAccessToken();
        Configure<AbpDistributedEventBusOptions>(options =>
            options.Inboxes.Configure(config => config.EventSelector = type => type != typeof(NotificationStreamedEto)));
        Configure<AbpEndpointRouterOptions>(options =>
        {
            options.EndpointConfigureActions.Add(endpointContext =>
                endpointContext.Endpoints.MapNotificationWebEndpoints());
        });
    }

    // Ileri tarihli bildirim iscisini baslatir; isci yalniz merkezi log host'unda calisir.
    public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.AddBackgroundWorkerAsync<ScheduledNotificationWorker>();
    }
}
