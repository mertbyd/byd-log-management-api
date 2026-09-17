using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.Extensions;
using Byd.CoreExtensions.EntityFrameworkCore;
using Byd.LogManagement.Entities.ActivityLogs;
using Byd.LogManagement.Entities.ExternalServiceLogs;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.LogManagement.EntityFrameworkCore;

[ConnectionStringName(LogManagementDbProperties.ConnectionStringName)]
public class LogManagementDbContext : BydDbContext<LogManagementDbContext>, ILogManagementDbContext
{
    public DbSet<ActivityLog> ActivityLogs { get; set; }
    public DbSet<ExternalServiceLog> ExternalServiceLogs { get; set; }

    public LogManagementDbContext(DbContextOptions<LogManagementDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureLogManagement();

        builder.ApplySnakeCaseNaming();
    }
}
