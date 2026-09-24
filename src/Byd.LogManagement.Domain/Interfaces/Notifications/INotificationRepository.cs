using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Byd.LogManagement.Entities.Notifications;

namespace Byd.LogManagement.Interfaces.Notifications;

/// <summary>
/// Bildirim kaydi icin extended repository sozlesmesini tanimlar.
/// </summary>
// islevi: Ekleme, filtreli liste ve sayim yeteneklerini tabandan alir; alicinin gonderilmis bildirimlerini
// sayfalayan iki sorguyu kendisi tanimlar.
// sistemdeki gorevi: Bildirim kullanim senaryolari ile EF Core uygulamasi arasindaki veri erisim siniridir.
public interface INotificationRepository : IExtendedRepository<Notification>
{
    // Alicinin gonderilmis bildirimlerini en yeniden eskiye dogru sayfalar.
    Task<List<Notification>> GetSentListByRecipientAsync(
        Guid recipientUserId,
        int skipCount,
        int maxResultCount,
        CancellationToken cancellationToken = default);

    // Alicinin gonderilmis bildirimlerinin toplam sayisini verir.
    Task<int> GetSentCountByRecipientAsync(
        Guid recipientUserId,
        CancellationToken cancellationToken = default);
}
