using System;
using Byd.CoreExtensions.Repositories;
using Byd.LogManagement.Entities.ExternalServiceLogs;
using Byd.LogManagement.Interfaces.ExternalServiceLogs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.LogManagement.EntityFrameworkCore.Repositories.ExternalServiceLogs;

/// <summary>
/// IExternalServiceLogRepository sozlesmesini EF Core DbContext'e baglar.
/// </summary>
// islevi: ExternalServiceLog icin generic, bulk ve DataTable sorgularini saglar.
// sistemdeki gorevi: IExternalServiceLogRepository sozlesmesinin EfExtendedRepository tabanli provider uygulamasidir.
[ExposeServices(typeof(IExternalServiceLogRepository))]
public class EfCoreExternalServiceLogRepository
    : EfExtendedRepository<LogManagementDbContext, ExternalServiceLog, Guid>,
        IExternalServiceLogRepository
{
    // DbContext provider'ini extended repository tabanina devreder.
    public EfCoreExternalServiceLogRepository(
        IDbContextProvider<LogManagementDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
