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
    /// Request {
    ///   Page                          (int)                       → Sayfa numarasi; varsayilan 1
    ///   PageSize                      (int)                       → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting                       (DataTableSorting[]?)       → Siralama alanlari ve yonleri
    ///   Filters                       (ExternalServiceLogFilter?) → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     CorrelationId               (DataTableFilterItem?)      → Correlation filtresi
    ///     ExternalServiceProviderName (DataTableFilterItem?)      → Saglayici filtresi
    ///     ModuleName                  (DataTableFilterItem?)      → Modul filtresi
    ///     MethodName                  (DataTableFilterItem?)      → Uc filtresi
    ///     RequestType                 (DataTableFilterItem?)      → Istek tipi filtresi
    ///     StartTime                   (DataTableFilterItem?)      → Baslama zamani filtresi
    ///     ExecutionDuration           (DataTableFilterItem?)      → Sure filtresi
    ///     ResponseCode                (DataTableFilterItem?)      → Durum kodu filtresi
    ///     CreationTime                (DataTableFilterItem?)      → Kayit zamani filtresi
    /// }
    /// Response {
    ///   TotalCount (long)                    → Filtreye uyan toplam kayit sayisi
    ///   Items      (ExternalServiceLogDto[]) → Sayfadaki kayitlar; alanlari asagida
    /// }
    /// ExternalServiceLogDto {
    ///   Id                          (Guid)     → Kayit Id'si
    ///   CorrelationId               (string?)  → Cagriyi baslatan istegin correlation kimligi
    ///   ExternalServiceProviderName (string)   → Cagrilan dis servis saglayicisi
    ///   ModuleName                  (string)   → Cagriyi yapan modul
    ///   MethodName                  (string)   → Cagrilan uc
    ///   RequestType                 (string)   → Istek tipi
    ///   StartTime                   (DateTime) → Cagrinin baslama zamani
    ///   ExecutionDuration           (double)   → Cagri suresi (saniye)
    ///   ResponseCode                (int)      → Donen HTTP durum kodu
    ///   ExceptionMessage            (string?)  → Varsa hata mesaji
    ///   ResponseData                (string?)  → Ham yanit govdesi
    ///   CreationTime                (DateTime) → Satirin yazildigi zaman
    /// }
    /// Eslesen kayit yoksa toplam sayi sifir ve liste bos doner. Bu endpoint yeni kayit yazmaz.
    /// </remarks>
    [HttpPost(ExternalServiceLogRoutes.FilteredList)]
    [Authorize(LogManagementPermissions.ExternalServiceLogs.View)]
    public virtual async Task<Result<PagedResultDto<ExternalServiceLogDto>>> GetFilteredListAsync(
        [FromBody] DataTableListInput<ExternalServiceLogFilter> input,
        CancellationToken cancellationToken  = default)
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
    /// Request {
    ///   correlationId (string) → Istegin correlation kimligi (route)
    /// }
    /// Response elemanlari filtreli liste ucundaki ExternalServiceLogDto alanlarini tasir ve baslama zamanina gore siralidir.
    /// Eslesen cagri yoksa bos liste doner. Bu endpoint yeni kayit yazmaz.
    /// </remarks>
    [HttpGet(ExternalServiceLogRoutes.ByCorrelationId)]
    [Authorize(LogManagementPermissions.ExternalServiceLogs.View)]
    public virtual async Task<Result<List<ExternalServiceLogDto>>> GetListByCorrelationIdAsync(
        [FromRoute] string correlationId,
        CancellationToken cancellationToken = default)
    {
        var result = await AppService.GetListByCorrelationIdAsync(correlationId, cancellationToken);
        return result;
    }
}
