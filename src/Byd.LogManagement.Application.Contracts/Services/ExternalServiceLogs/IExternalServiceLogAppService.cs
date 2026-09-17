using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Querying;
using Byd.LogManagement.Dtos.ExternalServiceLogs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Byd.LogManagement.Services.ExternalServiceLogs;

/// <summary>
/// Dis servis cagri kaydi okuma kullanim senaryolarini tanimlar.
/// </summary>
// islevi: Filtreli sayfa ve toplam kayit sayisini tek sozlesmeyle controller'a acar.
// sistemdeki gorevi: HttpApi katmaninin dis servis cagri kaydi okumasi icin bildigi tek servis sozlesmesidir.
public interface IExternalServiceLogAppService : IApplicationService
{
    // Filtre, siralama ve sayfalama uygulanmis dis servis cagri kayitlariyla toplam sayiyi getirir.
    Task<PagedResultDto<ExternalServiceLogDto>> GetFilteredListAsync(
        DataTableListInput<ExternalServiceLogFilter> input,
        CancellationToken cancellationToken=default);

    // Ayni correlation kimligine bagli dis servis cagrilarini getirir.
    Task<List<ExternalServiceLogDto>> GetListByCorrelationIdAsync(
        string correlationId,
        CancellationToken cancellationToken=default);
}
