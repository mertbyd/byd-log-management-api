using Volo.Abp.Reflection;

namespace Byd.LogManagement.Permissions;

public class LogManagementPermissions
{
    public const string GroupName = "LogManagement";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(LogManagementPermissions));
    }
}
