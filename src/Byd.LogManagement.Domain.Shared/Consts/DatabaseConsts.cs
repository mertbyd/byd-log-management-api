namespace Byd.LogManagement.Consts;

/// <summary>
/// Modulun veritabani sema adlarini tanimlar.
/// </summary>
// islevi: Log modulunun kendi tablolarinin yazildigi sema adini derleme guvenli tek sabitte tutar.
// sistemdeki gorevi: Sema adinin tek kaynagidir; DbProperties ve EF konfigurasyonlari ayni sabiti okur, core semasina dokunmaz.
public static class DatabaseConsts
{
    // Log modulunun kendi tablolarinin yazildigi sema.
    public const string ModuleSchema = "log_management";
}
