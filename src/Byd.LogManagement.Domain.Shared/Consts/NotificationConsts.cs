using System.Collections.Generic;

namespace Byd.LogManagement.Consts;

/// <summary>
/// Bildirimin tablo, kolon ve lookup kod sabitlerini tanimlar.
/// </summary>
// islevi: Bildirim tablolarinin kalicilik sabitlerini ve lookup satirlarinin is kodlarini tek yerde tutar.
// sistemdeki gorevi: EF konfigurasyonu, uretici uygulama ve seed akisinin ayni degerleri okumasini saglar;
// lookup satirinin kimligi ortamdan ortama degisir, is kodu degismez.
public static class NotificationConsts
{
    // Alici basina yazilan bildirim satirlarinin tutuldugu tablonun EF model adidir.
    public const string TableName = "Notifications";

    // Bildirim kanali lookup tablosunun EF model adidir.
    public const string ChannelTableName = "NotificationChannels";

    // Dil -> hazir metin sozlugunun saklandigi kolon tipi.
    public const string DescriptionColumnType = "jsonb";

    // Lookup kayitlarinin gorunen adi icin ust sinir.
    public const int MaxLookupNameLength = 50;

    // Bildirim metninin kurulup saklandigi diller.
    public static readonly IReadOnlyList<string> DescriptionCultureNames = ["tr", "en"];

    // Tarayici uzerinden gonderilen bildirim kanalinin is kodu.
    public const int WebChannelCode = 1;

    // Mobil uygulamaya gonderilen bildirim kanalinin is kodu.
    public const int MobileChannelCode = 2;

    // Web istemcisine iletilen bildirim olayinin sabit adidir.
    public const string WebMessageName = "notificationReceived";

    // Tenant bildirimlerinin gittigi SignalR grubunun on ekidir; grup adi "<onek>:<tenantId>" seklindedir.
    public const string TenantGroupPrefix = "notification-tenant";

    // Ileri tarihli web bildirimlerinin vadesi gelmis mi diye tarandigi aralik (saniye).
    public const int ScheduledScanPeriodSeconds = 10;
}
