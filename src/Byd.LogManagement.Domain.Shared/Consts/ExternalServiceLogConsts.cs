namespace Byd.LogManagement.Consts;

/// <summary>
/// Dis servis cagri kaydinin tablo sabitini tanimlar.
/// </summary>
// islevi: Tablo adini derleme guvenli tek sabitte tutar.
// sistemdeki gorevi: EF konfigurasyonunun inline tablo adi tasimamasini saglar.
public static class ExternalServiceLogConsts
{
    // Dis servis cagri kayitlarinin tutuldugu tablonun EF model adidir.
    public const string TableName = "ExternalServiceLogs";
}
