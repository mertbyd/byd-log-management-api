namespace Byd.LogManagement.Consts;

/// <summary>
/// Dis servis kaydi HTTP rotalarini tanimlar.
/// </summary>
// islevi: Dis servis kaydi okuma endpointlerinin kararli rota sozlesmesini tek yerde tutar.
// sistemdeki gorevi: Controller ile istemcilerin ayni derleme guvenli route degerlerini kullanmasini saglar.
public static class ExternalServiceLogRoutes
{
    public const string Root = "api/log-management/external-service-logs";
    public const string FilteredList = "filtered-list";
    public const string FilteredCount = "filtered-count";
}
