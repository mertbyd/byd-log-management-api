using System;
using Byd.LogManagement.Consts;
using Volo.Abp.EventBus;

namespace Byd.LogManagement.Etos.ExternalServiceLogs;

/// <summary>
/// Uretici uygulamadan dis bir servise yapilan cagrinin dagitik olayi.
/// </summary>
// islevi: Cagrinin kapsamini, hedefini, suresini, yanitini ve hata alanlarini tasir.
// sistemdeki gorevi: Uretici uygulama ile merkezi log servisi arasindaki tel sozlesmesidir; sinif Domain.Shared'da
// durdugu icin uretici uygulama kopya tutmaz, ayni tipi referans eder.
[EventName(ExternalServiceLogEventNames.Recorded)]
public class ExternalServiceLogRecordedEto
{
    // Cagrinin yapildigi tenant; host kapsamindaki cagrilar icin bos.
    public Guid? TenantId { get; set; }

    // Cagrinin yapildigi organizasyon; organizasyon kapsami disindaki cagrilar icin bos.
    public Guid? OrganizationId { get; set; }

    // Cagriyi baslatan istegin correlation kimligi; ABP audit log satirindaki correlation_id ile ayni degerdir.
    public string? CorrelationId { get; set; }

    // Cagri anindaki tenant adi.
    public string? TenantName { get; set; }

    // Cagri anindaki organizasyon adi.
    public string? OrganizationName { get; set; }

    // Cagrilan dis servis saglayicisinin adi.
    public string ExternalServiceProviderName { get; set; } = default!;

    // Cagriyi yapan tuketici modulun adi.
    public string ModuleName { get; set; } = default!;

    // Cagrilan uc veya operasyon adi.
    public string MethodName { get; set; } = default!;

    // Cagrinin istek tipi.
    public string RequestType { get; set; } = default!;

    // Cagrinin baslama zamani.
    public DateTime StartTime { get; set; }

    // Cagrinin saniye cinsinden suresi.
    public double ExecutionDuration { get; set; }

    // Donen HTTP durum kodu; cagri basariliysa 200, dis servis kod dondurmediyse 500.
    public int ResponseCode { get; set; }

    // Cagri istisna ile bittiyse istisnanin mesaji.
    public string? ExceptionMessage { get; set; }

    // Donen ham yanit govdesi; cagri istisna ile bittiyse bos kalir.
    public string? ResponseData { get; set; }
}
