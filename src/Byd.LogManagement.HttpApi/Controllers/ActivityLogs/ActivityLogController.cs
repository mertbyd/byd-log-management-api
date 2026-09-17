using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Dtos.ActivityLogs;
using Byd.LogManagement.Permissions;
using Byd.LogManagement.Services.ActivityLogs;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

namespace Byd.LogManagement.Controllers.ActivityLogs;

/// <summary>
/// Merkezi etkinlik kayitlarini HTTP uzerinden okur.
/// </summary>
// islevi: Route ve request binding metadatasini tasiyip her istegi tek AppService cagrisina yonlendirir.
// sistemdeki gorevi: ActivityLog okuma kesitinin ince HTTP giris noktasidir.
[ApiController]
[Route(ActivityLogRoutes.Root)]
public class ActivityLogController : LogManagementController
{
    /// <summary>ActivityLog AppService'ini lazy cozer.</summary>
    private IActivityLogAppService AppService =>
        LazyGetRequiredService<IActivityLogAppService>();

    /// <summary>
    /// Filtre, siralama ve sayfalama uygulanmis etkinlik kayitlarini getirir.
    /// </summary>
    /// <param name="input">Etkinlik kaydi filtre, siralama ve sayfalama bilgileri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value, eslesen toplam kayit sayisini ve sayfadaki kayitlari tasir. Kayit elemanlari Id, tenant,
    /// organizasyon, kullanici, is kaydi kimligi, uygulama adi, etkinlik turu, doldurulmus aciklama ve olusturma
    /// zamanini icerir. Eslesen kayit yoksa toplam sayi sifir ve liste bos doner. Bu endpoint yeni kayit yazmaz.
    /// </remarks>
    [HttpPost(ActivityLogRoutes.FilteredList)]
    [Authorize(LogManagementPermissions.ActivityLogs.View)]
    public virtual async Task<Result<PagedResultDto<ActivityLogDto>>> GetFilteredListAsync(
        [FromBody] DataTableListInput<ActivityLogFilter> input,
        CancellationToken cancellationToken = default)
    {
        var result = await AppService.GetFilteredListAsync(input, cancellationToken);
        return result;
    }
}
