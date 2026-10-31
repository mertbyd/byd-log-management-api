using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using Byd.AuthManagement.EntityFrameworkCore;
using Byd.CoreExtensions;
using Byd.CoreExtensions.Extensions;
using Byd.CoreExtensions.Options;
using Byd.FileManagement;
using Byd.LogManagement.EntityFrameworkCore;
using Byd.LogManagement.MultiTenancy;
using Volo.Abp;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Swashbuckle;

namespace Byd.LogManagement;

[DependsOn(
    typeof(LogManagementCollectorModule),
    typeof(LogManagementHttpApiModule),
    typeof(AuthManagementEntityFrameworkCoreModule),
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpAspNetCoreMultiTenancyModule),
    typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule))]
// islevi: Log HTTP hostunun ABP modul kompozisyonunu kurar ve tuketilen core modulunu bu host'a baglar. Izin ve
// feature degerleri auth EF paketi uzerinden AuthManagement baglanti dizesindeki auth veritabanindan okunur.
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

        // Tenant token'daki claim'den cozulur; kapali kalirsa her istek host kapsaminda calisir ve host izinleri gecerli olur.
        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

        ConfigureAuthentication(context);

        context.Services.AddAbpSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "LogManagement API", Version = "v1" });
            options.CustomSchemaIds(type => type.FullName);
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });
            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                { new OpenApiSecuritySchemeReference("Bearer", document), [] }
            });

            var xmlDocumentationFile = $"{typeof(LogManagementHttpApiModule).Assembly.GetName().Name}.xml";
            var xmlDocumentationPath = Path.Combine(AppContext.BaseDirectory, xmlDocumentationFile);
            if (File.Exists(xmlDocumentationPath))
            {
                options.IncludeXmlComments(xmlDocumentationPath, includeControllerXmlComments: true);
            }
        });
    }

    // Token'i auth sunucusu uretir; bu host imzayi sunucunun yayinladigi anahtarla, audience'i de
    // token'a yazilan API scope'uyla dogrular.
    private static void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddAbpJwtBearer(options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.Audience = configuration["AuthServer:Audience"];
                options.RequireHttpsMetadata = configuration.GetValue<bool>("AuthServer:RequireHttpsMetadata");
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
        app.UseAuthentication();
        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "LogManagement API");
        });
        app.UseRateLimiter();
        app.UseCoreExtensions();//core module
        app.UseFileManagement();//file module
        // Organizasyon kapsami core middleware'inde kurulur; yetki kontrolu ondan sonra calisir.
        app.UseAuthorization();
        app.UseAuditing();
        app.UseConfiguredEndpoints();
        return Task.CompletedTask;
    }

    // Modullerin seed katkilarini (bildirim kanallari vb.) uygulama acilirken idempotent olarak calistirir.
    public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.ServiceProvider.GetRequiredService<IDataSeeder>().SeedAsync();
    }
}
