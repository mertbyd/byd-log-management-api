using Volo.Abp.Modularity;

namespace Byd.LogManagement;

[DependsOn(
    typeof(LogManagementApplicationModule),
    typeof(LogManagementDomainTestModule)
    )]
public class LogManagementApplicationTestModule : AbpModule
{

}
