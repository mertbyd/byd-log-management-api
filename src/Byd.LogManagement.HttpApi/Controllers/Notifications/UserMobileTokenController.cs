using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Dtos.Notifications;
using Byd.LogManagement.Services.Notifications;
using SystemStandards.Results;

namespace Byd.LogManagement.Controllers.Notifications;

/// <summary>
/// Oturumdaki kullanicinin mobil token islemlerini HTTP uzerinden sunar.
/// </summary>
// islevi: Route ve request binding metadatasini tasiyip her istegi tek AppService cagrisina yonlendirir.
// sistemdeki gorevi: Mobil uygulamanin kullanici token'ini bildirim servisine kaydettigi ince HTTP giris noktasidir;
// yalniz oturum acmis kullanici kendi token'i icin cagirir, ozel yetki istemez.
[ApiController]
[Authorize]
[Route(UserMobileTokenRoutes.Root)]
public class UserMobileTokenController : LogManagementController
{
    /// <summary>UserMobileToken AppService'ini lazy cozer.</summary>
    private IUserMobileTokenAppService AppService =>
        LazyGetRequiredService<IUserMobileTokenAppService>();

    /// <summary>
    /// Oturumdaki kullanicinin mobil push bildirim token'ini kaydeder veya gunceller.
    /// </summary>
    /// <param name="input">Firebase proje anahtari, mobil kayit token'i ve aktiflik durumu.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request { FirebaseProjectKey, Token, IsActive }; Token FCM tarafindan uretilen kayit token'idir.
    /// Kullanici kimligi oturumdan alinir, istekte gonderilmez. Ayni kullanicinin o projede mevcut bir kaydi varsa
    /// yeni satir acilmaz; token ve aktiflik durumu guncellenir (upsert). Basarili istekte iceriksiz cevap dondurulur.
    /// </remarks>
    [HttpPost]
    public virtual async Task<Result> RegisterAsync(
        [FromBody] RegisterUserMobileTokenDto input,
        CancellationToken cancellationToken = default)
    {
        await AppService.RegisterAsync(input, cancellationToken);
        return Result.NoContent();
    }

    /// <summary>
    /// Oturumdaki kullanicinin ilgili projedeki mobil token kaydini siler.
    /// </summary>
    /// <param name="firebaseProjectKey">Bagli olunan Firebase projesinin benzersiz anahtari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Mobil uygulama oturum kapanirken cagirir; kullanici bu proje icin bildirim almayi birakir. Kayit yoksa hata
    /// vermez. Basarili istekte iceriksiz cevap dondurulur.
    /// </remarks>
    [HttpDelete]
    public virtual async Task<Result> RemoveAsync(
        [FromQuery] string firebaseProjectKey,
        CancellationToken cancellationToken = default)
    {
        await AppService.RemoveAsync(firebaseProjectKey, cancellationToken);
        return Result.NoContent();
    }

    /// <summary>
    /// Oturumdaki kullanicinin bildirim aktiflik durumunu getirir.
    /// </summary>
    /// <param name="firebaseProjectKey">Bagli olunan Firebase projesinin benzersiz anahtari.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value kullanicinin bildirim aktiflik durumunun acik olup olmadigini tasir. Kayitli token yoksa EntityNotFound
    /// hata koduyla reddedilir.
    /// </remarks>
    [HttpGet(UserMobileTokenRoutes.Permit)]
    public virtual async Task<Result<bool>> GetPermitAsync(
        [FromQuery] string firebaseProjectKey,
        CancellationToken cancellationToken = default)
        => await AppService.GetPermitAsync(firebaseProjectKey, cancellationToken);
}
