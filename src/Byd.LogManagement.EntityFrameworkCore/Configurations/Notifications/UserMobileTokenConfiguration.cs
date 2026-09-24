using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Entities.Notifications;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.LogManagement.EntityFrameworkCore.Configurations.Notifications;

/// <summary>
/// UserMobileToken entity'sinin EF Core tablo ve kolon sozlesmesini tanimlar.
/// </summary>
// islevi: Tablo adini, semasini, kolon sinirlarini, kullanici-proje tekilligini ve Firebase projesi bagini map eder.
// sistemdeki gorevi: UserMobileTokens tablosunun PostgreSQL semasindaki tek EF konfigurasyon sahibidir.
public sealed class UserMobileTokenConfiguration : IEntityTypeConfiguration<UserMobileToken>
{
    // ABP convention alanlarindan sonra mobil token kolon sozlesmesini uygular.
    public void Configure(EntityTypeBuilder<UserMobileToken> builder)
    {
        builder.ToTable(NotificationConsts.UserMobileTokenTableName, LogManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.Property(entity => entity.Token)
            .HasMaxLength(NotificationConsts.MaxDeviceTokenLength);

        builder.Property(entity => entity.LanguageCode)
            .HasMaxLength(NotificationConsts.MaxLanguageCodeLength);

        // Kullanicinin bir Firebase projesinde tek bir aktif token satiri olur; yeniden kayitta token guncellenir, indeks son savunmadir.
        builder.HasIndex(entity => new { entity.UserId, entity.FirebaseProjectId })
            .IsUnique();

        // Proje silinince token kayitlari da silinir.
        builder.HasOne<FirebaseProject>()
            .WithMany()
            .HasForeignKey(entity => entity.FirebaseProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
