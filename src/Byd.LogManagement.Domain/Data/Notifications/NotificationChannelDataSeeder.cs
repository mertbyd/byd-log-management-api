using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Interfaces.Notifications;
using Byd.LogManagement.Managers.Notifications;
using Byd.LogManagement.Models.Notifications;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace Byd.LogManagement.Data.Notifications;

/// <summary>
/// Bildirim kanali lookup satirlarini idempotent olarak olusturur.
/// </summary>
// islevi: NotificationConsts'taki kanal kodlarindan tabloda olmayanlari Manager'a ayirtir ve tek yazmada kalici hale
// getirir.
// sistemdeki gorevi: Bildirim akisinin kanal kodunu kimlige cevirebilmesi icin web ve mobil kanal satirlarinin her
// veritabaninda bulunmasini saglar; mevcut satirlara dokunmaz.
public class NotificationChannelDataSeeder(
    NotificationChannelManager manager,
    INotificationChannelRepository repository) : IDataSeedContributor, ITransientDependency
{
    // Sabitlerdeki kanallardan eksik olanlari tek turda ekler. Eksigi ayirmak Manager'in, yazmak repository'nin
    // isidir; kanal basina ayri sorgu ve ayri yazma yapilmaz.
    [UnitOfWork]
    public async Task SeedAsync(DataSeedContext context)
    {
        List<NotificationChannelCreateModel> models =
        [
            .. NotificationConsts.ChannelNames.Select(channel => new NotificationChannelCreateModel
            {
                Code = channel.Key,
                Name = channel.Value
            })
        ];

        var missing = await manager.CreateMissingAsync(models);
        if (missing.Count == 0)
        {
            return;
        }

        await repository.InsertManyAsync(missing, autoSave: true);
    }
}
