using System;
using System.Collections.Generic;
using System.Globalization;
using Byd.CoreExtensions.Managers;
using Byd.LogManagement.Entities.ActivityLogs;
using Byd.LogManagement.Interfaces.ActivityLogs;

namespace Byd.LogManagement.Managers.ActivityLogs;

/// <summary>
/// Etkinlik kaydinin aciklama secme kuralini yonetir.
/// </summary>
// islevi: Satirdaki dil basina hazir cumleler arasindan istek dilininkini secer.
// sistemdeki gorevi: AppService'i ve mapper'i dil secme kararindan uzak tutan domain sahibidir.
public class ActivityLogManager : BaseManager<ActivityLog, Guid>
{
    // ActivityLog extended repository'sini ortak guard tabanina devreder.
    public ActivityLogManager(IActivityLogRepository repository)
        : base(repository)
    {
    }

    // Istek dilinin hazir cumlesini secer; once tam kultur adina, sonra iki harfli dile bakar.
    public virtual string Describe(ActivityLog activityLog)
    {
        var culture = CultureInfo.CurrentUICulture;
        return activityLog.Description.GetValueOrDefault(culture.Name)
            ?? activityLog.Description.GetValueOrDefault(culture.TwoLetterISOLanguageName)
            ?? string.Empty;
    }
}
