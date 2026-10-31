namespace Byd.LogManagement.Dtos.Notifications;

/// <summary>
/// Web istemcisine iletilen tek bir canli akis mesajinin alanlarini tasir.
/// </summary>
// islevi: Akisi ureten modulu, akisin adini ve JSON govdesini tasir.
// sistemdeki gorevi: SignalR hub'i uzerinden tarayiciya giden canli akis sozlesmesidir; akis organizasyon kapsamindaysa
// yalniz o organizasyonun baglantilarina gider. Govde JSON metnidir, istemci kendisi cozer.
public class NotificationStreamMessageDto
{
    // Table Columns / Data
    public string ModuleName { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Payload { get; set; } = default!;
}
