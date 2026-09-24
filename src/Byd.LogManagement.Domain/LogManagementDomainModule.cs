using Volo.Abp.Domain;
using Volo.Abp.AuditLogging;
using Volo.Abp.Modularity;

using Byd.CoreExtensions;
using Byd.FileManagement;
namespace Byd.LogManagement;

[DependsOn(
    typeof(CoreExtensionsDomainModule),
    typeof(FileManagementDomainModule),
    typeof(AbpAuditLoggingDomainModule),
    typeof(AbpDddDomainModule),
    typeof(LogManagementDomainSharedModule)
)]
public class LogManagementDomainModule : AbpModule
{

}
