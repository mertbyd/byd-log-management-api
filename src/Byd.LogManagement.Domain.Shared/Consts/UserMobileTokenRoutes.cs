namespace Byd.LogManagement.Consts;

/// <summary>
/// Kullanici mobil token HTTP rotalarini tanimlar.
/// </summary>
// islevi: Mobil token kaydet, sil ve izin sorgula endpointlerinin kararli rota sozlesmesini tek yerde tutar.
// sistemdeki gorevi: Controller ile mobil istemcilerin ayni derleme guvenli route degerlerini kullanmasini saglar.
public static class UserMobileTokenRoutes
{
    // Mobil token endpoint ailesinin kok rotasidir.
    public const string Root = "api/log-management/mobile-tokens";

    // Bildirim aktiflik durumunu okuyan endpointin rota kalibidir.
    public const string Permit = "permit";
}
