using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.CoreExtensions.Querying;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Dtos.ExternalServiceLogs;
using Byd.LogManagement.Permissions;
using Byd.LogManagement.Services.ExternalServiceLogs;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;

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
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value, eslesen toplam kayit sayisini ve sayfadaki kayitlari tasir. Kayit elemanlari Id, cagriyi
    /// baslatan istegin correlation kimligi, cagrilan dis servis saglayicisi, cagriyi yapan tuketici modul,
    /// cagrilan uc ve istek tipi, cagrinin baslama zamani ve suresi, donen HTTP durum kodu, varsa hata mesaji,
    /// ham yanit govdesi ve satirin yazildigi zamani icerir. Eslesen kayit yoksa toplam sayi sifir ve liste bos
    /// doner. Bu endpoint yeni kayit yazmaz.
    /// </remarks>
    [HttpPost(ExternalServiceLogRoutes.FilteredList)]
    [Authorize(LogManagementPermissions.ExternalServiceLogs.View)]
    public virtual async Task<Result<PagedResultDto<ExternalServiceLogDto>>> GetFilteredListAsync(
        [FromBody] DataTableListInput<ExternalServiceLogFilter> input,
        CancellationToken cancellationToken)
    {
        var result = await AppService.GetFilteredListAsync(input, cancellationToken);
        return result;
    }

    /// <summary>
    /// Ayni istegin correlation kimligiyle kaydedilen dis servis cagrilarini getirir.
    /// </summary>
    /// <param name="correlationId">Iliskili dis servis cagrilarini birlestiren correlation kimligi.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Response value, verilen correlation kimligine bagli dis servis cagrilarini baslama zamanina gore sirali
    /// dondurur. Eslesen cagri yoksa bos liste doner. Bu endpoint yeni kayit yazmaz.
    /// </remarks>
    [HttpGet(ExternalServiceLogRoutes.ByCorrelationId)]
    [Authorize(LogManagementPermissions.ExternalServiceLogs.View)]
    public virtual async Task<Result<List<ExternalServiceLogDto>>> GetListByCorrelationIdAsync(
        [FromRoute] string correlationId,
        CancellationToken cancellationToken)
    {
        var result = await AppService.GetListByCorrelationIdAsync(correlationId, cancellationToken);
        return result;
    }
}
