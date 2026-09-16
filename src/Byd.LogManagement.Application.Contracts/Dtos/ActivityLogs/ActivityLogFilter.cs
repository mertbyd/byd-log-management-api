using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.LogManagement.Consts;

namespace Byd.LogManagement.Dtos.ActivityLogs;

/// <summary>
/// Etkinlik kaydi okuma uclarinin filtre alanlarini tasir.
/// </summary>
// islevi: Her filtre propertysini kararli tel adi ve entity property yoluyla DataTable motoruna tanitir.
// sistemdeki gorevi: Etkinlik kaydi liste ve sayim uclarinin tipli filtre izin listesidir.
public class ActivityLogFilter
{
    [DataTable(ActivityLogFilterFields.UserId, ActivityLogFilterFields.PropertyPaths.UserId)]
    public DataTableFilterItem? UserId { get; set; }

    [DataTable(ActivityLogFilterFields.ExternalId, ActivityLogFilterFields.PropertyPaths.ExternalId)]
    public DataTableFilterItem? ExternalId { get; set; }

    [DataTable(ActivityLogFilterFields.ApplicationName, ActivityLogFilterFields.PropertyPaths.ApplicationName)]
    public DataTableFilterItem? ApplicationName { get; set; }

    [DataTable(ActivityLogFilterFields.Kind, ActivityLogFilterFields.PropertyPaths.Kind)]
    public DataTableFilterItem? Kind { get; set; }

    [DataTable(ActivityLogFilterFields.CreationTime, ActivityLogFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
