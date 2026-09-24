using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Entities.Notifications;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Byd.LogManagement.EntityFrameworkCore.Configurations.Notifications;

/// <summary>
/// Notification entity'sinin EF Core tablo ve kolon sozlesmesini tanimlar.
/// </summary>
// islevi: Bildirim tablo adini, semasini, dil basina metin kolonunu ve okuma indekslerini map eder.
// sistemdeki gorevi: Notifications tablosunun PostgreSQL semasindaki tek EF konfigurasyon sahibidir.
public sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    // ABP convention alanlarindan sonra bildirim kolon sozlesmesini uygular.
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable(NotificationConsts.TableName, LogManagementDbProperties.DbSchema);
        builder.ConfigureByConvention();

        // Uretici modulun icerik sozlugu tek jsonb kolonunda tutulur; satir eklendikten sonra degismedigi icin referans karsilastirmasi yeterlidir.
        builder.Property(entity => entity.Data)
            .HasColumnType(NotificationConsts.DataColumnType)
            .HasConversion(
                data => JsonSerializer.Serialize(data, (JsonSerializerOptions?)null),
                json => JsonSerializer.Deserialize<Dictionary<string, string>>(json, (JsonSerializerOptions?)null)!,
                new ValueComparer<Dictionary<string, string>>(false));

        builder.Property(entity => entity.Topic)
            .HasMaxLength(NotificationConsts.MaxTopicNameLength);

        builder.Property(entity => entity.IsRead)
            .HasDefaultValue(false);

        // Bildirim listeleri her zaman tek bir alicinin kayitlariyla sinirlanir.
        builder.HasIndex(entity => entity.RecipientUserId);
        // Grup guncelleme ve iptal butun satirlari bu kimlikle toplar.
        builder.HasIndex(entity => entity.NotificationGroupId);
        // Zamanlanmis gonderim vadesi gelenleri bu kolona gore sorar.
        builder.HasIndex(entity => entity.ScheduledTime);
    }
}
