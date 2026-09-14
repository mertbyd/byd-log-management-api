using Volo.Abp.Modularity;

namespace Byd.LogManagement;

[DependsOn(
    typeof(LogManagementDomainModule),
    typeof(LogManagementTestBaseModule)
)]
public class LogManagementDomainTestModule : AbpModule
{

}
