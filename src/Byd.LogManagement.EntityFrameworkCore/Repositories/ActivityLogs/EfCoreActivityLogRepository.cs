using System;
using Byd.CoreExtensions.Repositories;
using Byd.LogManagement.Entities.ActivityLogs;
using Byd.LogManagement.Interfaces.ActivityLogs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.LogManagement.EntityFrameworkCore.Repositories.ActivityLogs;

/// <summary>
/// IActivityLogRepository sozlesmesini EF Core DbContext'e baglar.
/// </summary>
// islevi: ActivityLog icin generic, bulk ve DataTable sorgularini saglar.
// sistemdeki gorevi: IActivityLogRepository sozlesmesinin EfExtendedRepository tabanli provider uygulamasidir.
// mirastan gelen: CRUD, filtreli liste, sayim, kullanim kontrolu ve bulk yazma yuzeyleri.
// burada yazilan: yok; etkinlik kaydi tabanin disinda sorgu istemez.
[ExposeServices(typeof(IActivityLogRepository))]
public class EfCoreActivityLogRepository
    : EfExtendedRepository<LogManagementDbContext, ActivityLog, Guid>,
        IActivityLogRepository
{
    // DbContext provider'ini extended repository tabanina devreder.
    public EfCoreActivityLogRepository(
        IDbContextProvider<LogManagementDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
