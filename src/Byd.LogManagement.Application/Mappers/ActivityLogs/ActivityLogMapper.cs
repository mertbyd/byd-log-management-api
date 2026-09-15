using Byd.LogManagement.Dtos.ActivityLogs;
using Byd.LogManagement.Entities.ActivityLogs;
using Byd.LogManagement.Etos.ActivityLogs;
using Riok.Mapperly.Abstractions;

namespace Byd.LogManagement.Mappers.ActivityLogs;

/// <summary>
/// Etkinlik kaydi olay, entity ve DTO eslemelerini tanimlar.
/// </summary>
// islevi: Gelen olayi yeni satira, satiri Manager'in kurdugu aciklamayla cevaba Mapperly tarafindan uretilen kodla donusturur.
// sistemdeki gorevi: Etkinlik kaydi katmanlari arasindaki alan tasimanin tek sahibidir.
[Mapper]
public partial class ActivityLogMapper
{
    // Gelen etkinlik olayini eklenecek yeni satira esler.
    public partial ActivityLog Map(ActivityRecordedEto source);

    // Etkinlik satirini, Manager'in istek dilinde kurdugu aciklamayla tek cevapta esler.
    public partial ActivityLogDto Map(ActivityLog source, string description);
}
