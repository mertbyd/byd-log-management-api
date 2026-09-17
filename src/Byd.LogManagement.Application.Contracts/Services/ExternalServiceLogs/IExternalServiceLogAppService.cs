using System.Collections.Generic;
using System.Threading.Tasks;
using Byd.CoreExtensions.Querying;
using Byd.LogManagement.Dtos.ExternalServiceLogs;
using Volo.Abp.Application.Services;

namespace Byd.LogManagement.Services.ExternalServiceLogs;

/// <summary>
/// Dis servis cagri kaydi okuma kullanim senaryolarini tanimlar.
/// </summary>
// islevi: Filtreli liste ve sayim sozlesmesini controller'a acar.
// sistemdeki gorevi: HttpApi katmaninin dis servis cagri kaydi okumasi icin bildigi tek servis sozlesmesidir.
public interface IExternalServiceLogAppService : IApplicationService
{
    // Filtre, siralama ve sayfalama uygulanmis dis servis cagri kaydi listesini getirir.
    Task<List<ExternalServiceLogDto>> GetFilteredListAsync(DataTableListInput<ExternalServiceLogFilter> input);

    // Ayni filtreye uyan toplam kayit sayisini getirir.
    Task<int> GetFilteredCountAsync(DataTableListInput<ExternalServiceLogFilter> input);
}
