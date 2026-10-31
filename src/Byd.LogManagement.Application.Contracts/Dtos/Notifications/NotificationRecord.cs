using System;
using System.Collections.Generic;
using Byd.LogManagement.Etos.Notifications;

namespace Byd.LogManagement.Dtos.Notifications;

/// <summary>
/// Uretici uygulamanin istedigi bildirimin cagiran tarafindan doldurulan alanlari.
/// </summary>
// islevi: Kanallar, baslik, icerik sozlugu ve alici listesi gibi yalniz cagiranin bilebilecegi alanlari tasir.
// sistemdeki gorevi: Uretici uygulamanin bildirim yakalama noktasina verdigi tek girdi sozlesmesidir;
// modul adi, tenant ve gonderen kullanici gibi baglamdan cozulen alanlar burada istenmez.
public sealed record NotificationRecord(
    List<int> ChannelCodes,
    string Title,
    Dictionary<string, string> Data)
{
    // Mobil kanalin hangi Firebase projesiyle gidecegini soyleyen benzersiz anahtar; mobil kanal istendiyse zorunludur.
    public string? FirebaseProjectKey { get; init; }

    // Mobil bildirimin gidecegi topic; verildiyse mobil satir alicilara degil topic uyelerine gider.
    public string? Topic { get; init; }

    // Bildirimin dokundugu is kaydinin uretici uygulamadaki kimligi.
    public Guid? ExternalId { get; init; }

    // Bildirimin kapsamindaki herkese gidecegini gosterir: organizasyonda olustuysa organizasyona, degilse tenant'a;
    // isaretliyse alici listesine bakilmaz.
    public bool IsTenantNotification { get; init; }

    // Bildirimin gonderilebilecegi en erken an; verilmezse bildirim anlik gonderim icin kuyruga girer.
    public DateTime? ScheduledTime { get; init; }

    // Tenant bildirimi degilse bildirimin gonderilecegi alicilar; liste bossa hicbir satir yazilmaz.
    public List<NotificationRecipientEto> Recipients { get; init; } = [];
}
