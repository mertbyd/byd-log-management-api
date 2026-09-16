using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.EntityFrameworkCore;

namespace Byd.LogManagement.EntityFrameworkCore;

/// <summary>
/// Log modulu DbContext'ini tasarim aninda ureten fabrika.
/// </summary>
// islevi: Log context'inin baglanti dizesi adini bildirir ve ornegini kurar.
// sistemdeki gorevi: ActivityLogs tablosunun migration seti icin tasarim zamani girisidir; baglanti dizesi context'in
// kendi ConnectionStringName beyaniyla aynidir.
public class LogManagementDbContextFactory
    : CoreExtensionsDesignTimeDbContextFactory<LogManagementDbContext>
{
    protected override string ConnectionStringName => LogManagementDbProperties.ConnectionStringName;

    // Taban core semasini cozdukten sonra log context'ini verilen seceneklerle kurar.
    protected override LogManagementDbContext CreateInstance(
        DbContextOptions<LogManagementDbContext> options)
        => new(options);
}
