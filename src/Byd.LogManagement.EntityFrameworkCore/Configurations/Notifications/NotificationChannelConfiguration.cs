using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Entities.Notifications;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.LogManagement.EntityFrameworkCore.Configurations.Notifications;

/// <summary>
/// NotificationChannel entity'sinin EF Core tablo ve kolon sozlesmesini tanimlar.
/// </summary>
// islevi: Lookup tablo adini, semasini, ad sinirini ve kod benzersizligini map eder.
// sistemdeki gorevi: NotificationChannel tablosunun PostgreSQL semasindaki tek EF konfigurasyon sahibidir.
public sealed class NotificationChannelConfiguration : IEntityTypeConfiguration<NotificationChannel>
{
    // ABP convention alanlarindan sonra lookup kolon sozlesmesini uygular.
    public void Configure(EntityTypeBuilder<NotificationChannel> builder)
    {
        builder.ToTable(NotificationConsts.ChannelTableName, LogManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.Property(entity => entity.Name)
            .HasMaxLength(NotificationConsts.MaxLookupNameLength);

        // Is kodu, uretici uygulamanin lookup satirini adresledigi anahtardir; ayni kodun ikinci kez
        // acilmasi olaylari fark edilmeden iki satira bolerdi.
        builder.HasIndex(entity => entity.Code)
            .IsUnique();
    }
}
