using System;
using System.Collections.Generic;
using Byd.LogManagement.Consts;
using Volo.Abp.EventBus;

namespace Byd.LogManagement.Etos.Notifications;

/// <summary>
/// Uretici uygulamanin istedigi bildirimin dagitik olayi.
/// </summary>
// islevi: Bildirimin icerigini, kapsamini, kanallarini, vadesini ve alici listesini tasir.
// sistemdeki gorevi: Uretici uygulama ile merkezi bildirim servisi arasindaki tel sozlesmesidir; sinif
// Domain.Shared'da durdugu icin uretici uygulama kopya tutmaz, ayni tipi referans eder.
// Lookup kimligi degil is kodu tasinir; kimlik ortamdan ortama degisir, kod degismez.
[EventName(NotificationEventNames.Recorded)]
public class NotificationRecordedEto
{
    // Bildirimin olustugu tenant; host kayitlari icin bos.
    public Guid? TenantId { get; set; }

    // Bildirimin olustugu organizasyon; auth modulu organizasyon bilgisini saglayana kadar bos.
    public Guid? OrganizationId { get; set; }

    // Bildirimin gonderilecegi kanallarin is kodlari; NotificationConsts icindeki kanal kodlariyla eslesir ve her kanal
    // alici basina ayri satir uretir.
    public List<int> ChannelCodes { get; set; } = [];

    // Mobil kanalin hangi Firebase projesiyle gidecegini soyleyen benzersiz anahtar; mobil kanal istendiyse zorunludur.
    public string? FirebaseProjectKey { get; set; }

    // Mobil bildirimin gidecegi topic; verildiyse mobil satir alicilara degil topic uyelerine gider.
    public string? Topic { get; set; }

    // Bildirimi baslatan kullanicinin kimligi; sistem uretimi bildirimlerde bos kalir.
    public Guid? SenderUserId { get; set; }

    // Bildirimin dokundugu is kaydinin uretici uygulamadaki kimligi.
    public Guid? ExternalId { get; set; }

    // Bildirim anindaki tenant adi.
    public string? TenantName { get; set; }

    // Bildirim anindaki organizasyon adi.
    public string? OrganizationName { get; set; }

    // Bildirim anindaki gonderen kullanici adi.
    public string? SenderUserName { get; set; }

    // Bildirimi isteyen tuketici modulun adi.
    public string ModuleName { get; set; } = default!;

    // Bildirim basligi.
    public string Title { get; set; } = default!;

    // Bildirimin kapsamindaki herkese gidecegini gosterir: organizasyonda olustuysa organizasyona, degilse tenant'a;
    // isaretliyse alici listesine bakilmaz.
    public bool IsTenantNotification { get; set; }

    // Bildirimin gonderilebilecegi en erken an; gecmis bir deger anlik gonderim demektir.
    public DateTime ScheduledTime { get; set; }

    // Uretici modulun doldurdugu serbest anahtarli icerik; su an anahtar dil kodudur (tr, en).
    public Dictionary<string, string> Data { get; set; } = [];

    // Tenant bildirimi degilse bildirimin gonderilecegi alicilar; liste bossa hicbir satir yazilmaz.
    public List<NotificationRecipientEto> Recipients { get; set; } = [];
}
