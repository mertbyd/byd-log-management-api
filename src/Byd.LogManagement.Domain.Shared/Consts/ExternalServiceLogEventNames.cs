namespace Byd.LogManagement.Consts;

/// <summary>
/// Dis servis kaydinin dagitik olay adlarini tanimlar.
/// </summary>
// islevi: Dis servis log olay adlarini derleme guvenli sabitlerde tutar.
// sistemdeki gorevi: Uretici uygulama ile merkezi log servisinin ayni tel adinda bulusmasinin tek kaynagidir.
public static class ExternalServiceLogEventNames
{
    public const string Recorded = "LogManagement.ExternalServiceLogs.Recorded";
}
