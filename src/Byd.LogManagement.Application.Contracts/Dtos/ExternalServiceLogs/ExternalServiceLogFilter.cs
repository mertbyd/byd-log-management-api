using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.LogManagement.Consts;

namespace Byd.LogManagement.Dtos.ExternalServiceLogs;

/// <summary>
/// Dis servis cagri kaydi okuma uclarinin filtre alanlarini tasir.
/// </summary>
// islevi: Her filtre propertysini kararli tel adi ve entity property yoluyla DataTable motoruna tanitir.
// sistemdeki gorevi: Dis servis cagri kaydi liste ve sayim uclarinin tipli filtre izin listesidir.
public class ExternalServiceLogFilter
{
    [DataTable(ExternalServiceLogFilterFields.TenantId, ExternalServiceLogFilterFields.PropertyPaths.TenantId)]
    public DataTableFilterItem? TenantId { get; set; }

    [DataTable(ExternalServiceLogFilterFields.OrganizationId, ExternalServiceLogFilterFields.PropertyPaths.OrganizationId)]
    public DataTableFilterItem? OrganizationId { get; set; }

    [DataTable(ExternalServiceLogFilterFields.CorrelationId, ExternalServiceLogFilterFields.PropertyPaths.CorrelationId)]
    public DataTableFilterItem? CorrelationId { get; set; }

    [DataTable(ExternalServiceLogFilterFields.ExternalServiceProviderName, ExternalServiceLogFilterFields.PropertyPaths.ExternalServiceProviderName)]
    public DataTableFilterItem? ExternalServiceProviderName { get; set; }

    [DataTable(ExternalServiceLogFilterFields.ModuleName, ExternalServiceLogFilterFields.PropertyPaths.ModuleName)]
    public DataTableFilterItem? ModuleName { get; set; }

    [DataTable(ExternalServiceLogFilterFields.MethodName, ExternalServiceLogFilterFields.PropertyPaths.MethodName)]
    public DataTableFilterItem? MethodName { get; set; }

    [DataTable(ExternalServiceLogFilterFields.RequestType, ExternalServiceLogFilterFields.PropertyPaths.RequestType)]
    public DataTableFilterItem? RequestType { get; set; }

    [DataTable(ExternalServiceLogFilterFields.StartTime, ExternalServiceLogFilterFields.PropertyPaths.StartTime)]
    public DataTableFilterItem? StartTime { get; set; }

    [DataTable(ExternalServiceLogFilterFields.ExecutionDuration, ExternalServiceLogFilterFields.PropertyPaths.ExecutionDuration)]
    public DataTableFilterItem? ExecutionDuration { get; set; }

    [DataTable(ExternalServiceLogFilterFields.ResponseCode, ExternalServiceLogFilterFields.PropertyPaths.ResponseCode)]
    public DataTableFilterItem? ResponseCode { get; set; }

    [DataTable(ExternalServiceLogFilterFields.CreationTime, ExternalServiceLogFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
