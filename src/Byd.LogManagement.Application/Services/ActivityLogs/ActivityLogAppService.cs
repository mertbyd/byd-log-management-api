using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Byd.CoreExtensions.Querying;
using Byd.LogManagement.Dtos.ActivityLogs;
using Byd.LogManagement.Interfaces.ActivityLogs;
using Byd.LogManagement.Managers.ActivityLogs;
using Byd.LogManagement.Mappers.ActivityLogs;
using Volo.Abp.Application.Dtos;

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

    // Liste girdisini dogrular, filtreli sayfayi ve toplam sayiyi tek sonuc olarak getirir.
    public virtual async Task<PagedResultDto<ActivityLogDto>> GetFilteredListAsync(
        DataTableListInput<ActivityLogFilter> input,
        CancellationToken cancellationToken=default)
    {
        await ListValidator.ValidateAndThrowAsync(input, cancellationToken);
        var entities = await Repository.GetFilteredListAsync(input, cancellationToken);
        var totalCount = await Repository.GetFilteredCountAsync(input, cancellationToken);
        var items = entities.Select(entity => Mapper.Map(entity, Manager.Describe(entity))).ToList();
        return new PagedResultDto<ActivityLogDto>(totalCount, items);
    }
}
