using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

using Byd.FileManagement;
namespace Byd.LogManagement;

[DependsOn(
    typeof(FileManagementApplicationContractsModule),
    typeof(LogManagementDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class LogManagementApplicationContractsModule : AbpModule
{

}
