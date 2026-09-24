using System;

namespace Byd.LogManagement.Dtos.Notifications;

/// <summary>
/// Kalici Firebase projesi kaydinin public cevap alanlarini tasir.
/// </summary>
// islevi: Proje kimligini, lookup alanlarini ve olusturma zamanini HTTP ciktisina acar.
// sistemdeki gorevi: Entity veya EF tiplerini disariya sizdirmayan response sozlesmesidir.
public class FirebaseProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Key { get; set; } = default!;
    public string Environment { get; set; } = default!;
    public DateTime CreationTime { get; set; }
}
