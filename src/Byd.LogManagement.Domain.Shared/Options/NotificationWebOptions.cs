namespace Byd.LogManagement.Options;

/// <summary>
/// Web bildiriminin SignalR hub'inda hangi tasimayla gidecegini tasir.
/// </summary>
// islevi: Hub'in actigi tek tasimanin (SSE ya da WebSocket) adini tasir.
// sistemdeki gorevi: Host kendi modulunde PreConfigure ile beyan eder, modulun varsayilani yoktur; hub uygulama
// acilirken eslendigi icin secim de acilista bilinir.
// TODO: Tek tasimaya karar verildiginde bu secenek silinir; hub eslemesinde tasima sabitlenir ve secilmeyen tasimanin
// sabiti kaldirilir.
public class NotificationWebOptions
{
    // Hub'in actigi tasimanin adi; NotificationTransportNames degerlerinden biridir.
    public string TransportName { get; set; } = default!;
}
