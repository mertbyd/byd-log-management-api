using System.Threading;
using System.Threading.Tasks;
using Byd.LogManagement.Dtos.Notifications;
using Volo.Abp.Application.Services;

namespace Byd.LogManagement.Services.Notifications;

/// <summary>
/// Oturumdaki kullanicinin mobil token orkestrasyon senaryolarini tanimlar.
/// </summary>
// islevi: Mobil token kaydetme, token kaydini silme ve token aktiflik durumunu okuma kontratini acar.
// sistemdeki gorevi: Controller ile Application implementasyonu arasindaki public servis siniridir.
public interface IUserMobileTokenAppService : IApplicationService
{
    // Mobil token kaydeder; kullanicinin mevcut kaydi varsa token ve aktiflik durumunu gunceller (upsert).
    Task RegisterAsync(RegisterUserMobileTokenDto input, CancellationToken cancellationToken = default);

    // Kullanicinin projedeki mobil token kaydini siler; kayit yoksa bir sey yapmaz.
    Task RemoveAsync(string firebaseProjectKey, CancellationToken cancellationToken = default);

    // Kullanicinin projedeki bildirim aktiflik durumunun acik olup olmadigini getirir.
    Task<bool> GetPermitAsync(string firebaseProjectKey, CancellationToken cancellationToken = default);
}
