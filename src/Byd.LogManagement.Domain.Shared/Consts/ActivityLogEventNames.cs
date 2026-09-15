namespace Byd.LogManagement.Consts;

/// <summary>
/// Etkinlik kaydinin dagitik olay adlarini tanimlar.
/// </summary>
// islevi: Olay adlarini derleme guvenli sabitlerde tutar.
// sistemdeki gorevi: Uretici uygulama ile merkezi log servisinin ayni tel adinda bulusmasinin tek kaynagidir.
public static class ActivityLogEventNames
{
    // Uretici uygulamada isaretli bir action basariyla bittiginde yayinlanir; merkezi log servisi satiri yazar.
    public const string Recorded = "LogManagement.ActivityLogs.Recorded";
}
