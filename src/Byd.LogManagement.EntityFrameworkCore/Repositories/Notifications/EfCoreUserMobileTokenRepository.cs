using System;
using Byd.CoreExtensions.Repositories;
using Byd.LogManagement.Entities.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Byd.LogManagement.EntityFrameworkCore.Repositories.Notifications;

/// <summary>
/// IUserMobileTokenRepository sozlesmesini EF Core DbContext'e baglar.
/// </summary>
// islevi: UserMobileToken icin generic, bulk ve DataTable sorgularini saglar.
// sistemdeki gorevi: IUserMobileTokenRepository sozlesmesinin EfExtendedRepository tabanli provider uygulamasidir.
// mirastan gelen: CRUD, filtreli liste, sayim, kullanim kontrolu ve bulk yazma yuzeyleri.
// burada yazilan: yok; taban disinda sorgu istenmez.
[ExposeServices(typeof(IUserMobileTokenRepository))]
public class EfCoreUserMobileTokenRepository(IDbContextProvider<LogManagementDbContext> dbContextProvider)
    : EfExtendedRepository<LogManagementDbContext, UserMobileToken, Guid>(dbContextProvider),
        IUserMobileTokenRepository;
