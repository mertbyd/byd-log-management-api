using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Entities.Notifications;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.LogManagement.EntityFrameworkCore.Configurations.Notifications;

/// <summary>
/// MobileNotification entity'sinin EF Core tablo ve kolon sozlesmesini tanimlar.
/// </summary>
// islevi: Tablo adini, semasini, kolon sinirlarini ve bildirim satirina bagini map eder.
// sistemdeki gorevi: MobileNotifications tablosunun PostgreSQL semasindaki tek EF konfigurasyon sahibidir.
public sealed class MobileNotificationConfiguration : IEntityTypeConfiguration<MobileNotification>
{
    // ABP convention alanlarindan sonra mobil gonderim gunlugu kolon sozlesmesini uygular.
    public void Configure(EntityTypeBuilder<MobileNotification> builder)
    {
        builder.ToTable(NotificationConsts.MobileNotificationTableName, LogManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.Property(entity => entity.Target)
            .HasMaxLength(NotificationConsts.MaxTargetLength);
        builder.Property(entity => entity.StatusCode)
            .HasMaxLength(NotificationConsts.MaxStatusCodeLength);
        builder.Property(entity => entity.ErrorMessage)
            .HasMaxLength(NotificationConsts.MaxErrorMessageLength);
        // Gunluk bildirim satirina aittir; satir silinirse gonderim sonuclari da silinir.
        builder.HasOne<Notification>()
            .WithMany()
            .HasForeignKey(entity => entity.NotificationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
