namespace Byd.LogManagement.Consts;

/// <summary>
/// Bildirimin dagitik olay adlarini tanimlar.
/// </summary>
// islevi: Olay adlarini derleme guvenli sabitlerde tutar.
// sistemdeki gorevi: Yayinci ile Collector'un ayni tel adinda bulusmasinin tek kaynagidir.
public static class NotificationEventNames
{
    // Uretici uygulama bildirim istediginde yayinlanir; Collector satirlari kuyruga yazar.
    public const string Recorded = "LogManagement.Notifications.Recorded";


}
