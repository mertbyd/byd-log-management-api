namespace Byd.LogManagement.Consts;

/// <summary>
/// Dis servis cagri kaydi filtre yuzeyinde disariya acilan alan adlarini tanimlar.
/// </summary>
// islevi: DataTable tel alanlarini ve entity property yollarini derleme guvenli sabitlerde tutar.
// sistemdeki gorevi: Filtre DTO'su ile repository sorgu motoru arasindaki kararli alan adi sozlesmesidir.
public static class ExternalServiceLogFilterFields
{
    public const string TenantId = "tenantId";
    public const string OrganizationId = "organizationId";
    public const string CorrelationId = "correlationId";
    public const string ExternalServiceProviderName = "externalServiceProviderName";
    public const string ModuleName = "moduleName";
    public const string MethodName = "methodName";
    public const string RequestType = "requestType";
    public const string StartTime = "startTime";
    public const string ExecutionDuration = "executionDuration";
    public const string ResponseCode = "responseCode";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string TenantId = "TenantId";
        public const string OrganizationId = "OrganizationId";
        public const string CorrelationId = "CorrelationId";
        public const string ExternalServiceProviderName = "ExternalServiceProviderName";
        public const string ModuleName = "ModuleName";
        public const string MethodName = "MethodName";
        public const string RequestType = "RequestType";
        public const string StartTime = "StartTime";
        public const string ExecutionDuration = "ExecutionDuration";
        public const string ResponseCode = "ResponseCode";
        public const string CreationTime = "CreationTime";
    }
}
