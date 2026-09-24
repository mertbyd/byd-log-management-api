using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Managers;
using Byd.FileManagement.Entities.FileCategories;
using Byd.FileManagement.Managers.FileCategories;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Entities.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Byd.LogManagement.Models.Notifications;

namespace Byd.LogManagement.Managers.Notifications;

/// <summary>
/// Firebase projesi normalizasyon, varlik ve mutasyon kurallarini yonetir.
/// </summary>
// islevi: Proje adini kanoniklestirir, ad ve kod tekilligini tenant icinde dogrular, entity kurar, alanlarini degistirir
// ve yuklenen secret dosyalari arasindan guncel olani secer.
// sistemdeki gorevi: AppService'i is kararindan ve data-shell entity'yi davranistan uzak tutan domain sahibidir;
// projenin FCM secret dosyasinin dosya modulundeki kategorisini de o cozer.
public class FirebaseProjectManager(
    IFirebaseProjectRepository repository,
    FileCategoryManager fileCategoryManager) : BaseManager<FirebaseProject, Guid>(repository)
{
    // Kimligi verilen projeyi getirir; yoksa BaseManager lokalize NotFound firlatir.
    public virtual async Task<FirebaseProject> EnsureExistsAsync(Guid id, CancellationToken cancellationToken = default)
        => (await TryGetByIdAsync(id, throwIfNotExists: true, cancellationToken))!;

    // Benzersiz anahtariyla projeyi istegin tenant'inda getirir; yoksa lokalize NotFound firlatir.
    public virtual async Task<FirebaseProject> EnsureExistsByKeyAsync(string key, CancellationToken cancellationToken = default)
    {
        var normalizedKey = NormalizeRequiredKey(key);
        return (await TryGetByAsync(project => project.Key == normalizedKey, cancellationToken: cancellationToken))!;
    }

    // FCM secret dosyalarinin kategorisini getirir; seed calismadiysa lokalize NotFound firlatir.
    public virtual async Task<FileCategory> GetSecretCategoryAsync(CancellationToken cancellationToken = default)
        => (await fileCategoryManager.TryGetByAsync(
            category => category.Name == NotificationConsts.FirebaseSecretFileCategoryName,
            cancellationToken: cancellationToken))!;

    // Secret dosyalari arasindan en son yukleneni secer; proje henuz secret tasimiyorsa bos doner.
    public virtual FirebaseProjectSecretModel? FindLatestSecret(
        IReadOnlyCollection<FirebaseProjectSecretModel> secrets)
    {
        ArgumentNullException.ThrowIfNull(secrets);
        return secrets.MaxBy(secret => secret.CreationTime);
    }

    // Create modelini normalize edip istegin tenant'inda yeni FirebaseProject entity'sine donusturur.
    public virtual async Task<FirebaseProject> CreateAsync(
        FirebaseProjectCreateModel model,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        // Kapsam cagirandan alinmaz; tenant ortam baglamindan okunur.
        var entity = new FirebaseProject(GuidGenerator.Create())
        {
            TenantId = CurrentTenant.Id
        };
        await ApplyAsync(entity, model, cancellationToken);

        return entity;
    }

    // Update modelini normalize edip mevcut FirebaseProject alanlarina uygular.
    public virtual async Task<FirebaseProject> UpdateAsync(
        FirebaseProject entity,
        FirebaseProjectUpdateModel model,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(model);
        await ApplyAsync(entity, model, cancellationToken);

        return entity;
    }

    // Dogrulanmis input alanlarini FirebaseProject veri kabuguna uygular.
    // Anahtar, uretici bildirimin projeyi adresledigi anahtardir: ayni anahtar ikinci kez acilirsa bildirim hangi
    // projenin FCM hesabiyla gidecegini bilemez; ad da listede ayirt edilebilmek icin tektir. Tenant filtresi
    // acik oldugundan iki kontrol de istegin tenant'i icinde calisir.
    private async Task ApplyAsync(
        FirebaseProject entity,
        FirebaseProjectInputModel model,
        CancellationToken cancellationToken = default)
    {
        var name = NormalizeRequiredText(model.Name);
        var key = NormalizeRequiredKey(model.Key);
        await CheckAlreadyExistsAsync(other => other.Name == name, entity.Id, cancellationToken);
        await CheckAlreadyExistsAsync(other => other.Key == key, entity.Id, cancellationToken);
        entity.Name = name;
        entity.Key = key;
    }

    // Proje anahtarini kucuk harf ve bosluklardan arindirilmis kanonik bicime donusturur.
    private static string NormalizeRequiredKey(string key)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        return key.Trim().ToLowerInvariant();
    }
}
