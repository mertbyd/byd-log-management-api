using Byd.LogManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Byd.LogManagement.Permissions;

/// <summary>
/// Modulun yetki agacini ABP'ye tanimlar.
/// </summary>
// islevi: Yetki grubunu acar ve her ozellik ailesinin kendi partial metodunu cagirir.
// sistemdeki gorevi: Yeni bir ozellik ailesi eklemek bu dosyaya tek satir eklemekten ibarettir;
// mevcut satirlar degismez.
public partial class LogManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var group = context.AddGroup(
            LogManagementPermissions.GroupName,
            L(LogManagementLocalizationKeys.Permissions.Group));

        AddActivityLogsPermissions(group);
        AddExternalServiceLogsPermissions(group);
    }

    // Localization anahtarini modul kaynagina baglar.
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LogManagementResource>(name);
    }
}
