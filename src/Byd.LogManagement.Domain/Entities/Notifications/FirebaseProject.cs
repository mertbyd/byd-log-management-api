using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Byd.LogManagement.Entities.Notifications;

/// <summary>
/// Mobil bildirimlerin gonderildigi Firebase projesi kaydini tasir.
/// </summary>
// islevi: Tenant'in Firebase projesinin adini, benzersiz anahtarini ve calisma ortamini kalici olarak tutar.
// sistemdeki gorevi: Kurallari FirebaseProjectManager'a birakan ince persistence kabugudur. Uretici
// bildirimde projeyi anahtariyla adresler; cihaz token'lari ve FCM secret'i bu kayda baglanir.
//
// IMultiTenant: her tenant kendi Firebase projelerini gorur; ad ve anahtar tekilligi tenant ve ortam icinde aranir.
public class FirebaseProject : CreationAuditedEntity<Guid>, IMultiTenant
{
    // Projenin ait oldugu tenant; host kayitlari icin bos.
    public Guid? TenantId { get; internal set; }

    // Projenin ait oldugu organizasyon; auth modulu organizasyon bilgisini saglayana kadar bos.
    public Guid? OrganizationId { get; internal set; }

    // Projenin kullaniciya gosterilen adidir.
    public string Name { get; internal set; } = default!;

    // Uretici bildirimin projeyi adresledigi benzersiz anahtardir (slug).
    public string Key { get; internal set; } = default!;

    // Projenin ve ona bagli FCM secret dosyasinin kullanildigi calisma ortamidir.
    public string Environment { get; internal set; } = default!;

    // EF Core materializasyonu icin bos kurucudur.
    protected FirebaseProject()
    {
    }

    // Manager tarafindan uretilen kimlikle bos veri kabugunu kurar.
    internal FirebaseProject(Guid id)
        : base(id)
    {
    }
}
