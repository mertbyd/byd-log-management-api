using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Byd.LogManagement.Services.Notifications;

/// <summary>
/// Oturum kullanicisinin bildirim islemlerini tanimlar.
/// </summary>
// islevi: Bildirimin okundu durumunu degistiren public kullanim senaryosunu acar.
// sistemdeki gorevi: NotificationController ile Application implementasyonu arasindaki servis siniridir.
public interface INotificationAppService : IApplicationService
{
    // Oturum kullanicisina ait bildirimin okundu durumunu degistirir.
    Task UpdateReadStatusAsync(
        Guid id,
        bool isRead,
        CancellationToken cancellationToken = default);
}
