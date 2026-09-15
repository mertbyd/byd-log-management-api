using Byd.LogManagement.Entities.ActivityLogs;
using Byd.LogManagement.Etos.ActivityLogs;
using Riok.Mapperly.Abstractions;

namespace Byd.LogManagement.Mappers.ActivityLogs;

/// <summary>
/// Etkinlik kaydi olay ve entity eslemelerini tanimlar.
/// </summary>
// islevi: Gelen olayi yeni satira Mapperly tarafindan uretilen kodla donusturur.
// sistemdeki gorevi: Etkinlik kaydi katmanlari arasindaki alan tasimanin tek sahibidir.
[Mapper]
public partial class ActivityLogMapper
{
    // Gelen etkinlik olayini eklenecek yeni satira esler.
    public partial ActivityLog Map(ActivityRecordedEto source);
}
