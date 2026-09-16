using System;
using Byd.LogManagement.ActivityLogs;

namespace Byd.LogManagement.Attributes;

/// <summary>
/// Action basariyla bittiginde etkinlik kaydi uretilecegini bildirir.
/// </summary>
// islevi: Uretici modul dil dosyalarindaki kalip anahtarini, etkinlik turunu ve {1}'den itibaren doldurulacak alan adlarini tasir.
// sistemdeki gorevi: ActivityTrackingFilter'in action uzerinden okudugu beyandir.
[AttributeUsage(AttributeTargets.Method)]
public abstract class TrackActivityAttribute(string localizationKey, ActivityKind kind, string[] values) : Attribute
{
    public string LocalizationKey { get; } = localizationKey;
    public ActivityKind Kind { get; } = kind;
    public string[] Values { get; } = values;

    // Kalibin okunacagi uretici modul localization kaynagi.
    public abstract Type ResourceType { get; }
}

/// <summary>
/// Kalibi verilen localization kaynagindan okunan etkinlik beyanidir.
/// </summary>
// islevi: [TrackActivity<InventoryResource>(InventoryActivities.StockCreated, ActivityKind.Created, nameof(StockDto.Name))] yazimini saglar.
// sistemdeki gorevi: Controller action'larina konan tek etkinlik attribute'udur.
public sealed class TrackActivityAttribute<TResource>(string localizationKey, ActivityKind kind, params string[] values)
    : TrackActivityAttribute(localizationKey, kind, values)
{
    public override Type ResourceType => typeof(TResource);
}
