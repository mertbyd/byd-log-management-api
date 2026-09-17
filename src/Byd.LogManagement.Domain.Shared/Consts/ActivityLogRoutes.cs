namespace Byd.LogManagement.Consts;

/// <summary>
/// Etkinlik kaydi HTTP rotalarini tanimlar.
/// </summary>
// islevi: Etkinlik kaydi okuma endpointlerinin kararli rota sozlesmesini tek yerde tutar.
// sistemdeki gorevi: Controller ile istemcilerin ayni derleme guvenli route degerlerini kullanmasini saglar.
public static class ActivityLogRoutes
{
    public const string Root = "api/log-management/activity-logs";
    public const string FilteredList = "filtered-list";
}
