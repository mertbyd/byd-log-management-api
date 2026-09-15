using System;
using Byd.LogManagement.ActivityLogs;

namespace Byd.LogManagement.Dtos.ActivityLogs;

/// <summary>
/// Merkezi etkinlik kaydinin public cevap alanlarini tasir.
/// </summary>
// islevi: Etkinligin kapsamini, kullanicisini, is kaydini, uygulamasini ve istek dilinde kurulmus cumlesini HTTP ciktisina acar.
// sistemdeki gorevi: Entity veya EF tiplerini disariya sizdirmayan response sozlesmesidir.
public class ActivityLogDto
{
    public Guid Id { get; set; }
    public Guid? TenantId { get; set; }
    public string? TenantName { get; set; }
    public Guid? OrganizationId { get; set; }
    public string? OrganizationName { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = default!;
    public Guid? ExternalId { get; set; }
    public string ApplicationName { get; set; } = default!;
    public ActivityKind Kind { get; set; }
    public string Description { get; set; } = default!;
    public DateTime CreationTime { get; set; }
}
