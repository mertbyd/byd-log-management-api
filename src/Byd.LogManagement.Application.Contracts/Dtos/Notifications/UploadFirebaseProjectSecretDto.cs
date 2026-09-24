using System;

namespace Byd.LogManagement.Dtos.Notifications;

/// <summary>
/// Firebase projesinin FCM servis hesabi dosyasini yukleme istegini tanimlar.
/// </summary>
// islevi: Hedef tenant'i, dosya adini ve JSON'da base64 olarak gelen dosya icerigini tasir.
// sistemdeki gorevi: Secret yukleme endpointinin public girdi tipidir; proje kimligi route'tan gelir.
public sealed class UploadFirebaseProjectSecretDto
{
    // Secret'in yazilacagi tenant; host projeleri icin bos birakilir.
    // TODO: Auth modulu bagalandiginda kaldirilacak; tenant yukleyen kullanicinin token'indan okunacak.
    public Guid? TenantId { get; set; }

    public string FileName { get; set; } = default!;
    public byte[] Data { get; set; } = default!;
}
