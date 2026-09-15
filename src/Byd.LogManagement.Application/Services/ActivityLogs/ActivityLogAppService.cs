using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Byd.CoreExtensions.Querying;
using Byd.LogManagement.Dtos.ActivityLogs;
using Byd.LogManagement.Interfaces.ActivityLogs;
using Byd.LogManagement.Managers.ActivityLogs;
using Byd.LogManagement.Mappers.ActivityLogs;

namespace Byd.LogManagement.Services.ActivityLogs;

/// <summary>
/// Etkinlik kaydi okuma orkestrasyonunu yurutur.
/// </summary>
// islevi: Liste girdisini dogrular, repository dinamik sorgusunu calistirir, aciklamayi Manager'a kurdurur ve sonucu DTO'ya esler.
// sistemdeki gorevi: Controller ile ActivityLog Manager/Repository zinciri arasindaki tek public giris noktasidir.
public class ActivityLogAppService : LogManagementAppService, IActivityLogAppService
{
    private static readonly ActivityLogMapper Mapper = new();

    private ActivityLogManager Manager =>
        LazyGetRequiredService<ActivityLogManager>();

    private IActivityLogRepository Repository =>
        LazyGetRequiredService<IActivityLogRepository>();

    private IValidator<DataTableListInput<ActivityLogFilter>> ListValidator =>
        LazyGetRequiredService<IValidator<DataTableListInput<ActivityLogFilter>>>();

    // Liste girdisini dogrular, filtreye uyan satirlari getirir ve her birini istek dilindeki aciklamasiyla esler.
    public virtual async Task<List<ActivityLogDto>> GetFilteredListAsync(DataTableListInput<ActivityLogFilter> input)
    {
        var cancellationToken = CancellationTokenProvider.Token;
        await ListValidator.ValidateAndThrowAsync(input, cancellationToken);
        var entities = await Repository.GetFilteredListAsync(input, cancellationToken);
        return [.. entities.Select(entity => Mapper.Map(entity, Manager.Describe(entity)))];
    }

    // Liste girdisini dogrular ve ayni filtreye uyan kayit sayisini dondurur.
    public virtual async Task<int> GetFilteredCountAsync(DataTableListInput<ActivityLogFilter> input)
    {
        var cancellationToken = CancellationTokenProvider.Token;
        await ListValidator.ValidateAndThrowAsync(input, cancellationToken);
        return await Repository.GetFilteredCountAsync(input, cancellationToken);
    }
}
