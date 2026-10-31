using Byd.LogManagement.Entities.ExternalServiceLogs;
using Byd.LogManagement.Etos.ExternalServiceLogs;

namespace Byd.LogManagement.Extensions.ExternalServiceLogs;

/// <summary>
/// Dis servis cagri olayini merkezi log satirina donusturur.
/// </summary>
// islevi: ExternalServiceLogRecordedEto alanlarini yeni bir ExternalServiceLog satirina elle tasir.
// sistemdeki gorevi: Olaydan satir kurmanin tek yeridir.
public static class ExternalServiceLogRecordedEtoExtensions
{
    // Olayin cagri alanlarini yeni satira tasir; kimligi ve olusturma zamanini ABP ekleme aninda verir.
    public static ExternalServiceLog ToExternalServiceLog(this ExternalServiceLogRecordedEto source)
    {
        return new ExternalServiceLog
        {
            TenantId = source.TenantId,
            OrganizationId = source.OrganizationId,
            CorrelationId = source.CorrelationId,
            TenantName = source.TenantName,
            OrganizationName = source.OrganizationName,
            ExternalServiceProviderName = source.ExternalServiceProviderName,
            ModuleName = source.ModuleName,
            MethodName = source.MethodName,
            RequestType = source.RequestType,
            StartTime = source.StartTime,
            ExecutionDuration = source.ExecutionDuration,
            ResponseCode = source.ResponseCode,
            ExceptionMessage = source.ExceptionMessage,
            ResponseData = source.ResponseData
        };
    }
}
