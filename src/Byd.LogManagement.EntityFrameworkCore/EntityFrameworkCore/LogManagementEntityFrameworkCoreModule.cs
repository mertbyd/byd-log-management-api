using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Byd.CoreExtensions.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace Byd.LogManagement.EntityFrameworkCore;

[DependsOn(
    typeof(CoreExtensionsEntityFrameworkCoreModule),
    typeof(LogManagementDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule)
)]
// islevi: File DbContext'ini PostgreSQL saglayicisiyla ABP'ye kaydeder.
// sistemdeki gorevi: File entity/repository altyapisinin EF Core kompozisyon kokudur; core ile ayni veritabanini paylasir.
public class LogManagementEntityFrameworkCoreModule : AbpModule
{
    // PostgreSQL saglayicisi yalniz File DbContext'ine baglanir; core kendi context'ini yonetir.
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure<LogManagementDbContext>(configurationContext =>
            {
                configurationContext.UseNpgsql();
            });
        });

        context.Services.AddAbpDbContext<LogManagementDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
        });
    }
}
