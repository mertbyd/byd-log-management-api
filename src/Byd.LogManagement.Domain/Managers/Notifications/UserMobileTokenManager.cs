using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Byd.CoreExtensions.Managers;
using Byd.LogManagement.Entities.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Byd.LogManagement.Models.Notifications;
using Volo.Abp.Data;

namespace Byd.LogManagement.Managers.Notifications;

/// <summary>
/// Kullanici mobil token kaydinin varlik, kayit ve silme kurallarini yonetir.
/// </summary>
// islevi: Kullaniciyi (kullanici, Firebase projesi) ikilisiyle tekil adresler; yeni istek geldiginde
// satir acmak yerine kullanicinin token'ini ve aktiflik durumunu gunceller (upsert); gonderim icin aktif
// token'lari verir, gecersiz token'lari siler.
// sistemdeki gorevi: AppService'i upsert kararindan ve data-shell entity'yi davranistan uzak tutan domain sahibidir.
public class UserMobileTokenManager(
    IUserMobileTokenRepository repository,
    IDataFilter dataFilter)
    : BaseManager<UserMobileToken, Guid>(repository)
{
    // Kullanicinin token kaydini aktiflik durumundan bagimsiz getirir; yoksa lokalize NotFound firlatir.
    public virtual async Task<UserMobileToken> EnsureExistsAsync(
        Guid userId,
        Guid firebaseProjectId,
        CancellationToken cancellationToken = default)
    {
        using (dataFilter.Disable<IHasIsActive>())
        {
            return (await TryGetByAsync(Matches(userId, firebaseProjectId), cancellationToken: cancellationToken))!;
        }
    }

    // Kullanici kayitliysa (aktif/pasif fark etmeksizin) token ve aktiflik durumunu gunceller; degilse yeni kayit acar.
    public virtual async Task<UserMobileToken> RegisterAsync(
        UserMobileTokenRegisterModel model,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        UserMobileToken? entity;
        using (dataFilter.Disable<IHasIsActive>())
        {
            entity = await TryGetByAsync(
                Matches(model.UserId, model.FirebaseProjectId),
                throwIfNotExists: false,
                cancellationToken);
        }

        if (entity is not null)
        {
            Apply(entity, model);
            return await Repository.UpdateAsync(entity, autoSave: true, cancellationToken: cancellationToken);
        }

        entity = new UserMobileToken(GuidGenerator.Create())
        {
            FirebaseProjectId = model.FirebaseProjectId,
            UserId = model.UserId
        };
        Apply(entity, model);
        return await Repository.InsertAsync(entity, autoSave: true, cancellationToken: cancellationToken);
    }

    // Kullanicinin token kaydini aktiflik durumundan bagimsiz silip silinen kaydi dondurur; kayit yoksa bir sey yapmaz ve bos doner.
    public virtual async Task<UserMobileToken?> RemoveAsync(
        Guid userId,
        Guid firebaseProjectId,
        CancellationToken cancellationToken = default)
    {
        UserMobileToken? entity;
        using (dataFilter.Disable<IHasIsActive>())
        {
            entity = await TryGetByAsync(
                Matches(userId, firebaseProjectId),
                throwIfNotExists: false,
                cancellationToken);
        }

        if (entity is null)
            return null;

        await Repository.DeleteAsync(entity, autoSave: true, cancellationToken: cancellationToken);
        return entity;
    }

    // Projenin aktif kullanici token'larini getirir; kullanici listesi verilmezse projedeki butun kullanicilarinkini getirir.
    // IHasIsActive global veri filtresi sayesinde pasif token'lar sorgudan otomatik elenir.
    public virtual Task<List<UserMobileToken>> GetEnabledListAsync(
        Guid firebaseProjectId,
        IReadOnlyCollection<Guid>? userIds,
        CancellationToken cancellationToken = default)
        => TryGetListByAsync(
            token => token.FirebaseProjectId == firebaseProjectId
                     && token.IsActive
                     && (userIds == null || userIds.Contains(token.UserId)),
            cancellationToken: cancellationToken);

    // FCM'in gecersiz dedigi token'lari projenin defterinden siler; liste bossa bir sey yapmaz.
    public virtual async Task RemoveByTokensAsync(
        Guid firebaseProjectId,
        IEnumerable<string> tokens,
        CancellationToken cancellationToken = default)
    {
        List<string> tokenList = [.. tokens.Distinct()];
        if (tokenList.Count == 0)
            return;

        using (dataFilter.Disable<IHasIsActive>())
        {
            await Repository.DeleteAsync(
                token => token.FirebaseProjectId == firebaseProjectId && tokenList.Contains(token.Token),
                autoSave: true,
                cancellationToken: cancellationToken);
        }
    }

    // Kullaniciyi adresleyen (kullanici, proje) esitligini kurar.
    private static Expression<Func<UserMobileToken, bool>> Matches(
        Guid userId,
        Guid firebaseProjectId)
        => token => token.UserId == userId
                    && token.FirebaseProjectId == firebaseProjectId;

    // Degisebilen alanlari normalize ederek veri kabuguna uygular.
    private static void Apply(UserMobileToken entity, UserMobileTokenRegisterModel model)
    {
        entity.Token = NormalizeRequiredText(model.Token);
        entity.LanguageCode = NormalizeOptionalText(model.LanguageCode);
        entity.IsActive = model.IsActive;
    }
}
