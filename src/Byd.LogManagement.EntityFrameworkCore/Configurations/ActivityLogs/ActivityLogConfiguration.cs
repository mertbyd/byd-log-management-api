using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Entities.ActivityLogs;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.LogManagement.EntityFrameworkCore.Configurations.ActivityLogs;

/// <summary>
/// ActivityLog entity'sinin EF Core tablo ve kolon sozlesmesini tanimlar.
/// </summary>
// islevi: Etkinlik kaydi tablo adini, semasini ve dil basina cumle kolonunu map eder.
// sistemdeki gorevi: ActivityLogs tablosunun PostgreSQL semasindaki tek EF konfigurasyon sahibidir.
public sealed class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
{
    // ABP convention alanlarindan sonra ActivityLog kolon sozlesmesini uygular.
    public void Configure(EntityTypeBuilder<ActivityLog> builder)
    {
        builder.ToTable(ActivityLogConsts.TableName, LogManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        // Dil -> hazir cumle sozlugu tek jsonb kolonunda tutulur; satir eklendikten sonra degismedigi icin referans karsilastirmasi yeterlidir.
        builder.Property(entity => entity.Description)
            .HasColumnType(ActivityLogConsts.DescriptionColumnType)
            .HasConversion(
                description => JsonSerializer.Serialize(description, (JsonSerializerOptions?)null),
                json => JsonSerializer.Deserialize<Dictionary<string, string>>(json, (JsonSerializerOptions?)null)!,
                new ValueComparer<Dictionary<string, string>>(false));
    }
}
