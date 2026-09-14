using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.EntityFrameworkCore;
using Byd.LogManagement.Extensions;
using Volo.Abp.Data;

namespace Byd.LogManagement.EntityFrameworkCore;

/// <summary>
/// Audit log DbContext'ini tasarim aninda ureten fabrika.
/// </summary>
// islevi: Audit log context'inin baglanti dizesi adini bildirir ve ornegini kurar.
// sistemdeki gorevi: Audit log tablolari kendi migration setini tasidigi icin bu context'in de ayri bir tasarim
// zamani girisi vardir; baglanti dizesi context'in kendi ConnectionStringName beyaniyla aynidir.
public class LogManagementAuditLoggingDbContextFactory
    : CoreExtensionsDesignTimeDbContextFactory<LogManagementAuditLoggingDbContext>
{
    protected override string ConnectionStringName => ConnectionStrings.DefaultConnectionStringName;

    // Taban yalniz core'un semasini cozer; audit tablolarinin semasi ve on eki, EF modulunun acilistaki cagrisinin
    // karsiligi olarak context kurulmadan once burada cozulur.
    protected override LogManagementAuditLoggingDbContext CreateInstance(
        DbContextOptions<LogManagementAuditLoggingDbContext> options)
    {
        ServiceConfigurationContextExtension.ResolveSchemaAndPrefix();
        return new(options);
    }
}
