namespace Byd.LogManagement.Permissions;

/// <summary>
/// Dis servis cagri kaydi okumasinin yetki sabitlerini tanimlar.
/// </summary>
// islevi: Dis servis cagri kaydi okuma yetkisinin string sabitini tanimlar.
// sistemdeki gorevi: Controller action'lari yetki string'ini buradan okur; inline string kullanilmaz.
public partial class LogManagementPermissions
{
    public static class ExternalServiceLogs
    {
        public const string Default = GroupName + ".ExternalServiceLogs";

        // Dis servis cagri kayitlarinin filtreli listesini ve sayimini okuma yetkisi.
        public const string View = Default + ".View";
    }
}
