using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Byd.LogManagement.ActivityLogs.Tracking;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

using Byd.CoreExtensions;
namespace Byd.LogManagement;

[DependsOn(
    typeof(CoreExtensionsApplicationModule),
    typeof(LogManagementDomainModule),
    typeof(LogManagementApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpMapperlyModule)
    )]
public class LogManagementApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<LogManagementApplicationModule>();
        Configure<MvcOptions>(options => options.Filters.AddService<ActivityTrackingFilter>());
    }
}
