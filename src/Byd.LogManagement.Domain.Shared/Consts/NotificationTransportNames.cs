namespace Byd.LogManagement.Consts;

/// <summary>
/// Bildirim hub'inin kullanabilecegi tasimalarin kararli isimlerini tanimlar.
/// </summary>
// islevi: SignalR hub'inin altinda acilabilecek SSE ve WebSocket tasimalarinin kararli adlarini tek merkezde tutar.
// sistemdeki gorevi: Host'un beyani ile hub eslemesinin ayni adlari kullanmasini saglar.
public static class NotificationTransportNames
{
    // Server-Sent Events tasimasinin adidir.
    public const string ServerSentEvents = "sse";

    // WebSocket tasimasinin adidir.
    public const string WebSocket = "websocket";
}
