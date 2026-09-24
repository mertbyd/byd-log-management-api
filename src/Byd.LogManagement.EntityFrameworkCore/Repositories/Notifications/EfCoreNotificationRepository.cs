using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
// burada yazilan: alicinin gonderilmis bildirimlerini sayfalayan liste ve sayim sorgulari; taban sorgusu
// yuklem alan sayfalama sunmadigi icin ikisi de burada yazilir.
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

    // Alicinin gonderilmis bildirimlerini en yeni gonderim once gelecek sekilde sayfalar.
    public async Task<List<Notification>> GetSentListByRecipientAsync(
        Guid recipientUserId,
        int skipCount,
        int maxResultCount,
        CancellationToken cancellationToken = default)
    {
        var queryable = await GetQueryableAsync();
        var query = SentOf(queryable, recipientUserId)
            .OrderByDescending(notification => notification.SentTime)
            .Skip(skipCount)
            .Take(maxResultCount);
        return await AsyncExecuter.ToListAsync(query, cancellationToken);
    }

    // Alicinin gonderilmis bildirimlerini sayfalamadan sayar.
    public async Task<int> GetSentCountByRecipientAsync(
        Guid recipientUserId,
        CancellationToken cancellationToken = default)
    {
        var queryable = await GetQueryableAsync();
        return await AsyncExecuter.CountAsync(SentOf(queryable, recipientUserId), cancellationToken);
    }

    // Aliciya yazilmis ve gonderimi tamamlanmis satirlari secer; kuyrukta bekleyen satir listede gorunmez.
    private static IQueryable<Notification> SentOf(IQueryable<Notification> queryable, Guid recipientUserId)
        => queryable.Where(notification =>
            notification.RecipientUserId == recipientUserId && notification.SentTime != null);
}
