using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Managers;
using Byd.LogManagement.Entities.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Byd.LogManagement.Models.Notifications;

namespace Byd.LogManagement.Managers.Notifications;

/// <summary>
/// Bildirim kanali olusturma ve kod tekilligi kuralini yonetir.
/// </summary>
// islevi: Verilen kanal kodlarindan tabloda bulunmayanlari ayirir ve data-shell entity'lerini kurar.
// sistemdeki gorevi: Seed akisini entity kurma ve tekillik kararindan uzak tutan domain sahibidir.
public class NotificationChannelManager(INotificationChannelRepository repository)
    : BaseManager<NotificationChannel, Guid>(repository)
{
    // Modellerden tabloda henuz bulunmayanlari entity'ye cevirir. Mevcut kodlar tek sorguda okunur; model basina
    // ayri varlik sorgusu atilmaz. Bulunan satira dokunulmadigi icin seed tekrar tekrar calistirilabilir.
    public virtual async Task<List<NotificationChannel>> CreateMissingAsync(
        IReadOnlyCollection<NotificationChannelCreateModel> models,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(models);
        List<int?> codes = [.. models.Select(model => (int?)model.Code)];
        var existing = await TryGetListByAsync(
            entity => codes.Contains(entity.Code),
            cancellationToken: cancellationToken);
        var existingCodes = existing.Select(entity => entity.Code).ToHashSet();

        return
        [
            .. models
                .Where(model => !existingCodes.Contains(model.Code))
                .Select(model => new NotificationChannel(GuidGenerator.Create())
                {
                    Name = model.Name,
                    Code = model.Code
                })
        ];
    }
}
