using Byd.LogManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.LogManagement.Permissions;

// islevi: Etkinlik kaydi yetkilerini ABP yetki agacina ekler.
// sistemdeki gorevi: Ana provider'in Define metodu bu partial metodu cagirir.
public partial class LogManagementPermissionDefinitionProvider
{
    private void AddActivityLogsPermissions(PermissionGroupDefinition group)
    {
        var activityLogs = group.AddPermission(
            LogManagementPermissions.ActivityLogs.Default,
            L(LogManagementLocalizationKeys.Permissions.ActivityLogs));

        activityLogs.AddChild(
            LogManagementPermissions.ActivityLogs.View,
            L(LogManagementLocalizationKeys.Permissions.ActivityLogsView));
    }
}
