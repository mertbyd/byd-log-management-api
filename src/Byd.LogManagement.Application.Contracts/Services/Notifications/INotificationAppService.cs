using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.LogManagement.Dtos.Notifications;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Byd.LogManagement.Services.Notifications;

/// <summary>
/// Oturum kullanicisinin bildirim kullanim senaryolarini tanimlar.
/// </summary>
// islevi: Kullanicinin kendi bildirimlerini sayfalayan okuma ile okundu durumunu degistiren yazma senaryosunu acar.
// sistemdeki gorevi: NotificationController ile Application implementasyonu arasindaki servis siniridir.
public interface INotificationAppService : IApplicationService
{
    // Oturum kullanicisinin gonderilmis bildirimlerini toplam sayisiyla birlikte sayfali getirir.
    Task<PagedResultDto<NotificationDto>> GetListAsync(
        PagedResultRequestDto input,
        CancellationToken cancellationToken = default);

    // Oturum kullanicisina ait bildirimin okundu durumunu degistirir.
    Task UpdateReadStatusAsync(
        Guid id,
        bool isRead,
        CancellationToken cancellationToken = default);
}
