using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Byd.LogManagement.Consts;

namespace Byd.LogManagement.Extensions.Notifications;

/// <summary>
/// Bildirim hub'ina gelen istegin access token'ini sorgu parametresinden okutur.
/// </summary>
// islevi: Hub yolundaki istekte Authorization basligi yoksa token'i access_token sorgu parametresinden alir; host'un kurdugu
// JWT olaylarini ezmez, onlardan sonra calisir.
// sistemdeki gorevi: Tarayici WebSocket ve SSE baglantisinda baslik gonderemedigi icin hub token'i ancak sorgu parametresiyle
// alabilir; hub'in sahibi Collector modulu oldugu icin bu kural host'a yazdirilmaz.
public static class NotificationHubAuthenticationExtensions
{
    // Tum JWT semalarina, hub istegindeki sorgu parametresini token olarak okuyan adimi ekler.
    public static IServiceCollection AddNotificationHubAccessToken(this IServiceCollection services)
    {
        return services.PostConfigureAll<JwtBearerOptions>(options =>
        {
            var previous = options.Events.OnMessageReceived;
            options.Events.OnMessageReceived = async context =>
            {
                await previous(context);
                if (context.Token is null
                    && context.Request.Path.StartsWithSegments(NotificationRoutes.Hub)
                    && context.Request.Query.TryGetValue(NotificationRoutes.HubAccessTokenQueryName, out var token))
                    context.Token = token;
            };
        });
    }
}
