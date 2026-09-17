using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Byd.CoreExtensions.Querying;
using Byd.LogManagement.Dtos.ExternalServiceLogs;
using Byd.LogManagement.Interfaces.ExternalServiceLogs;
using Byd.LogManagement.Mappers.ExternalServiceLogs;

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

    // Liste girdisini dogrular ve filtreye uyan dis servis cagri kayitlarini getirir.
    public virtual async Task<List<ExternalServiceLogDto>> GetFilteredListAsync(
        DataTableListInput<ExternalServiceLogFilter> input)
    {
        var cancellationToken = CancellationTokenProvider.Token;
        await ListValidator.ValidateAndThrowAsync(input, cancellationToken);
        var entities = await Repository.GetFilteredListAsync(input, cancellationToken);
        return [.. entities.Select(Mapper.Map)];
    }

    // Liste girdisini dogrular ve ayni filtreye uyan kayit sayisini dondurur.
    public virtual async Task<int> GetFilteredCountAsync(DataTableListInput<ExternalServiceLogFilter> input)
    {
        var cancellationToken = CancellationTokenProvider.Token;
        await ListValidator.ValidateAndThrowAsync(input, cancellationToken);
        return await Repository.GetFilteredCountAsync(input, cancellationToken);
    }
}
