namespace Byd.LogManagement.Permissions;

/// <summary>
/// Etkinlik kaydi okumasinin yetki sabitlerini tanimlar.
/// </summary>
// islevi: Etkinlik kaydi okuma yetkisinin string sabitini tanimlar.
// sistemdeki gorevi: Controller action'lari yetki string'ini buradan okur; inline string kullanilmaz.
public partial class LogManagementPermissions
{
    public static class ActivityLogs
    {
        public const string Default = GroupName + ".ActivityLogs";

        // Etkinlik kayitlarinin filtreli listesini ve sayimini okuma yetkisi.
        public const string View = Default + ".View";
    }
}
