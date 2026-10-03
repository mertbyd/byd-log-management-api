namespace Byd.LogManagement.Dtos.Notifications;

/// <summary>
/// Firebase projesinin FCM servis hesabi dosyasini yukleme istegini tanimlar.
/// </summary>
// islevi: Dosya adini ve JSON'da base64 olarak gelen dosya icerigini tasir; tenant istegi yapan kullanicidan gelir.
// sistemdeki gorevi: Secret yukleme endpointinin public girdi tipidir; proje kimligi route'tan gelir.
public sealed class UploadFirebaseProjectSecretDto
{
    public string FileName { get; set; } = default!;
    public byte[] Data { get; set; } = default!;
}
