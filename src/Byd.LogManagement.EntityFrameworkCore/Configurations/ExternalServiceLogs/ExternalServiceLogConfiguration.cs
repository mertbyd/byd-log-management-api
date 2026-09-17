using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Entities.ExternalServiceLogs;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.LogManagement.EntityFrameworkCore.Configurations.ExternalServiceLogs;

/// <summary>
/// ExternalServiceLog entity'sinin EF Core tablo sozlesmesini tanimlar.
/// </summary>
// islevi: Dis servis cagri kaydinin tablo adini ve semasini map eder.
// sistemdeki gorevi: ExternalServiceLogs tablosunun PostgreSQL semasindaki tek EF konfigurasyon sahibidir.
public sealed class ExternalServiceLogConfiguration : IEntityTypeConfiguration<ExternalServiceLog>
{
    // Dis servis cagri kaydini kendi semasindaki tabloya ve ABP convention alanlarina baglar.
    public void Configure(EntityTypeBuilder<ExternalServiceLog> builder)
    {
        builder.ToTable(ExternalServiceLogConsts.TableName, LogManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();
    }
}
