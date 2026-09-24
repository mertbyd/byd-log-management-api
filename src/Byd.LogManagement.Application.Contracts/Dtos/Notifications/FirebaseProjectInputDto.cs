namespace Byd.LogManagement.Dtos.Notifications;

/// <summary>
/// Firebase projesi create ve update isteklerinin ortak alanlarini tasir.
/// </summary>
// islevi: Public CRUD girdisinin degistirilebilir proje alanlarini tanimlar.
// sistemdeki gorevi: FluentValidation ile Mapperly arasindaki ortak request sozlesmesidir.
public abstract class FirebaseProjectInputDto
{
    public string Name { get; set; } = default!;
    public string Key { get; set; } = default!;
}
