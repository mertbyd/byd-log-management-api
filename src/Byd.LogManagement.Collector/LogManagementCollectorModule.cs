using Volo.Abp.Modularity;

namespace Byd.LogManagement;

[DependsOn(typeof(LogManagementApplicationModule))]
// islevi: Uretici uygulamalardan gelen etkinlik olaylarinin handler'ini kaydeder.
// sistemdeki gorevi: Yalniz merkezi log host'unun referans ettigi tuketici modulu; handler Application'da olsaydi
// log paketini alan her uretici uygulama da olaya abone olurdu.
public class LogManagementCollectorModule : AbpModule
{
}
