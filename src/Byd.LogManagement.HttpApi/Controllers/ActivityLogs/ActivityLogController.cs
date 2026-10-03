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
    /// Request {
    ///   Page              (int)                  → Sayfa numarasi; varsayilan 1
    ///   PageSize          (int)                  → Sayfa boyutu; varsayilan 10, en fazla 1000
    ///   Sorting           (DataTableSorting[]?)  → Siralama alanlari ve yonleri
    ///   Filters           (ActivityLogFilter?)   → Alan bazli filtreler; her alan { Operator, Value, SecondValue, Values } tasir
    ///     UserId          (DataTableFilterItem?) → Kullanici filtresi
    ///     ExternalId      (DataTableFilterItem?) → Is kaydi filtresi
    ///     ApplicationName (DataTableFilterItem?) → Modul filtresi
    ///     Kind            (DataTableFilterItem?) → Etkinlik turu filtresi
    ///     CreationTime    (DataTableFilterItem?) → Kayit zamani filtresi
    /// }
    /// Response {
    ///   TotalCount (long)             → Filtreye uyan toplam kayit sayisi
    ///   Items      (ActivityLogDto[]) → Sayfadaki kayitlar; alanlari asagida
    /// }
    /// ActivityLogDto {
    ///   Id               (Guid)         → Kayit Id'si
    ///   TenantId         (Guid?)        → Etkinligin tenant'i
    ///   TenantName       (string?)      → Tenant adi
    ///   OrganizationId   (Guid?)        → Etkinligin organizasyonu
    ///   OrganizationName (string?)      → Organizasyon adi
    ///   UserId           (Guid)         → Islemi yapan kullanici Id'si
    ///   UserName         (string)       → Islemi yapan kullanici adi
    ///   ExternalId       (Guid?)        → Etkinligin dokundugu is kaydi Id'si
    ///   ApplicationName  (string)       → Etkinligi ureten modul
    ///   Kind             (ActivityKind) → Etkinlik turu
    ///   Description      (string)       → Istegin dilinde doldurulmus aciklama
    ///   CreationTime     (DateTime)     → Kayit zamani
    /// }
    /// Eslesen kayit yoksa toplam sayi sifir ve liste bos doner. Bu endpoint yeni kayit yazmaz.
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
