using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

using Byd.CoreExtensions;
namespace Byd.LogManagement;

[DependsOn(
    typeof(CoreExtensionsApplicationContractsModule),
    typeof(LogManagementDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class LogManagementApplicationContractsModule : AbpModule
{

}
