using Byd.LogManagement.Entities.ActivityLogs;
using Byd.LogManagement.Etos.ActivityLogs;

namespace Byd.LogManagement.Extensions.ActivityLogs;

/// <summary>
/// Etkinlik olayini merkezi log satirina donusturur.
/// </summary>
// islevi: ActivityRecordedEto alanlarini yeni bir ActivityLog satirina elle tasir.
// sistemdeki gorevi: Olaydan satir kurmanin tek yeridir; satir Domain'de kuruldugu icin tuketici modul entity'nin
// ic setter'larini goren bir esleyiciye ihtiyac duymaz.
public static class ActivityRecordedEtoExtensions
{
    // Olayin baglam alanlarini yeni etkinlik satirina tasir; kimligi ve olusturma zamanini ABP ekleme aninda verir.
    public static ActivityLog ToActivityLog(this ActivityRecordedEto source)
    {
        return new ActivityLog
        {
            TenantId = source.TenantId,
            OrganizationId = source.OrganizationId,
            UserId = source.UserId,
            ExternalId = source.ExternalId,
            TenantName = source.TenantName,
            OrganizationName = source.OrganizationName,
            UserName = source.UserName,
            ApplicationName = source.ApplicationName,
            Kind = source.Kind,
            Description = source.Description
        };
    }
}
