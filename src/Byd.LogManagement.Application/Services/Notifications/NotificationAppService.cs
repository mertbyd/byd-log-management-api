using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Byd.LogManagement.Dtos.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Byd.LogManagement.Managers.Notifications;
using Byd.LogManagement.Mappers.Notifications;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;

namespace Byd.LogManagement.Services.Notifications;

/// <summary>
/// Oturum kullanicisinin bildirim islemlerini orkestre eder.
/// </summary>
// islevi: Oturum kullanicisinin sayfasini repository'den okur, metni Manager'a istek dilinde sectirir ve okundu
// durumu mutasyonunu Manager'a devreder.
// sistemdeki gorevi: Controller ile Notification Manager/Repository zinciri arasindaki tek public giris noktasidir.
public class NotificationAppService : LogManagementAppService, INotificationAppService
{
    private static readonly NotificationMapper Mapper = new();

    private NotificationManager Manager =>
        LazyGetRequiredService<NotificationManager>();

    private INotificationRepository Repository =>
        LazyGetRequiredService<INotificationRepository>();

    // Oturum kullanicisinin sayfasini ve toplam sayisini tek sonuc olarak getirir; metin istegin dilinde secilir.
    public virtual async Task<PagedResultDto<NotificationDto>> GetListAsync(
        PagedResultRequestDto input,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(input);
        var recipientUserId = CurrentUser.GetId();
        var entities = await Repository.GetSentListByRecipientAsync(
            recipientUserId,
            input.SkipCount,
            input.MaxResultCount,
            cancellationToken);
        var totalCount = await Repository.GetSentCountByRecipientAsync(recipientUserId, cancellationToken);
        var items = entities.Select(entity => Mapper.Map(entity, Manager.TextOf(entity))).ToList();
        return new PagedResultDto<NotificationDto>(totalCount, items);
    }

    // Oturum kullanicisina ait bildirimin okundu durumunu Manager uzerinden degistirir.
    public virtual async Task UpdateReadStatusAsync(
        Guid id,
        bool isRead,
        CancellationToken cancellationToken = default)
    {
        await Manager.UpdateReadStatusAsync(id, CurrentUser.GetId(), isRead, cancellationToken);
    }
}
