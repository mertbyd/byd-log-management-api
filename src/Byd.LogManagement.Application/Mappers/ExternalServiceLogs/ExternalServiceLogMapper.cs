using Byd.LogManagement.Dtos.ExternalServiceLogs;
using Byd.LogManagement.Entities.ExternalServiceLogs;
using Riok.Mapperly.Abstractions;

namespace Byd.LogManagement.Mappers.ExternalServiceLogs;

/// <summary>
/// Dis servis cagri kaydi entity ve DTO eslemelerini tanimlar.
/// </summary>
// islevi: Dis servis cagri satirini cevaba Mapperly tarafindan uretilen kodla donusturur.
// sistemdeki gorevi: Application katmanindaki dis servis cagri kaydi cevap eslemesinin sahibidir.
[Mapper]
public partial class ExternalServiceLogMapper
{
    // Dis servis cagri satirini cevap DTO'suna esler.
    public partial ExternalServiceLogDto Map(ExternalServiceLog source);
}
