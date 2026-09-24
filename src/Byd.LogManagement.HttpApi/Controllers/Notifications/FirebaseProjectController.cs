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
    /// Response value { Id, Name, Key, Environment, CreationTime }. Kayit istegin tenant'inda aranir; baska tenant'in
    /// projesi bulunamadi olarak doner. FCM secret'i bu endpointte okunmaz.
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
    /// Response value elemanlari { Id, Name, Key, Environment, CreationTime } alanlarini tasir. Liste sayfalanmaz;
    /// tenant'ta proje yoksa bos liste doner.
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
    /// Request { Name, Key }. Calisma ortami sunucudan damgalanir. Log modulu girdiyi dogrular, ad ve anahtarin tenant ile ortam icinde
    /// tekilligini denetler ve kalici kaydi olusturur. FCM secret'i bu endpointte yuklenmez.
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
    /// Request { Name, Key }. Calisma ortami sunucudan damgalanir. Log modulu kaydin varligini ve ad ile anahtarin tenant ve ortam icinde
    /// tekilligini denetler. Response value guncel ortam bilgisini de tasir.
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
    /// Kayit istegin tenant'inda aranir ve kalici olarak silinir; projenin secret dosyalarini dosya modulu olay
    /// uzerinden arka planda siler. Basarili istekte iceriksiz cevap dondurulur.
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
    /// Request { FileName, Data }; Data JSON'da base64 olarak gonderilir. Dosya, dosya modulunde projenin
    /// kimligiyle FCM secret kategorisine yazilir; FirebaseProject kimligi ortama ozel oldugu icin secret da ayni
    /// ortam kapsamina baglanir. Onceki secret yenisi yuklendikten sonra silinir.
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
