using Localization.Resources.AbpUi;
using Byd.LogManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

using Byd.CoreExtensions;
namespace Byd.LogManagement;

[DependsOn(
    typeof(CoreExtensionsHttpApiModule),
    typeof(LogManagementApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class LogManagementHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(LogManagementHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<LogManagementResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
