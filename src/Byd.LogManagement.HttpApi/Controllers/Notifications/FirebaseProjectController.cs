using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Dtos.Notifications;
using Byd.LogManagement.Permissions;
using Byd.LogManagement.Services.Notifications;
using SystemStandards.Results;

namespace Byd.LogManagement.Controllers.Notifications;

/// <summary>
/// Firebase projesi CRUD islemlerini HTTP uzerinden sunar.
/// </summary>
// islevi: Route ve request binding metadatasini tasiyip her istegi tek AppService cagrisina yonlendirir.
// sistemdeki gorevi: FirebaseProject tam CRUD dikey kesitinin ince HTTP giris noktasidir.
[ApiController]
[Route(FirebaseProjectRoutes.Root)]
public class FirebaseProjectController : LogManagementController
{
    /// <summary>FirebaseProject AppService'ini lazy cozer.</summary>
    private IFirebaseProjectAppService AppService =>
        LazyGetRequiredService<IFirebaseProjectAppService>();

    /// <summary>
    /// Kimligi verilen Firebase projesini getirir.
    /// </summary>
    /// <param name="id">Getirilecek FirebaseProject kaydinin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response {
    ///   Id           (Guid)     → Kayit Id'si
    ///   Name         (string)   → Proje adi
    ///   Key          (string)   → Projenin is kodu; bildirim isteklerinde kullanilir
    ///   Environment  (string)   → Projenin bagli oldugu calisma ortami
    ///   CreationTime (DateTime) → Olusturulma zamani
    /// }
    /// Kayit istegin tenant'inda aranir; baska tenant'in projesi bulunamadi olarak doner. FCM secret'i bu endpointte okunmaz.
    /// </remarks>
    [HttpGet(FirebaseProjectRoutes.ById)]
    [Authorize(LogManagementPermissions.FirebaseProjects.View)]
    public virtual async Task<Result<FirebaseProjectDto>> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default)
        => await AppService.GetAsync(id, cancellationToken);

    /// <summary>
    /// Istegin tenant'indaki Firebase projelerinin tamamini getirir.
    /// </summary>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response elemanlari tek kayit ucuyla ayni alanlari tasir. Liste sayfalanmaz; tenant'ta proje yoksa bos liste doner.
    /// </remarks>
    [HttpGet]
    [Authorize(LogManagementPermissions.FirebaseProjects.View)]
    public virtual async Task<Result<List<FirebaseProjectDto>>> GetListAsync(
        CancellationToken cancellationToken = default)
        => await AppService.GetListAsync(cancellationToken);

    /// <summary>
    /// Istegin tenant'inda yeni bir Firebase projesi olusturur.
    /// </summary>
    /// <param name="input">Proje adi, anahtari ve calisma ortami.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   Name (string) → Proje adi; tenant ve ortam icinde tekil
    ///   Key  (string) → Projenin is kodu; tenant ve ortam icinde tekil
    /// }
    /// Proje istegi yapan kullanicinin tenant'inda acilir; calisma ortami sunucudan damgalanir.
    /// Response tek kayit ucuyla ayni alanlari tasir. FCM secret'i bu endpointte yuklenmez.
    /// </remarks>
    [HttpPost]
    [Authorize(LogManagementPermissions.FirebaseProjects.Manage)]
    public virtual async Task<Result<FirebaseProjectDto>> CreateAsync(
        [FromBody] CreateFirebaseProjectDto input,
        CancellationToken cancellationToken = default)
        => await AppService.CreateAsync(input, cancellationToken);

    /// <summary>
    /// Kimligi verilen Firebase projesinin adini, anahtarini ve calisma ortamini gunceller.
    /// </summary>
    /// <param name="id">Guncellenecek FirebaseProject kaydinin kimligi.</param>
    /// <param name="input">Yeni proje adi, anahtari ve calisma ortami.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request olusturma istegiyle ayni alanlari tasir; calisma ortami sunucudan damgalanir. Log modulu kaydin varligini ve ad ile
    /// anahtarin tenant ve ortam icinde tekilligini denetler. Response guncel kaydi tek kayit ucuyla ayni alanlarla tasir.
    /// </remarks>
    [HttpPut(FirebaseProjectRoutes.ById)]
    [Authorize(LogManagementPermissions.FirebaseProjects.Manage)]
    public virtual async Task<Result<FirebaseProjectDto>> UpdateAsync(
        Guid id,
        [FromBody] UpdateFirebaseProjectDto input,
        CancellationToken cancellationToken = default)
        => await AppService.UpdateAsync(id, input, cancellationToken);

    /// <summary>
    /// Kimligi verilen Firebase projesini siler.
    /// </summary>
    /// <param name="id">Silinecek FirebaseProject kaydinin kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Kayit istegin tenant'inda aranir ve kalici olarak silinir; projenin secret dosyalarini dosya modulu olay uzerinden
    /// arka planda siler. Basarili istekte iceriksiz cevap dondurulur.
    /// </remarks>
    [HttpDelete(FirebaseProjectRoutes.ById)]
    [Authorize(LogManagementPermissions.FirebaseProjects.Manage)]
    public virtual async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await AppService.DeleteAsync(id, cancellationToken);
        return Result.NoContent();
    }

    /// <summary>
    /// Kimligi verilen Firebase projesinin FCM servis hesabi dosyasini yukler.
    /// </summary>
    /// <param name="id">Secret'i yuklenecek FirebaseProject kaydinin kimligi.</param>
    /// <param name="input">Dosya adi ve base64 kodlu dosya icerigi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   FileName (string) → Servis hesabi dosyasinin adi
    ///   Data     (byte[]) → Dosya icerigi; JSON'da base64
    /// }
    /// Dosya, dosya modulunde projenin kimligiyle FCM secret kategorisine ve istegi yapan kullanicinin tenant'ina yazilir.
    /// Onceki secret yenisi yuklendikten sonra silinir. Basarili istekte iceriksiz cevap dondurulur.
    /// </remarks>
    [HttpPost(FirebaseProjectRoutes.Secret)]
    [Authorize(LogManagementPermissions.FirebaseProjects.Manage)]
    public virtual async Task<Result> UploadSecretAsync(
        Guid id,
        [FromBody] UploadFirebaseProjectSecretDto input,
        CancellationToken cancellationToken = default)
    {
        await AppService.UploadSecretAsync(id, input, cancellationToken);
        return Result.NoContent();
    }
}
