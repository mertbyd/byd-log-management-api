using System;
using System.Collections.Generic;
using Byd.LogManagement.Etos.Notifications;

namespace Byd.LogManagement.Dtos.Notifications;

/// <summary>
/// Uretici uygulamanin istedigi bildirimin cagiran tarafindan doldurulan alanlari.
/// </summary>
// islevi: Kanal, baslik, metin ve alici listesi gibi yalniz cagiranin bilebilecegi alanlari tasir.
// sistemdeki gorevi: Uretici uygulamanin bildirim yakalama noktasina verdigi tek girdi sozlesmesidir;
// modul adi, tenant ve gonderen kullanici gibi baglamdan cozulen alanlar burada istenmez.
public sealed record NotificationRecord(
    int ChannelCode,
    string Title,
    Dictionary<string, string> Description)
{
    // Bildirimin dokundugu is kaydinin uretici uygulamadaki kimligi.
    public Guid? ExternalId { get; init; }

    // Bildirimin tenant'taki herkese gidecegini gosterir; isaretliyse alici listesine bakilmaz.
    public bool IsTenantNotification { get; init; }

    // Bildirimin gonderilebilecegi en erken an; verilmezse bildirim anlik gonderim icin kuyruga girer.
    public DateTime? ScheduledTime { get; init; }

    // Tenant bildirimi degilse bildirimin gonderilecegi alicilar; liste bossa hicbir satir yazilmaz.
    public List<NotificationRecipientEto> Recipients { get; init; } = [];
}
