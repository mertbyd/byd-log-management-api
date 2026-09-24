namespace Byd.LogManagement.Permissions;

/// <summary>
/// Firebase projesi lookup'inin yetki sabitlerini tanimlar.
/// </summary>
// islevi: Firebase projesi okuma ve yonetim yetkilerinin string sabitlerini tanimlar.
// sistemdeki gorevi: Controller action'lari yetki string'ini buradan okur; inline string kullanilmaz.
public partial class LogManagementPermissions
{
    public static class FirebaseProjects
    {
        public const string Default = GroupName + ".FirebaseProjects";

        // Firebase projesi listeleme ve detay okuma yetkisi.
        public const string View = Default + ".View";

        // Firebase projesi olusturma, guncelleme ve silme yetkisi.
        public const string Manage = Default + ".Manage";
    }
}
