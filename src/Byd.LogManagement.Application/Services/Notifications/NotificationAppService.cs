using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.LogManagement.Managers.Notifications;
using Volo.Abp.Users;

namespace Byd.LogManagement.Services.Notifications;

/// <summary>
/// Oturum kullanicisinin bildirim islemlerini orkestre eder.
/// </summary>
// islevi: Oturum kullanicisini Manager'in okundu durumu mutasyonuna tasir.
// sistemdeki gorevi: Controller ile NotificationManager arasindaki public kullanim senaryosu siniridir.
public class NotificationAppService : LogManagementAppService, INotificationAppService
{
    private NotificationManager Manager =>
        LazyGetRequiredService<NotificationManager>();

    // Oturum kullanicisina ait bildirimin okundu durumunu Manager uzerinden degistirir.
    public virtual async Task UpdateReadStatusAsync(
        Guid id,
        bool isRead,
        CancellationToken cancellationToken = default)
    {
        await Manager.UpdateReadStatusAsync(id, CurrentUser.GetId(), isRead, cancellationToken);
    }
}
