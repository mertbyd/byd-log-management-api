using Byd.LogManagement.Dtos.ActivityLogs;
using Byd.LogManagement.Entities.ActivityLogs;
using Riok.Mapperly.Abstractions;

namespace Byd.LogManagement.Mappers.ActivityLogs;

/// <summary>
/// Etkinlik kaydi entity ve DTO eslemelerini tanimlar.
/// </summary>
// islevi: Etkinlik satirini Manager'in kurdugu aciklamayla cevaba Mapperly tarafindan uretilen kodla donusturur.
// sistemdeki gorevi: Application katmanindaki etkinlik kaydi cevap eslemesinin sahibidir.
[Mapper]
public partial class ActivityLogMapper
{
    // Etkinlik satirini, Manager'in istek dilinde kurdugu aciklamayla tek cevapta esler.
    public partial ActivityLogDto Map(ActivityLog source, string description);
}
