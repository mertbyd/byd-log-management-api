namespace Byd.LogManagement;

/// <summary>
/// Modulun is hatasi kod grameri.
/// </summary>
// islevi: Kok namespace'i ve ozellik ailesi basina numara grubunu tanimlar.
// sistemdeki gorevi: ABP bir BusinessException mesajini kodun ilk iki nokta ust uste oncesini birebir arayarak
// cozer; bu yuzden her kod tam olarak modulun MapCodeNamespace ile kaydettigi kok ile baslamak zorundadir.
// Aile namespace'te degil numarada tasinir: "LogManagement:01" gibi.
public static class LogManagementErrorCodes
{
    // Modulun Domain.Shared modulunde MapCodeNamespace ile kaydedilen kok namespace.
    public const string RootNamespace = "LogManagement";

    // Bildirim ailesinin numara grubu.
    public const string NotificationGroup = RootNamespace + ":01";
}
