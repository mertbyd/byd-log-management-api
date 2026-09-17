using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.LogManagement.Entities.ExternalServiceLogs;

/// <summary>
/// Merkezi olarak saklanan dis servis cagri kaydini tasir.
/// </summary>
// islevi: Cagri basina tek satirda hedefi, sonucu, suresi ve hata alanlarini saklar.
// sistemdeki gorevi: Kurallari Manager'a birakan veri kabugudur.
public class ExternalServiceLog : CreationAuditedEntity<Guid>
{
    // Cagriyi baslatan istegin correlation kimligi; ABP audit log satirindaki correlation_id ile ayni degerdir.
    public string? CorrelationId { get; internal set; }
    // Cagrilan dis servis saglayicisinin adi.
    public string ExternalServiceProviderName { get; internal set; } = default!;
    // Cagriyi yapan tuketici modulun adi.
    public string ModuleName { get; internal set; } = default!;
    // Cagrilan uc veya operasyon adi.
    public string MethodName { get; internal set; } = default!;
    // Cagrinin istek tipi.
    public string RequestType { get; internal set; } = default!;
    // Cagrinin baslama zamani.
    public DateTime StartTime { get; internal set; }
    // Cagrinin saniye cinsinden suresi; bitis zamani baslangica bu sure eklenerek bulunur.
    public double ExecutionDuration { get; internal set; }
    // Donen HTTP durum kodu; cagri basariliysa 200, dis servis kod dondurmediyse 500.
    public int ResponseCode { get; internal set; }
    // Cagri istisna ile bittiyse istisnanin mesaji.
    public string? ExceptionMessage { get; internal set; }
    // Donen ham yanit govdesi; cagri istisna ile bittiyse bos kalir.
    public string? ResponseData { get; internal set; }

    // EF Core materializasyonu ve olaydan satir kuran uzanti icin parametresiz ctor; kimligi ABP ekleme aninda verir.
    internal ExternalServiceLog()
    {
    }
}
