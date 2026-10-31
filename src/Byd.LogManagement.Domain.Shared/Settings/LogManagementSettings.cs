namespace Byd.LogManagement.Settings;

/// <summary>
/// Modulun ayar adlarini tanimlar.
/// </summary>
// islevi: Ayar grubunun adini ve audit kaydinin yazma yerini secen ayarin adini tanimlar.
// sistemdeki gorevi: Ayar tanim saglayicisi ve audit store ayar adini buradan okur; inline string kullanilmaz.
public static class LogManagementSettings
{
    public const string GroupName = "LogManagement";

    // Audit kaydinin nereye yazilacagini secen ayar; degerler AuditLoggingConsts icindeki yazma yerleridir.
    public const string AuditLogSaveMode = GroupName + ".AuditLogSaveMode";
}
