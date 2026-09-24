using Byd.LogManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.LogManagement.Permissions;

// islevi: Firebase projesi yetkilerini ABP yetki agacina ekler.
// sistemdeki gorevi: Ana provider'in Define metodu bu partial metodu cagirir.
public partial class LogManagementPermissionDefinitionProvider
{
    private void AddFirebaseProjectsPermissions(PermissionGroupDefinition group)
    {
        var firebaseProjects = group.AddPermission(
            LogManagementPermissions.FirebaseProjects.Default,
            L(LogManagementLocalizationKeys.Permissions.FirebaseProjects));

        firebaseProjects.AddChild(
            LogManagementPermissions.FirebaseProjects.View,
            L(LogManagementLocalizationKeys.Permissions.FirebaseProjectsView));

        firebaseProjects.AddChild(
            LogManagementPermissions.FirebaseProjects.Manage,
            L(LogManagementLocalizationKeys.Permissions.FirebaseProjectsManage));
    }
}
