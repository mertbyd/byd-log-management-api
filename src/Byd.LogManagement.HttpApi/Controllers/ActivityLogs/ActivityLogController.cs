using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Dtos.ActivityLogs;
using Byd.LogManagement.Permissions;
using Byd.LogManagement.Services.ActivityLogs;
using SystemStandards.Results;

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
    /// <remarks>
    /// Response value elemanlari kayit Id, tenant, organizasyon, kullanici, is kaydi kimligi, uygulama adi,
    /// etkinlik turu, doldurulmus aciklama ve olusturma zamanini tasir. Eslesen kayit yoksa bos liste doner.
    /// Bu endpoint yeni etkinlik kaydi yazmaz.
    /// </remarks>
    [HttpPost(ActivityLogRoutes.FilteredList)]
    [Authorize(LogManagementPermissions.ActivityLogs.View)]
    public virtual async Task<Result<List<ActivityLogDto>>> GetFilteredList(
        [FromBody] DataTableListInput<ActivityLogFilter> input)
    {
        var result = await AppService.GetFilteredListAsync(input);
        return result;
    }

    /// <summary>
    /// Verilen filtrelere uyan etkinlik kayitlarinin toplam sayisini getirir.
    /// </summary>
    /// <param name="input">Sayimda kullanilacak etkinlik kaydi filtre bilgileri.</param>
    /// <remarks>
    /// Liste endpointiyle ayni filtre kurallari uygulanir; response value eslesen kayit sayisidir.
    /// </remarks>
    [HttpPost(ActivityLogRoutes.FilteredCount)]
    [Authorize(LogManagementPermissions.ActivityLogs.View)]
    public virtual async Task<Result<int>> GetFilteredCount(
        [FromBody] DataTableListInput<ActivityLogFilter> input)
    {
        var result = await AppService.GetFilteredCountAsync(input);
        return result;
    }
}
