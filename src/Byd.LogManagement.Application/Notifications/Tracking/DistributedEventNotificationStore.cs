using System.Threading;
using System.Threading.Tasks;
using Byd.LogManagement.Etos.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace Byd.LogManagement.Notifications.Tracking;

/// <summary>
/// Bildirim olaylarini merkezi servise gonderir.
/// </summary>
// islevi: Yakalama noktasinin kurdugu olayi kendi calisma biriminde olay kutusuna yazip hemen tamamlar.
// sistemdeki gorevi: Uretici uygulamanin merkezi bildirim veritabanina baglanmadan istek biraktigi store'dur.
// Kayit, bildirimi isteyen isin calisma biriminden ayri tutulur; is geri alinsa da bildirim istegi kalir.
public class DistributedEventNotificationStore(
    IDistributedEventBus distributedEventBus,
    IUnitOfWorkManager unitOfWorkManager) : INotificationStore, ITransientDependency
{
    // Bildirim istegini kendi calisma biriminde olay kutusuna yazar ve birimi hemen tamamlar.
    public virtual Task SaveAsync(
        NotificationRecordedEto notification,
        CancellationToken cancellationToken = default) =>
        PublishAsync(notification, cancellationToken);

    // Olayi cagiran isten ayri bir calisma biriminde yayinlar.
    private async Task PublishAsync<TEvent>(TEvent eventData, CancellationToken cancellationToken = default)
        where TEvent : class
    {
        using var unitOfWork = unitOfWorkManager.Begin(requiresNew: true);
        await distributedEventBus.PublishAsync(eventData);
        await unitOfWork.CompleteAsync(cancellationToken);
    }
}
