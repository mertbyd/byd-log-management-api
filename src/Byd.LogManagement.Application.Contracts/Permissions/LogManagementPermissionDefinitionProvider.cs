using Byd.LogManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Byd.LogManagement.Permissions;

public class LogManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(LogManagementPermissions.GroupName, L("Permission:LogManagement"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LogManagementResource>(name);
    }
}
