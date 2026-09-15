namespace Byd.LogManagement.ActivityLogs;

/// <summary>
/// Etkinlik kaydinin turlerini tanimlar.
/// </summary>
// islevi: Bir etkinligin kayit uzerinde ne yaptigini sabit degerlerle ifade eder.
// sistemdeki gorevi: TrackActivity beyaninin ve etkinlik kaydi filtresinin ortak tur sozlesmesidir.
public enum ActivityKind
{
    // Yeni kayit olusturuldu.
    Created = 1,

    // Mevcut kayit guncellendi.
    Updated = 2,

    // Kayit silindi.
    Deleted = 3,

    // Dosya yuklendi.
    Uploaded = 4,

    // Dosya indirildi.
    Downloaded = 5,

    // Kaydin durumu degisti.
    StatusChanged = 6
}
