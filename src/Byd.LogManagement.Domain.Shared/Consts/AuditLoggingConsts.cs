namespace Byd.LogManagement.Consts;

/// <summary>
/// Audit log kaydina modulun ekledigi ozelliklerin, yazma yerlerinin ve arama indeksinin kararli adlarini tanimlar.
/// </summary>
// islevi: ExtraProperties anahtarlarini, audit kaydinin yazilabilecegi yerleri ve Elasticsearch indeks adini tek yerde tutar.
// sistemdeki gorevi: Contributor, audit store ve arama repository'sinin ortak sozlesmesidir.
public static class AuditLoggingConsts
{
    // Kaydin organizasyon kapsamini tasiyan ozelligin kararli adi.
    public const string OrganizationIdPropertyName = "OrganizationId";

    // Kaydin organizasyon adini tasiyan ozelligin kararli adi.
    public const string OrganizationNamePropertyName = "OrganizationName";

    // Audit kaydinin yalniz veritabanina yazildigi yer; ayarin varsayilanidir.
    public const string DatabaseSaveMode = "Database";

    // Audit kaydinin yalniz Elasticsearch indeksine yazildigi yer.
    public const string ElasticsearchSaveMode = "Elasticsearch";

    // Audit kaydinin ayni kimlikle hem veritabanina hem Elasticsearch indeksine yazildigi yer.
    public const string BothSaveMode = "Both";

    // Audit kayitlarinin tutuldugu Elasticsearch indeksi; tablonun sema ve adini izler.
    public const string IndexName = "log_management_audit_logs";
}
