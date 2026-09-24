using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Byd.LogManagement.Dtos.Notifications;
using Byd.LogManagement.Managers.Notifications;
using Byd.LogManagement.Mappers.Notifications;
using Volo.Abp.Users;

namespace Byd.LogManagement.Services.Notifications;

/// <summary>
/// Oturumdaki kullanicinin mobil token orkestrasyonunu yurutur.
/// </summary>
// islevi: Firebase projesini is koduyla cozer, kullaniciyi oturumdan alir, kayit, silme ve izin okuma kararlarini
// Manager'a devreder.
// sistemdeki gorevi: Controller ile UserMobileToken Manager zinciri arasindaki tek public giris noktasidir.
public class UserMobileTokenAppService : LogManagementAppService, IUserMobileTokenAppService
{
    private static readonly UserMobileTokenMapper Mapper = new();

    private UserMobileTokenManager Manager =>
        LazyGetRequiredService<UserMobileTokenManager>();

    private FirebaseProjectManager FirebaseProjectManager =>
        LazyGetRequiredService<FirebaseProjectManager>();

    private IValidator<RegisterUserMobileTokenDto> RegisterValidator =>
        LazyGetRequiredService<IValidator<RegisterUserMobileTokenDto>>();

    // Girdiyi dogrular, projeyi cozer ve mobil token'i oturumdaki kullanici adina kaydeder (varsa gunceller).
    public virtual async Task RegisterAsync(
        RegisterUserMobileTokenDto input,
        CancellationToken cancellationToken = default)
    {
        await RegisterValidator.ValidateAndThrowAsync(input, cancellationToken);
        var project = await FirebaseProjectManager.EnsureExistsByKeyAsync(input.FirebaseProjectKey, cancellationToken);
        var model = Mapper.Map(input, project.Id, CurrentUser.GetId());
        await Manager.RegisterAsync(model, cancellationToken);
    }

    // Projeyi cozer ve oturumdaki kullanicinin mobil token kaydini siler.
    public virtual async Task RemoveAsync(
        string firebaseProjectKey,
        CancellationToken cancellationToken = default)
    {
        var project = await FirebaseProjectManager.EnsureExistsByKeyAsync(firebaseProjectKey, cancellationToken);
        await Manager.RemoveAsync(CurrentUser.GetId(), project.Id, cancellationToken);
    }

    // Projeyi cozer ve oturumdaki kullanicinin bildirim aktiflik durumunu getirir.
    public virtual async Task<bool> GetPermitAsync(
        string firebaseProjectKey,
        CancellationToken cancellationToken = default)
    {
        var project = await FirebaseProjectManager.EnsureExistsByKeyAsync(firebaseProjectKey, cancellationToken);
        var token = await Manager.EnsureExistsAsync(CurrentUser.GetId(), project.Id, cancellationToken);
        return token.IsActive;
    }
}
