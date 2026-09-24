using System;

namespace Byd.LogManagement.Models.Notifications;

/// <summary>
/// Kullanici mobil token kaydinin domain girdisini tanimlar.
/// </summary>
// islevi: Cozulmus proje kimligini, oturumdaki kullaniciyi, token ve cihaz dil kodunu Manager'in kayit akisina tasir.
// sistemdeki gorevi: UserMobileToken kaydet/guncelle senaryosunun transporttan bagimsiz girdi tipidir.
public sealed class UserMobileTokenRegisterModel
{
    public Guid FirebaseProjectId { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; } = default!;
    public string? LanguageCode { get; set; }
    public bool IsActive { get; set; } = true;
}
