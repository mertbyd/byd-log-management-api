using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Routing;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Hubs.Notifications;

namespace Byd.LogManagement.Extensions.Notifications;

/// <summary>
/// Web bildirim hub'ini endpoint agacina ekler.
/// </summary>
// islevi: Kimligi dogrulanmis istemcinin baglandigi SignalR hub'ini WebSocket ve SSE tasimalariyla esler.
// sistemdeki gorevi: Collector modulunun hub'ini host'a yazdirmadan ekledigi tek noktadir; tasimayi SignalR tahmin
// etmez, istemci once WebSocket'i dener, acamazsa SSE ile baglanir.
public static class NotificationEndpointRouteBuilderExtensions
{
    // Hub'i WebSocket ve SSE tasimalariyla esler.
    public static IEndpointRouteBuilder MapNotificationWebEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHub<NotificationWebHub>(
            NotificationRoutes.Hub,
            options => options.Transports = HttpTransportType.WebSockets | HttpTransportType.ServerSentEvents);

        return endpoints;
    }
}
