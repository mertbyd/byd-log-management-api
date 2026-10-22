using System;

namespace Byd.LogManagement.Dtos.ExternalServiceLogs;

/// <summary>
/// Merkezi dis servis cagri kaydinin public cevap alanlarini tasir.
/// </summary>
// islevi: Cagrinin kapsamini, hedefini, sonucunu, suresini ve hata alanlarini HTTP ciktisina acar.
// sistemdeki gorevi: Entity veya EF tiplerini disariya sizdirmayan response sozlesmesidir.
public class ExternalServiceLogDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid? TenantId { get; set; }
    public Guid? OrganizationId { get; set; }

    // Table Columns / Data
    public string? TenantName { get; set; }
    public string? OrganizationName { get; set; }
    public string? CorrelationId { get; set; }
    public string ExternalServiceProviderName { get; set; } = default!;
    public string ModuleName { get; set; } = default!;
    public string MethodName { get; set; } = default!;
    public string RequestType { get; set; } = default!;
    public DateTime StartTime { get; set; }
    public double ExecutionDuration { get; set; }
    public int ResponseCode { get; set; }
    public string? ExceptionMessage { get; set; }
    public string? ResponseData { get; set; }
    public DateTime CreationTime { get; set; }
}
