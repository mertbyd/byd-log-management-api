using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Dtos.Notifications;
using Byd.LogManagement.Services.Notifications;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.LogManagement.Controllers.Notifications;

/// <summary>
/// Oturum kullanicisinin bildirim islemlerini HTTP uzerinden sunar.
/// </summary>
// islevi: Route ve request binding metadatasini tasiyip her istegi tek AppService cagrisina yonlendirir.
// sistemdeki gorevi: Kullanicinin bildirim listesi ve okundu durumu dikey kesitinin ince HTTP giris noktasidir.
[ApiController]
[Authorize]
[Route(NotificationRoutes.Root)]
public class NotificationController : LogManagementController
{
    /// <summary>Notification AppService'ini lazy cozer.</summary>
    private INotificationAppService AppService =>
        LazyGetRequiredService<INotificationAppService>();

    /// <summary>
    /// Oturum kullanicisinin gonderilmis bildirimlerini sayfali getirir.
    /// </summary>
    /// <param name="input">Atlanacak kayit sayisi ve sayfa buyuklugu.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <returns>Toplam kayit sayisi ve sayfadaki bildirimler.</returns>
    /// <remarks>
    /// Response value, eslesen toplam kayit sayisini ve sayfadaki bildirimleri tasir; her eleman Id, baslik,
    /// istegin dilinde secilmis metin, okundu durumu ve gonderim anini icerir. Yalnizca oturum kullanicisina
    /// yazilmis ve gonderimi tamamlanmis satirlar doner; kuyrukta bekleyen ve tenant geneline yazilan satirlar
    /// bu listede yer almaz. Bu endpoint okundu durumunu degistirmez.
    /// </remarks>
    [HttpGet(NotificationRoutes.List)]
    public virtual async Task<Result<PagedResultDto<NotificationDto>>> GetListAsync(
        [FromQuery] PagedResultRequestDto input,
        CancellationToken cancellationToken = default)
    {
        var result = await AppService.GetListAsync(input, cancellationToken);
        return result;
    }

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
