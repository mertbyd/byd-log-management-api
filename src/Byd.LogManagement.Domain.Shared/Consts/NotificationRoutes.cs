namespace Byd.LogManagement.Consts;

/// <summary>
/// Web bildirim hub'inin kararli rota sabitini tanimlar.
/// </summary>
// islevi: Tarayicinin baglandigi SignalR hub rotasini tek merkezde tutar; SSE tasimasi bu rotadan acilir.
// sistemdeki gorevi: Hub eslemesi ve istemci cagrilari icin tek rota kaynagidir.
public static class NotificationRoutes
{
    // SignalR bildirim hub'inin baglanti yoludur.
    public const string Hub = "/signalr-hubs/log-management/notifications";
}
