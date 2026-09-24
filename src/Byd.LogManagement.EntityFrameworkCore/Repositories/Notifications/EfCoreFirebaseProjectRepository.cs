using System;
using Byd.CoreExtensions.Repositories;
using Byd.LogManagement.Entities.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.LogManagement.EntityFrameworkCore.Repositories.Notifications;

/// <summary>
/// IFirebaseProjectRepository sozlesmesini EF Core DbContext'e baglar.
/// </summary>
// islevi: FirebaseProject icin generic, bulk ve DataTable sorgularini saglar.
// sistemdeki gorevi: IFirebaseProjectRepository sozlesmesinin EfExtendedRepository tabanli provider uygulamasidir.
// mirastan gelen: CRUD, filtreli liste, sayim, kullanim kontrolu ve bulk yazma yuzeyleri.
// burada yazilan: yok; taban disinda sorgu istenmez.
[ExposeServices(typeof(IFirebaseProjectRepository))]
public class EfCoreFirebaseProjectRepository(IDbContextProvider<LogManagementDbContext> dbContextProvider)
    : EfExtendedRepository<LogManagementDbContext, FirebaseProject, Guid>(dbContextProvider),
        IFirebaseProjectRepository;
