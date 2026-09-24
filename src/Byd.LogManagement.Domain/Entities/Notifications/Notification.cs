using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Byd.LogManagement.Entities.Notifications;

/// <summary>
/// Gonderilmeyi bekleyen veya gonderilmis tek bir bildirimi tasir.
/// </summary>
// islevi: Alici ve kanal basina tek satirda icerigi, kanali, vadeyi, gonderim ve okunma durumunu saklar.
// sistemdeki gorevi: Kurallari Manager'a birakan veri kabugudur.
//
// Satir gonderimden ONCE yazilir: SentTime bos oldugu surece bildirim kuyruktadir ve degistirilebilir.
// Ayni istekten dogan butun satirlar ayni NotificationGroupId'yi tasir; grup guncelleme ve iptal bu
// kimlik uzerinden calisir, icerik esitligi uzerinden degil.
// Tenant'taki herkese gidecegini IsTenantNotification soyler; bos RecipientUserId tek basina yayin demek degildir.
//
// IMultiTenant: sorgular istegin tenant'iyla sinirlanir; butun tenant'lari tarayan isci filtreyi kendisi kapatir.
// Navigation property yok: lookup adlari okuma ucunde cozulur.
public class Notification : CreationAuditedEntity<Guid>, IMultiTenant
{
    // Bildirimin olustugu tenant; host kayitlari icin bos.
    public Guid? TenantId { get; internal set; }
    // Bildirimin olustugu organizasyon; auth modulu organizasyon bilgisini saglayana kadar bos.
    public Guid? OrganizationId { get; internal set; }
    // Ayni istekten dogan butun satirlari birbirine baglayan kimlik.
    public Guid NotificationGroupId { get; internal set; }
    // Bildirimin gonderilecegi kanalin lookup kimligi.
    public Guid ChannelId { get; internal set; }
    // Mobil satirin hangi Firebase projesiyle gidecegi; web satirinda bos.
    public Guid? FirebaseProjectId { get; internal set; }
    // Bildirimi baslatan kullanicinin kimligi; sistem uretimi bildirimlerde bos.
    public Guid? SenderUserId { get; internal set; }
    // Bildirimin gonderilecegi kullanicinin kimligi; tenant bildiriminde bos.
    public Guid? RecipientUserId { get; internal set; }
    // Bildirimin dokundugu is kaydinin uretici uygulamadaki kimligi.
    public Guid? ExternalId { get; internal set; }
    // Bildirim anindaki tenant adi.
    public string? TenantName { get; internal set; }
    // Bildirim anindaki organizasyon adi.
    public string? OrganizationName { get; internal set; }
    // Bildirim anindaki gonderen kullanici adi.
    public string? SenderUserName { get; internal set; }
    // Bildirim anindaki alici kullanici adi; tenant bildiriminde bos.
    public string? RecipientUserName { get; internal set; }
    // Bildirimi isteyen tuketici modulun adi.
    public string ModuleName { get; internal set; } = default!;
    // Bildirim basligi.
    public string Title { get; internal set; } = default!;
    // Mobil satirin gidecegi topic; doluysa satir alicilara degil topic uyelerine gider.
    public string? Topic { get; internal set; }
    // Bildirimin tek bir aliciya degil tenant'taki herkese gittigini gosterir.
    // TODO: IsTenantNotification, auth modulu organizasyon bilgisini sagladiginda organizasyon kapsamiyla birlikte guncellenir. çözüm yolu bulunucak !!!!!!!!!!!!!!1
    public bool IsTenantNotification { get; internal set; }
    // Bildirimin gonderilebilecegi en erken an.
    public DateTime ScheduledTime { get; internal set; }
    // Bildirimin gonderildigi an; bos olmasi bildirimin hala kuyrukta oldugu anlamina gelir.
    public DateTime? SentTime { get; internal set; }
    // Uretici modulun doldurdugu serbest anahtarli icerik; su an dil kodu basina hazir metin tasir ve
    // gonderirken alicinin diline gore secilir.
    public Dictionary<string, string> Data { get; internal set; } = [];

    // EF Core materializasyonu ve olaydan satir kuran uzanti icin parametresiz ctor; kimligi ABP ekleme aninda verir.
    internal Notification()
    {
    }
}
