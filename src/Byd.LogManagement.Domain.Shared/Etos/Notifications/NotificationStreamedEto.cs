using System;
using Byd.LogManagement.Consts;
using Volo.Abp.EventBus;

namespace Byd.LogManagement.Etos.Notifications;

/// <summary>
/// Uretici uygulamanin canli akisinin dagitik olayi.
/// </summary>
// islevi: Akisin kapsamini, ureten modulu, akisin adini ve JSON govdesini tasir.
// sistemdeki gorevi: Canli akis her mesajda kendini yeniledigi icin kalici degildir; olay outbox'a ve inbox'a yazilmadan
// tasinir, merkezi servis de satir yazmaz. Kacan mesajin yerini bir sonraki alir.
[EventName(NotificationEventNames.Streamed)]
public class NotificationStreamedEto
{
    // Akisin olustugu tenant; host kayitlari icin bos.
    public Guid? TenantId { get; set; }

    // Akisin olustugu organizasyon; varsa akis yalniz bu organizasyonun baglantilarina gider.
    public Guid? OrganizationId { get; set; }

    // Akisi ureten modulun adi.
    public string ModuleName { get; set; } = default!;

    // Akisin adi; istemci mesajlari bu adla ayirt eder.
    public string Name { get; set; } = default!;

    // Akisin JSON govdesi.
    public string Payload { get; set; } = default!;
}
