using Volo.Abp.Domain;
using Volo.Abp.AuditLogging;
using Volo.Abp.Modularity;

using Byd.CoreExtensions;
namespace Byd.LogManagement;

[DependsOn(
    typeof(CoreExtensionsDomainModule),
    typeof(AbpAuditLoggingDomainModule),
    typeof(AbpDddDomainModule),
    typeof(LogManagementDomainSharedModule)
)]
public class LogManagementDomainModule : AbpModule
{

}
