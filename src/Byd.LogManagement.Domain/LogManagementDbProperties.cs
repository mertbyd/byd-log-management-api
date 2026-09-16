using Byd.LogManagement.Consts;

namespace Byd.LogManagement;

public static class LogManagementDbProperties
{
    public static string DbTablePrefix { get; set; } = "LogManagement";

    public static string? DbSchema { get; set; } = DatabaseConsts.ModuleSchema;

    public const string ConnectionStringName = "Default";
}
