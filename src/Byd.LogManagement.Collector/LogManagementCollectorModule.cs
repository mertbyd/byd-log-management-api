using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Modularity;

namespace Byd.LogManagement;

[DependsOn(
    typeof(LogManagementApplicationModule),
    typeof(AbpAspNetCoreSignalRModule))]
// islevi: Uretici uygulamalardan gelen olaylarin handler'larini ve web bildirim hub'ini kaydeder.
// sistemdeki gorevi: Yalniz merkezi log host'unun referans ettigi tuketici modulu; handler Application'da olsaydi
// log paketini alan her uretici uygulama da olaya abone olurdu, hub orada olsaydi her uretici uygulamada acilirdi.
public class LogManagementCollectorModule : AbpModule
{
}
