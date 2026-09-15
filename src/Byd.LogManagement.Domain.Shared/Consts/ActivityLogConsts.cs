using System.Collections.Generic;

namespace Byd.LogManagement.Consts;

/// <summary>
/// Etkinlik kaydinin tablo ve kolon sabitlerini tanimlar.
/// </summary>
// islevi: Etkinlik kaydinin kalicilik sabitlerini tek yerde tutar.
// sistemdeki gorevi: EF konfigurasyonunun ActivityLogs tablosu icin kullandigi ortak sabit sozlesmesidir.
public static class ActivityLogConsts
{
    // Etkinlik kayitlarinin tutuldugu tablonun EF model adidir.
    public const string TableName = "ActivityLogs";

    // ABP'nin kendi adini verdigi olusturma zamani kolonunun snake_case adi.
    public const string CreationTimeColumnName = "creation_time";

    // ABP'nin kendi adini verdigi olusturan kullanici kolonunun snake_case adi.
    public const string CreatorIdColumnName = "creator_id";

    // Dil -> hazir cumle sozlugunun saklandigi kolon tipi.
    public const string DescriptionColumnType = "jsonb";

    // Etkinlik cumlesinin kurulup saklandigi diller.
    public static readonly IReadOnlyList<string> DescriptionCultureNames = ["tr", "en"];
}
