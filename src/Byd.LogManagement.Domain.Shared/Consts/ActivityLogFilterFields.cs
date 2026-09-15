namespace Byd.LogManagement.Consts;

/// <summary>
/// Etkinlik kaydi filtre yuzeyinde disariya acilan alan adlari.
/// </summary>
// islevi: DataTable izin listesindeki tel alanlarini ve entity property yollarini derleme guvenli sabitler olarak tanimlar.
// sistemdeki gorevi: Filtre DTO'su ile repository sorgu motoru arasindaki kararli alan adi sozlesmesidir.
public static class ActivityLogFilterFields
{
    public const string UserId = "userId";
    public const string ExternalId = "externalId";
    public const string ApplicationName = "applicationName";
    public const string Kind = "kind";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string UserId = "UserId";
        public const string ExternalId = "ExternalId";
        public const string ApplicationName = "ApplicationName";
        public const string Kind = "Kind";
        public const string CreationTime = "CreationTime";
    }
}
