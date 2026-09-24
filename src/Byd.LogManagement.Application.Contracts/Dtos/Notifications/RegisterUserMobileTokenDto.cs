namespace Byd.LogManagement.Dtos.Notifications;

/// <summary>
/// Oturumdaki kullanicinin mobil token'ini kaydetme istegini tanimlar.
/// </summary>
// islevi: Firebase proje anahtarini, cihazin FCM kayit token'ini, dil kodunu ve aktiflik durumunu tasir.
// sistemdeki gorevi: Mobil token kayit endpointinin public girdi tipidir; kullanici kimligi istekten degil oturumdan gelir.
public sealed class RegisterUserMobileTokenDto
{
    public string FirebaseProjectKey { get; set; } = default!;
    public string Token { get; set; } = default!;

    // Cihazin dil kodu ("tr", "en-US" gibi); verilmezse bildirim metni istegin diline duser.
    public string? LanguageCode { get; set; }

    public bool IsActive { get; set; } = true;
}
