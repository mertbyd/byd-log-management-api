using System;
using Byd.CoreExtensions.Repositories;
using Byd.LogManagement.Entities.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.LogManagement.EntityFrameworkCore.Repositories.Notifications;

/// <summary>
/// INotificationRepository sozlesmesini EF Core DbContext'e baglar.
/// </summary>
// islevi: Notification icin generic, bulk ve DataTable sorgularini saglar.
// sistemdeki gorevi: INotificationRepository sozlesmesinin EfExtendedRepository tabanli provider uygulamasidir.
// mirastan gelen: CRUD, filtreli liste, sayim, kullanim kontrolu ve bulk yazma yuzeyleri.
// burada yazilan: yok; taban disinda sorgu istenmez.
[ExposeServices(typeof(INotificationRepository))]
public class EfCoreNotificationRepository
    : EfExtendedRepository<LogManagementDbContext, Notification, Guid>,
        INotificationRepository
{
    // DbContext provider'ini extended repository tabanina devreder.
    public EfCoreNotificationRepository(
        IDbContextProvider<LogManagementDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
