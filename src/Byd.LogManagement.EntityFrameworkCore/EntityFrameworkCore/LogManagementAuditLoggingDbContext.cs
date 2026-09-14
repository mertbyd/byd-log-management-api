using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Extensions;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.LogManagement.EntityFrameworkCore;

/// <summary>
/// ABP audit log tablolarini modulun adlandirma bicimiyle sahiplenen DbContext.
/// </summary>
// islevi: Audit log tablolarinin modelini kurar ve konvansiyondan gelen fiziksel adlari snake_case bicimine cevirir.
// sistemdeki gorevi: ReplaceDbContext beyani sayesinde cercevenin AbpAuditLoggingDbContext'i yerine gecer; ABP'nin
// audit store'u ve IAuditLogRepository bu context uzerinden yazar ve okur.
[ReplaceDbContext(typeof(IAuditLoggingDbContext))]
[ConnectionStringName(ConnectionStrings.DefaultConnectionStringName)]
public class LogManagementAuditLoggingDbContext
    : AbpDbContext<LogManagementAuditLoggingDbContext>, IAuditLoggingDbContext
{
    // Audit entity'leri cerceveden gelir; IAuditLoggingDbContext bu iki seti talep eder.
    public DbSet<AuditLog> AuditLogs { get; set; } = default!;

    public DbSet<AuditLogExcelFile> AuditLogExcelFiles { get; set; } = default!;//todo araştır

    public LogManagementAuditLoggingDbContext(DbContextOptions<LogManagementAuditLoggingDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureAuditLogging();
        builder.ApplySnakeCaseNaming();
    }
}
