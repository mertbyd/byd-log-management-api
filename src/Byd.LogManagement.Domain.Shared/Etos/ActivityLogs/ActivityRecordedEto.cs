using System;
using System.Collections.Generic;
using Byd.LogManagement.ActivityLogs;
using Byd.LogManagement.Consts;
using Volo.Abp.EventBus;

namespace Byd.LogManagement.Etos.ActivityLogs;

/// <summary>
/// Uretici uygulamada gerceklesen bir etkinligin dagitik olayi.
/// </summary>
// islevi: Etkinligin dil basina hazir cumlesini baglam alanlariyla tasir.
// sistemdeki gorevi: Uretici uygulama ile merkezi log servisi arasindaki tel sozlesmesidir; sinif Domain.Shared'da
// durdugu icin uretici uygulama kopya tutmaz, ayni tipi referans eder.
[EventName(ActivityLogEventNames.Recorded)]
public class ActivityRecordedEto
{
    // Etkinligin olustugu tenant; host kayitlari icin bos.
    public Guid? TenantId { get; set; }

    // Etkinligin olustugu organizasyon; auth modulu organizasyon bilgisini saglayana kadar bos.
    public Guid? OrganizationId { get; set; }

    // Etkinligi yapan kullanicinin kimligi.
    public Guid UserId { get; set; }

    // Etkinligin dokundugu is kaydinin uretici uygulamadaki kimligi.
    public Guid? ExternalId { get; set; }

    // Etkinlik anindaki tenant adi.
    public string? TenantName { get; set; }

    // Etkinlik anindaki organizasyon adi.
    public string? OrganizationName { get; set; }

    // Etkinlik anindaki kullanici adi.
    public string UserName { get; set; } = default!;

    // Etkinligi ureten uygulamanin adi.
    public string ApplicationName { get; set; } = default!;

    // Etkinligin turu.
    public ActivityKind Kind { get; set; }

    // Etkinligin dil basina hazir cumlesi; anahtar kultur adidir (tr, en).
    public Dictionary<string, string> Description { get; set; } = [];
}
