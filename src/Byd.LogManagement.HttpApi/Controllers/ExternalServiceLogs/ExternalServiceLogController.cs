using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Dtos.ExternalServiceLogs;
using Byd.LogManagement.Permissions;
using Byd.LogManagement.Services.ExternalServiceLogs;
using SystemStandards.Results;

namespace Byd.LogManagement.Controllers.ExternalServiceLogs;

/// <summary>
/// Merkezi dis servis cagri kayitlarini HTTP uzerinden okur.
/// </summary>
// islevi: Route ve request binding metadatasini tasiyip her istegi tek AppService cagrisina yonlendirir.
// sistemdeki gorevi: ExternalServiceLog okuma kesitinin ince HTTP giris noktasidir.
[ApiController]
[Route(ExternalServiceLogRoutes.Root)]
public class ExternalServiceLogController : LogManagementController
{
    /// <summary>ExternalServiceLog AppService'ini lazy cozer.</summary>
    private IExternalServiceLogAppService AppService =>
        LazyGetRequiredService<IExternalServiceLogAppService>();

    /// <summary>
    /// Filtre, siralama ve sayfalama uygulanmis dis servis cagri kayitlarini getirir.
    /// </summary>
    /// <param name="input">Dis servis cagri kaydi filtre, siralama ve sayfalama bilgileri.</param>
    /// <remarks>
    /// Response value elemanlari kayit Id, cagriyi baslatan istegin correlation kimligi, cagrilan dis servis
    /// saglayicisi, cagriyi yapan tuketici modul, cagrilan uc ve istek tipi, cagrinin baslama zamani ve suresi,
    /// donen HTTP durum kodu, varsa hata mesaji, ham yanit govdesi ve satirin yazildigi zamani tasir. Eslesen
    /// kayit yoksa bos liste doner. Bu endpoint yeni kayit yazmaz.
    /// </remarks>
    [HttpPost(ExternalServiceLogRoutes.FilteredList)]
    [Authorize(LogManagementPermissions.ExternalServiceLogs.View)]
    public virtual async Task<Result<List<ExternalServiceLogDto>>> GetFilteredList(
        [FromBody] DataTableListInput<ExternalServiceLogFilter> input)
    {
        var result = await AppService.GetFilteredListAsync(input);
        return result;
    }

    /// <summary>
    /// Verilen filtrelere uyan dis servis cagri kayitlarinin toplam sayisini getirir.
    /// </summary>
    /// <param name="input">Sayimda kullanilacak dis servis cagri kaydi filtre bilgileri.</param>
    /// <remarks>
    /// Liste endpointiyle ayni filtre kurallari uygulanir; response value eslesen kayit sayisidir.
    /// </remarks>
    [HttpPost(ExternalServiceLogRoutes.FilteredCount)]
    [Authorize(LogManagementPermissions.ExternalServiceLogs.View)]
    public virtual async Task<Result<int>> GetFilteredCount(
        [FromBody] DataTableListInput<ExternalServiceLogFilter> input)
    {
        var result = await AppService.GetFilteredCountAsync(input);
        return result;
    }
}
