using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Routing;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Hubs.Notifications;

namespace Byd.LogManagement.Extensions.Notifications;

/// <summary>
/// Web bildirim hub'ini endpoint agacina ekler.
/// </summary>
// islevi: Kimligi dogrulanmis istemcinin baglandigi SignalR hub'ini, host'un sectigi tasimayla (SSE ya da WebSocket)
// esler.
// sistemdeki gorevi: Collector modulunun hub'ini host'a yazdirmadan ekledigi tek noktadir; tasimayi SignalR tahmin
// etmez, yalniz secilen tasima acilir.
public static class NotificationEndpointRouteBuilderExtensions
{
    // Hub'i yalniz secilen tasimayla esler; tanimsiz tasima adi uygulama acilisinda hata verir.
    public static IEndpointRouteBuilder MapNotificationWebEndpoints(
        this IEndpointRouteBuilder endpoints,
        string transportName)
    {
        var transport = transportName switch
        {
            NotificationTransportNames.ServerSentEvents => HttpTransportType.ServerSentEvents,
            NotificationTransportNames.WebSocket => HttpTransportType.WebSockets,
            _ => throw new ArgumentOutOfRangeException(nameof(transportName), transportName, null)
        };

        endpoints.MapHub<NotificationWebHub>(NotificationRoutes.Hub, options => options.Transports = transport);

        return endpoints;
    }
}
