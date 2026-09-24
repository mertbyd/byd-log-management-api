using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.LogManagement.Entities.Notifications;

/// <summary>
/// Mobil bildirimin tek bir hedefe gonderim denemesinin sonucunu tasir.
/// </summary>
// islevi: Hangi bildirim satirinin hangi cihaz token'ina ya da topic'e gonderildigini, basarili olup olmadigini, FCM'in
// dondurdugu mesaji ve gonderimin suresini saklar.
// sistemdeki gorevi: Bildirim satirinin mobil gonderim gunlugudur; bir mobil satir, alicinin cihaz sayisi kadar kayit
// uretebilir. Basarisiz gonderim tekrar denenmez, sonucu burada kalir.
public class MobileNotification : CreationAuditedEntity<Guid>
{
    // Gonderilen bildirim satirinin kimligi.
    public Guid NotificationId { get; internal set; }

    // Gonderimin hedefi: cihaz token'i ya da topic adi.
    public string Target { get; internal set; } = default!;//todo eğer tek token cihaza göre güncelenecekse direk token ı log için yoksa dire usermobiletoke.id de tuttulabilir  (yapay zeka silme )

    // FCM gonderimi kabul ettiyse isaretlidir.
    public bool IsSuccess { get; internal set; }

    // FCM'in dondurdugu hata kodu; basaride bos, hatada saglayicinin verdigi kodu tasir.
    public string? StatusCode { get; internal set; }

    // FCM'in dondurdugu mesaj kimligi ya da hata mesaji.
    public string? ErrorMessage { get; internal set; }

    // Gonderimin milisaniye cinsinden suresi.
    public double ExecutionDuration { get; internal set; }

    // EF Core materializasyonu icin bos kurucudur.
    protected MobileNotification()
    {
    }

    // Manager tarafindan uretilen kimlikle bos veri kabugunu kurar.
    internal MobileNotification(Guid id)
        : base(id)
    {
    }
}
