using System.Collections.Generic;
using System.Threading.Tasks;
using Byd.CoreExtensions.Querying;
using Byd.LogManagement.Dtos.ActivityLogs;
using Volo.Abp.Application.Services;

namespace Byd.LogManagement.Services.ActivityLogs;

/// <summary>
/// Etkinlik kaydi okuma kullanim senaryolarini tanimlar.
/// </summary>
// islevi: Filtreli liste ve sayim sozlesmesini controller'a acar.
// sistemdeki gorevi: HttpApi katmaninin etkinlik kaydi okumasi icin bildigi tek servis sozlesmesidir.
public interface IActivityLogAppService : IApplicationService
{
    // Filtre, siralama ve sayfalama uygulanmis etkinlik kaydi listesini getirir.
    Task<List<ActivityLogDto>> GetFilteredListAsync(DataTableListInput<ActivityLogFilter> input);

    // Ayni filtreye uyan toplam kayit sayisini getirir.
    Task<int> GetFilteredCountAsync(DataTableListInput<ActivityLogFilter> input);
}
