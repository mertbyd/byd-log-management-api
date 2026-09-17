using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Byd.CoreExtensions.Querying;
using Byd.LogManagement.Dtos.ExternalServiceLogs;
using Byd.LogManagement.Interfaces.ExternalServiceLogs;
using Byd.LogManagement.Mappers.ExternalServiceLogs;
using Volo.Abp.Application.Dtos;

namespace Byd.LogManagement.Services.ExternalServiceLogs;

/// <summary>
/// Dis servis cagri kaydi okuma orkestrasyonunu yurutur.
/// </summary>
// islevi: Liste girdisini dogrular, repository dinamik sorgusunu calistirir ve sonucu DTO'ya esler.
// sistemdeki gorevi: Controller ile ExternalServiceLog repository zinciri arasindaki tek public giris noktasidir.
public class ExternalServiceLogAppService : LogManagementAppService, IExternalServiceLogAppService
{
    private static readonly ExternalServiceLogMapper Mapper = new();

    private IExternalServiceLogRepository Repository =>
        LazyGetRequiredService<IExternalServiceLogRepository>();

    private IValidator<DataTableListInput<ExternalServiceLogFilter>> ListValidator =>
        LazyGetRequiredService<IValidator<DataTableListInput<ExternalServiceLogFilter>>>();

    // Liste girdisini dogrular, filtreli sayfayi ve toplam sayiyi tek sonuc olarak getirir.
    public virtual async Task<PagedResultDto<ExternalServiceLogDto>> GetFilteredListAsync(
        DataTableListInput<ExternalServiceLogFilter> input,
        CancellationToken cancellationToken)
    {
        await ListValidator.ValidateAndThrowAsync(input, cancellationToken);
        var entities = await Repository.GetFilteredListAsync(input, cancellationToken);
        var totalCount = await Repository.GetFilteredCountAsync(input, cancellationToken);
        var items = entities.Select(Mapper.Map).ToList();
        return new PagedResultDto<ExternalServiceLogDto>(totalCount, items);
    }

    // Ayni correlation kimligine bagli dis servis cagrilarini DTO listesine esler.
    public virtual async Task<List<ExternalServiceLogDto>> GetListByCorrelationIdAsync(
        string correlationId,
        CancellationToken cancellationToken)
    {
        var entities = await Repository.GetListByCorrelationIdAsync(correlationId, cancellationToken);
        return entities.Select(Mapper.Map).ToList();
    }
}
