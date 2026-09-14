using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using Byd.CoreExtensions;
using Byd.CoreExtensions.Extensions;
using Byd.CoreExtensions.Options;
using Byd.LogManagement.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace Byd.LogManagement;

[DependsOn(
    typeof(LogManagementApplicationModule),
    typeof(LogManagementEntityFrameworkCoreModule),
    typeof(LogManagementHttpApiModule),
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule))]
// islevi: Log HTTP hostunun ABP modul kompozisyonunu kurar ve tuketilen core modulunu bu host'a baglar.
// sistemdeki gorevi: Log modulunu ayaga kaldiran calistirilabilir host; core semasini cozer, core modul ayarini beyan eder ve core pipeline'ini takar.
public class LogManagementHttpApiHostModule : AbpModule
{
    // Core tablolarinin sema ve on ekini acilisin en basinda cozer.
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        ServiceConfigurationContextExtension.ResolveSchemaAndPrefix();
    }

    // Core modul adi ve ortam adini beyan eder; core saglayicilari bu beyan olmadan acilista durur.
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var environmentName = context.Services.GetHostingEnvironment().EnvironmentName;
        Configure<CoreExtensionsModuleOptions>(options =>
        {
            options.ModuleName = Assembly.GetEntryAssembly()?.GetName().Name?
                .Replace(".HttpApi.Host", string.Empty);
            options.EnvironmentName = environmentName;
        });

        context.Services.AddAbpSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "LogManagement API", Version = "v1" });
            options.CustomSchemaIds(type => type.FullName);
        });
    }

    // HTTP pipeline'ini kurar; core mukerrer istek korumasi rate limiter'dan sonra, endpointlerden once takilir.
    public override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        if (context.GetEnvironment().IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseCorrelationId();
        app.UseRouting();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "LogManagement API");
        });
        app.UseRateLimiter();
        app.UseCoreExtensions();//core module 
        app.UseAuditing();
        app.UseConfiguredEndpoints();
        return Task.CompletedTask;
    }
}
