using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Byd.CoreExtensions.EntityFrameworkCore;
using Byd.LogManagement.Extensions;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace Byd.LogManagement.EntityFrameworkCore;

[DependsOn(
    typeof(CoreExtensionsEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(LogManagementDomainModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule)
)]
// islevi: Log DbContext'lerini PostgreSQL saglayicisiyla ABP'ye kaydeder ve audit kaydina organizasyon contributor'ini ekler.
// sistemdeki gorevi: Log ve audit altyapisinin EF Core kompozisyon kokudur; core ile ayni veritabanini paylasir.
public class LogManagementEntityFrameworkCoreModule : AbpModule
{
    // Audit log tablolarinin sema ve on ekini acilisin en basinda cozer; degerler EF Core modeli kurulurken okunur,
    // bu yuzden model kurulumundan once tamamlanmis olmalari gerekir.
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        ServiceConfigurationContextExtension.ResolveSchemaAndPrefix();
    }

    // Saglayiciyi, context kayitlarini ve audit contributor'ini kurar; PostgreSQL saglayicisi yalniz ana Log
    // DbContext'ine baglanir, core varsayilani diger context'lere uygular.
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure<LogManagementDbContext>(configurationContext =>
            {
                configurationContext.UseNpgsql();
            });
        });

        // Cercevenin audit log context'i yerine gecen context de burada kaydedilir; kayit olmadan ReplaceDbContext
        // beyani karsiliksiz kalir ve audit store cercevenin kendi context'ine yazmaya calisir.
        context.Services.AddAbpDbContext<LogManagementAuditLoggingDbContext>();

        context.Services.AddAbpDbContext<LogManagementDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        // Organizasyon kapsami her audit kaydina modulun kendi contributor'i ile yazilir; tuketen host ayrica kayit
        // yapmadan ayni alanlari alir.
        Configure<AbpAuditingOptions>(options =>
        {
            options.Contributors.Add(new OrganizationAuditLogContributor());
        });
    }
}
