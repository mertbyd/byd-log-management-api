namespace Byd.LogManagement.Localization;

// islevi: Modul localization kaynaklarinda kod tarafindan kullanilan kararli anahtarlari tanimlar.
// sistemdeki gorevi: Izin tanimlarinin localization JSON anahtarlariyla compile-time tek sozlesmede
// bulusmasini saglar; provider'lar inline string tasimaz.
public static class LogManagementLocalizationKeys
{
    public static class Permissions
    {
        public const string Group = "Permission:LogManagement";
        public const string ActivityLogs = "Permission:ActivityLogs";
        public const string ActivityLogsView = "Permission:ActivityLogs.View";
        public const string ExternalServiceLogs = "Permission:ExternalServiceLogs";
        public const string ExternalServiceLogsView = "Permission:ExternalServiceLogs.View";
    }
}
