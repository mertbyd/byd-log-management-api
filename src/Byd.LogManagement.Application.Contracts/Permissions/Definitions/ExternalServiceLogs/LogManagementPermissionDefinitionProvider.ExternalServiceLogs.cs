using Byd.LogManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.LogManagement.Permissions;

// islevi: Dis servis cagri kaydi yetkilerini ABP yetki agacina ekler.
// sistemdeki gorevi: Ana provider'in Define metodu bu partial metodu cagirir.
public partial class LogManagementPermissionDefinitionProvider
{
    private void AddExternalServiceLogsPermissions(PermissionGroupDefinition group)
    {
        var externalServiceLogs = group.AddPermission(
            LogManagementPermissions.ExternalServiceLogs.Default,
            L(LogManagementLocalizationKeys.Permissions.ExternalServiceLogs));

        externalServiceLogs.AddChild(
            LogManagementPermissions.ExternalServiceLogs.View,
            L(LogManagementLocalizationKeys.Permissions.ExternalServiceLogsView));
    }
}
