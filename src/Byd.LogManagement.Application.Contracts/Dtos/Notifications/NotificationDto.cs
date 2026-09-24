using System;

namespace Byd.LogManagement.Dtos.Notifications;

/// <summary>
/// Oturum kullanicisinin bildirim listesindeki tek satirin public cevap alanlarini tasir.
/// </summary>
// islevi: Bildirimin kimligini, basligini, istek dilinde secilmis metnini, okundu durumunu ve gonderim anini
// HTTP ciktisina acar.
// sistemdeki gorevi: Entity veya EF tiplerini disariya sizdirmayan response sozlesmesidir; icerik sozlugu
// disariya cikmaz, tek dil secilmis olarak doner.
public class NotificationDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsRead { get; set; }
    public DateTime? SentTime { get; set; }
}
