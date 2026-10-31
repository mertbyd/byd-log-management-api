using Byd.LogManagement.Consts;
using Volo.Abp.Settings;

namespace Byd.LogManagement.Settings;

public class LogManagementSettingDefinitionProvider : SettingDefinitionProvider
{
    // Audit kaydi varsayilan olarak veritabanina yazilir; host ya da ayar tablosu bu degeri ezer.
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(new SettingDefinition(LogManagementSettings.AuditLogSaveMode, AuditLoggingConsts.DatabaseSaveMode));
    }
}
