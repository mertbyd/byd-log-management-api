using System;
using Byd.CoreExtensions.Repositories;
using Byd.LogManagement.Entities.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.LogManagement.EntityFrameworkCore.Repositories.Notifications;

/// <summary>
/// INotificationChannelRepository sozlesmesini EF Core DbContext'e baglar.
/// </summary>
// islevi: NotificationChannel icin generic, bulk ve DataTable sorgularini saglar.
// sistemdeki gorevi: INotificationChannelRepository sozlesmesinin EfExtendedRepository tabanli provider uygulamasidir.
// mirastan gelen: CRUD, filtreli liste, sayim, kullanim kontrolu ve bulk yazma yuzeyleri.
// burada yazilan: yok; taban disinda sorgu istenmez.
[ExposeServices(typeof(INotificationChannelRepository))]
public class EfCoreNotificationChannelRepository
    : EfExtendedRepository<LogManagementDbContext, NotificationChannel, Guid>,
        INotificationChannelRepository
{
    // DbContext provider'ini extended repository tabanina devreder.
    public EfCoreNotificationChannelRepository(
        IDbContextProvider<LogManagementDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
