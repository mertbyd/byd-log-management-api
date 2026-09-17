using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Querying;
using Byd.LogManagement.Dtos.ActivityLogs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Byd.LogManagement.Services.ActivityLogs;

/// <summary>
/// Etkinlik kaydi okuma kullanim senaryolarini tanimlar.
/// </summary>
// islevi: Filtreli sayfa ve toplam kayit sayisini tek sozlesmeyle controller'a acar.
// sistemdeki gorevi: HttpApi katmaninin etkinlik kaydi okumasi icin bildigi tek servis sozlesmesidir.
public interface IActivityLogAppService : IApplicationService
{
    // Filtre, siralama ve sayfalama uygulanmis etkinlik kayitlariyla toplam sayiyi getirir.
    Task<PagedResultDto<ActivityLogDto>> GetFilteredListAsync(
        DataTableListInput<ActivityLogFilter> input,
        CancellationToken cancellationToken=default);
}
