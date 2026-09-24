using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Services.Notifications;
using SystemStandards.Results;

namespace Byd.LogManagement.Controllers.Notifications;

/// <summary>
/// Oturum kullanicisinin bildirim islemlerini HTTP uzerinden sunar.
/// </summary>
// islevi: Route ve request binding metadatasini tasiyip istegi tek AppService cagrisina yonlendirir.
// sistemdeki gorevi: Bildirim okundu durumu dikey kesitinin ince HTTP giris noktasidir.
[ApiController]
[Authorize]
[Route(NotificationRoutes.Root)]
public class NotificationController : LogManagementController
{
    /// <summary>Notification AppService'ini lazy cozer.</summary>
    private INotificationAppService AppService =>
        LazyGetRequiredService<INotificationAppService>();

    /// <summary>
    /// Oturum kullanicisina ait bildirimin okundu durumunu degistirir.
    /// </summary>
    /// <param name="id">Guncellenecek bildirimin kimligi.</param>
    /// <param name="isRead">Bildirimin yeni okundu durumu.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <returns>Iceriksiz basari cevabi.</returns>
    /// <remarks>
    /// Yalnizca oturum kullanicisina ait bildirimi gunceller; cevap govdesi dondurmez.
    /// </remarks>
    [HttpPut(NotificationRoutes.ReadStatus)]
    public virtual async Task<Result> UpdateReadStatusAsync(
        Guid id,
        bool isRead,
        CancellationToken cancellationToken = default)
    {
        await AppService.UpdateReadStatusAsync(id, isRead, cancellationToken);
        return Result.NoContent();
    }
}
