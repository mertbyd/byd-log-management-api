using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.LogManagement.EntityFrameworkCore;

[ConnectionStringName(LogManagementDbProperties.ConnectionStringName)]
public interface ILogManagementDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
