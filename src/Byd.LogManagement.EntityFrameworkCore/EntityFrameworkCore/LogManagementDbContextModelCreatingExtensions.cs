using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Byd.LogManagement.EntityFrameworkCore;

public static class LogManagementDbContextModelCreatingExtensions
{
    public static void ConfigureLogManagement(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.ApplyConfigurationsFromAssembly(
            typeof(LogManagementDbContextModelCreatingExtensions).Assembly);
    }
}
