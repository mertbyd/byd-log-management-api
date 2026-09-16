using System;
using System.Collections.Generic;
using Byd.LogManagement.ActivityLogs;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.LogManagement.Entities.ActivityLogs;

/// <summary>
/// Merkezi olarak saklanan etkinlik kaydini tasir.
/// </summary>
// islevi: Islem basina tek satirda etkinligin dil basina hazir cumlesini baglam alanlariyla saklar.
// sistemdeki gorevi: Listelerken istek dilindeki cumlesi secilen veri kabugudur.
//
// IMultiTenant yok: kayitlar merkezi log servisinde butun uygulamalardan toplanir; tenant filtresi uygulanmaz,
// TenantId yalniz etkinligin hangi tenant'ta olustugunu gosterir.
public class ActivityLog : CreationAuditedEntity<Guid>
{
    // Etkinligin olustugu tenant; host kayitlari icin bos.
    public Guid? TenantId { get; internal set; }
    // Etkinligin olustugu organizasyon; auth modulu organizasyon bilgisini saglayana kadar bos.
    public Guid? OrganizationId { get; internal set; }
    // Etkinligi yapan kullanicinin kimligi.
    public Guid UserId { get; internal set; }
    // Etkinligin dokundugu is kaydinin uretici uygulamadaki kimligi.
    public Guid? ExternalId { get; internal set; }
    // Etkinlik anindaki tenant adi.
    public string? TenantName { get; internal set; }
    // Etkinlik anindaki organizasyon adi.
    public string? OrganizationName { get; internal set; }
    // Etkinlik anindaki kullanici adi.
    public string UserName { get; internal set; } = default!;
    // Etkinligi ureten uygulamanin adi; CurrentModuleNameProvider'dan gelir.
    public string ApplicationName { get; internal set; } = default!;
    // Etkinligin turu.
    public ActivityKind Kind { get; internal set; }
    // Dil -> hazir cumle sozlugu; listelerken istek dilindeki cumle buradan secilir.
    public Dictionary<string, string> Description { get; internal set; } = [];

    // EF Core materializasyonu ve olaydan satir kuran uzanti icin parametresiz ctor; kimligi ABP ekleme aninda verir.
    internal ActivityLog()
    {
    }
}
