using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Providers;
using Byd.LogManagement.Dtos.Notifications;
using Byd.LogManagement.Etos.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;
using Volo.Abp.Users;

namespace Byd.LogManagement.Notifications.Tracking;

/// <summary>
/// Bildirim istegini merkezi servise birakir.
/// </summary>
// islevi: Cagiranin verdigi kaydi modul adi, tenant ve gonderen kullanici bilgisiyle tamamlayip tek olaya cevirir.
// sistemdeki gorevi: Uretici uygulamada bildirimin yakalandigi tek noktadir; cagiran yalnizca kendi bildigi
// alanlari verir, baglamdan cozulen alanlari burasi doldurur.
public class NotificationLogger(
    INotificationStore notificationStore,
    CurrentModuleNameProvider moduleNameProvider,
    ICurrentTenant currentTenant,
    ICurrentUser currentUser,
    IClock clock) : INotificationLogger, ITransientDependency
{
    // Cagiranin kaydini baglam alanlariyla tamamlayip merkezi servise birakir; vade verilmediyse anlik kabul edilir.
    public virtual Task NotifyAsync(NotificationRecord record, CancellationToken cancellationToken = default) =>
        notificationStore.SaveAsync(
            new NotificationRecordedEto
            {
                TenantId = currentTenant.Id,
                TenantName = currentTenant.Name,
                ChannelCode = record.ChannelCode,
                SenderUserId = currentUser.Id,
                SenderUserName = currentUser.UserName,
                ExternalId = record.ExternalId,
                ModuleName = moduleNameProvider.Value,
                Title = record.Title,
                IsTenantNotification = record.IsTenantNotification,
                ScheduledTime = record.ScheduledTime ?? clock.Now,
                Description = record.Description,
                Recipients = record.Recipients
            },
            cancellationToken);
}
