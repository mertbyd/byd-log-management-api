using System;

namespace Byd.LogManagement.Dtos.ExternalServiceLogs;

/// <summary>
/// Merkezi dis servis cagri kaydinin public cevap alanlarini tasir.
/// </summary>
// islevi: Cagrinin hedefini, sonucunu, suresini ve hata alanlarini HTTP ciktisina acar.
// sistemdeki gorevi: Entity veya EF tiplerini disariya sizdirmayan response sozlesmesidir.
public class ExternalServiceLogDto
{
    public Guid Id { get; set; }
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
