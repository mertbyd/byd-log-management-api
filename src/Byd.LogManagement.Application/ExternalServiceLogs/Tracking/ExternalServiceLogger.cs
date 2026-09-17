using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Byd.CoreExtensions.Providers;
using Byd.LogManagement.Dtos.ExternalServiceLogs;
using Byd.LogManagement.Etos.ExternalServiceLogs;
using Byd.LogManagement.Interfaces.ExternalServiceLogs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Timing;
using Volo.Abp.Tracing;

namespace Byd.LogManagement.ExternalServiceLogs.Tracking;

/// <summary>
/// Dis servis cagrisini calistirir ve sonucunu merkezi log servisine birakir.
/// </summary>
// islevi: Cagriyi sure olcerek calistirir ve cagri nasil biterse bitsin sonucu tek seferde olaya cevirip yayinlar.
// sistemdeki gorevi: Uretici uygulamada dis servis cagrisinin yakalandigi tek noktadir.
public class ExternalServiceLogger(
    IExternalServiceLogStore externalServiceLogStore,
    CurrentModuleNameProvider moduleNameProvider,
    ICorrelationIdProvider correlationIdProvider,
    IClock clock) : IExternalServiceLogger, ITransientDependency
{
    // Cagriyi calistirir; yanitini ve suresini merkezi log servisine birakir, istisnayi oldugu gibi yukari verir.
    public virtual async Task<string> TrackAsync(ExternalServiceCall call, Func<Task<string>> work)
    {
        var startTime = clock.Now;
        var startedAt = Stopwatch.GetTimestamp();
        try
        {
            var response = await work();
            await RecordAsync(call, startTime, startedAt, response, exception: null);
            return response;
        }
        catch (Exception exception)
        {
            await RecordAsync(call, startTime, startedAt, response: null, exception);
            throw;
        }
    }

    // Cagrinin hedefini, suresini ve sonucunu tek olayda toplayip yayinlar.
    private Task RecordAsync(
        ExternalServiceCall call,
        DateTime startTime,
        long startedAt,
        string? response,
        Exception? exception) =>
        externalServiceLogStore.SaveAsync(new ExternalServiceLogRecordedEto
        {
            CorrelationId = correlationIdProvider.Get(),
            ExternalServiceProviderName = call.ProviderName,
            ModuleName = moduleNameProvider.Value,
            MethodName = call.MethodName,
            RequestType = call.RequestType,
            StartTime = startTime,
            ExecutionDuration = Math.Round(Stopwatch.GetElapsedTime(startedAt).TotalSeconds, 2),
            ResponseCode = ResolveResponseCode(exception),
            ExceptionMessage = exception?.Message,
            ResponseData = response
        });

    // Cagri basariliysa 200, dis servis bir durum kodu dondurduyse onu, dondurmediyse sunucu hatasini yazar.
    private static int ResolveResponseCode(Exception? exception) => exception switch
    {
        null => (int)HttpStatusCode.OK,
        HttpRequestException { StatusCode: { } statusCode } => (int)statusCode,
        _ => (int)HttpStatusCode.InternalServerError
    };
}
