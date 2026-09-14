using Byd.LogManagement.Consts;
using Volo.Abp.AuditLogging;

namespace Byd.LogManagement.Extensions;

/// <summary>
/// ABP audit log tablolarinin sema ve tablo on ekini modulun sozlesmesine gore ezer.
/// </summary>
// islevi: AbpAuditLoggingDbProperties degerlerini model kurulmadan once log modulunun semasina ve bos on eke yonlendirir.
// sistemdeki gorevi: Audit tablolari log modulunun verisidir; core ve file modullerinin kendi semalari gibi bu tablolar
// da log_management semasinda durur ve adlari modulun migration'indaki adlarla ayni kalir. Karar tuketen host'a
// birakilmaz: ABP bu degerleri sinifa ilk erisildigi anda AbpCommonDbProperties'ten kopyalar; host sinifa erken
// erisirse on ek "Abp" olarak kalir ve modulun context'i migration'in kurmadigi bir tabloya yazmaya calisir. ABP'de
// HideErrors acik oldugu icin bu hata sessizce yutulur. Modulun EF modulu acilista, design-time fabrikasi da context'i
// kurmadan once bu cozumu cagirir.
public static class ServiceConfigurationContextExtension
{
    // Audit log tablolarini log modulunun semasina alir; tablo on ekini kaldirir.
    public static void ResolveSchemaAndPrefix()
    {
        AbpAuditLoggingDbProperties.DbSchema = DatabaseConsts.ModuleSchema;
        AbpAuditLoggingDbProperties.DbTablePrefix = string.Empty;
    }
}
