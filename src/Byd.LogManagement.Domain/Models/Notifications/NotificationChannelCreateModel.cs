namespace Byd.LogManagement.Models.Notifications;

/// <summary>
/// Yeni bildirim kanali kaydinin domain create girdisini tanimlar.
/// </summary>
// islevi: Kanalin is kodunu ve gorunen adini Manager'in entity kurma akimina tasir.
// sistemdeki gorevi: NotificationChannel olusturma senaryosunun transporttan bagimsiz girdi tipidir.
public sealed class NotificationChannelCreateModel
{
    public string Name { get; set; } = default!;
    public int Code { get; set; }
}
