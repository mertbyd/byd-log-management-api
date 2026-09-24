namespace Byd.LogManagement.Models.Notifications;

/// <summary>
/// Firebase projesi create ve update islemlerinin ortak domain girdisini tasir.
/// </summary>
// islevi: Public DTO alanlarini entity mutasyonundan ayiran tipli domain modelini tanimlar.
// sistemdeki gorevi: Mapperly ile FirebaseProjectManager arasindaki ortak girdi sozlesmesidir.
public abstract class FirebaseProjectInputModel
{
    public string Name { get; set; } = default!;
    public string Key { get; set; } = default!;
}
