using System;
using Byd.CoreExtensions.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.LogManagement.Entities.Notifications;

/// <summary>
/// Kullanicinin mobil push bildirim hedef kaydini tasir.
/// </summary>
// islevi: Hangi kullanicinin hangi Firebase projesine bagli en son aktif mobil token'ini, cihazin dilini ve bildirim
// aktiflik durumunu tutar.
// sistemdeki gorevi: Kurallari UserMobileTokenManager'a birakan veri kabugudur; mobil gonderici alicinin aktif
// token'ini buradan bulur.
//
// Tenant'siz: kullanici kimligi tenant'lar arasinda tektir, kapsam bagli oldugu Firebase projesinden gelir.
public class UserMobileToken : AuditedEntity<Guid>, IHasIsActive
{
    // Token'in ait oldugu Firebase projesinin kimligi; proje silinince token kayitlari da silinir.
    public Guid FirebaseProjectId { get; internal set; }

    // Token'i kaydeden kullanicinin kimligi; istekteki oturumdan gelir.
    public Guid UserId { get; internal set; }

    // FCM'in cihaza verdigi en guncel kayit token'idir.
    public string Token { get; internal set; } = default!;

    // Cihazin dil kodu; bildirim metni bu dile gore secilir. Bos ise gonderim istegin diline duser.
    public string? LanguageCode { get; internal set; }

    // Token aktifse bildirim alir; pasif kayitlar global veri filtresi tarafindan sorgulardan otomatik elenir.
    public bool IsActive { get; internal set; } = true;

    // EF Core materializasyonu icin bos kurucudur.
    protected UserMobileToken()
    {
    }

    // Manager tarafindan uretilen kimlikle bos veri kabugunu kurar.
    internal UserMobileToken(Guid id)
        : base(id)
    {
    }

    // IHasIsActive yazma yuzeyini Domain disina public entity mutasyonu acmadan uygular.
    bool IHasIsActive.IsActive
    {
        get => IsActive;
        set => IsActive = value;
    }
}
