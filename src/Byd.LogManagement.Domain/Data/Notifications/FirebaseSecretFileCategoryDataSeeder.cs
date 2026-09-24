using System.Threading;
using System.Threading.Tasks;
using Byd.FileManagement.Interfaces.FileCategories;
using Byd.FileManagement.Managers.FileCategories;
using Byd.FileManagement.Models.FileCategories;
using Byd.LogManagement.Consts;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace Byd.LogManagement.Data.Notifications;

/// <summary>
/// FCM secret dosyalarinin dosya modulundeki kategorisini idempotent olarak olusturur.
/// </summary>
// islevi: Kategori dosya modulunde yoksa dosya modulunun Manager'i ile kurar ve repository uzerinden kalici hale getirir.
// sistemdeki gorevi: Dosya modulu kategori seed etmez, kategori listesinin sahibi tuketicidir; Firebase projesi secret
// yuklemesi bu kategori olmadan calismaz. Mevcut kategoriye dokunmaz.
public class FirebaseSecretFileCategoryDataSeeder(
    FileCategoryManager manager,
    IFileCategoryRepository repository) : IDataSeedContributor, ITransientDependency
{
    // Kategori yoksa sabitteki adla ekler; varsa hicbir sey yapmaz.
    [UnitOfWork]
    public async Task SeedAsync(DataSeedContext context)
    {
        var current = await repository.FindAsync(
            entity => entity.Name == NotificationConsts.FirebaseSecretFileCategoryName,
            cancellationToken: CancellationToken.None);
        if (current is not null)
            return;

        var model = new FileCategoryCreateModel { Name = NotificationConsts.FirebaseSecretFileCategoryName };
        var entity = await manager.CreateAsync(model, CancellationToken.None);
        await repository.InsertAsync(entity, autoSave: true, cancellationToken: CancellationToken.None);
    }
}
