using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Entities.Notifications;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.LogManagement.EntityFrameworkCore.Configurations.Notifications;

/// <summary>
/// FirebaseProject entity'sinin EF Core tablo ve kolon sozlesmesini tanimlar.
/// </summary>
// islevi: Lookup tablo adini, semasini, ad sinirini ve tenant icinde ad/kod benzersizligini map eder.
// sistemdeki gorevi: FirebaseProject tablosunun PostgreSQL semasindaki tek EF konfigurasyon sahibidir.
public sealed class FirebaseProjectConfiguration : IEntityTypeConfiguration<FirebaseProject>
{
    // ABP convention alanlarindan sonra lookup kolon sozlesmesini uygular.
    public void Configure(EntityTypeBuilder<FirebaseProject> builder)
    {
        builder.ToTable(NotificationConsts.FirebaseProjectTableName, LogManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.Property(entity => entity.Name)
            .HasMaxLength(NotificationConsts.MaxLookupNameLength);

        builder.Property(entity => entity.Key)
            .HasMaxLength(NotificationConsts.MaxFirebaseProjectKeyLength);

        builder.Property(entity => entity.Environment)
            .HasMaxLength(NotificationConsts.MaxEnvironmentLength);

        // Ad ve anahtar tenant ile ortam icinde tektir; Manager ayni kurali is hatasi olarak verir.
        builder.HasIndex(entity => new { entity.TenantId, entity.Environment, entity.Name })
            .IsUnique();
        builder.HasIndex(entity => new { entity.TenantId, entity.Environment, entity.Key })
            .IsUnique();
    }
}
