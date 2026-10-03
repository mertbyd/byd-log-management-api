using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.LogManagement.Dtos.ExternalServiceLogs;

namespace Byd.LogManagement.Interfaces.ExternalServiceLogs;

/// <summary>
/// Bir dis servis cagrisini kaydederek calistirir.
/// </summary>
// islevi: Cagriyi sure olcerek calistirir, yanitini ve sonucunu merkezi log servisine birakir, hatayi oldugu gibi
// yukari birakir.
// sistemdeki gorevi: Dis servis cagrisinin yakalandigi tek noktadir; bir cagrinin is anlamini ve ham yanitini
// yalniz cagiran bildigi icin beyan cagiran tarafta kalir.
public interface IExternalServiceLogger
{
    // Cagriyi kaydederek calistirir ve dis servisin ham yanitini dondurur.
    Task<string> TrackAsync(
        ExternalServiceCall call,
        Func<Task<string>> work,
        CancellationToken cancellationToken = default);
}
