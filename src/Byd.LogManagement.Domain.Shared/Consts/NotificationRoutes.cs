namespace Byd.LogManagement.Consts;

/// <summary>
/// Kullanici bildirimlerinin HTTP ve SignalR rotalarini tanimlar.
/// </summary>
// islevi: Bildirim listeleme ve okundu durumu endpointleri ile web bildirim hub'inin kararli rota sozlesmesini tek yerde tutar.
// sistemdeki gorevi: Controller ile istemcilerin ayni derleme guvenli route degerini kullanmasini saglar.
public static class NotificationRoutes
{
    // Kullanici bildirimleri endpoint ailesinin kok rotasidir.
    public const string Root = "api/log-management/notifications";

    // Oturum kullanicisinin gonderilmis bildirimlerini sayfalayan rotadir.
    public const string List = "my";

    // Kimligi verilen bildirimin okundu durumunu degistiren rota kalibidir.
    public const string ReadStatus = "{id:guid}/read-status/{isRead:bool}";

    // SignalR bildirim hub'inin baglanti yoludur.
    public const string Hub = "/signalr-hubs/log-management/notifications";
}
