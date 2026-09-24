using System.Threading.Tasks;
using Byd.LogManagement.Etos.Notifications;
using Byd.LogManagement.Senders.Notifications.Dispatcher;
using Byd.LogManagement.Managers.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace Byd.LogManagement.Handlers.Notifications;

/// <summary>
/// Gelen bildirim olayini merkezi tabloya yazar ve anlik olani web'e push eder.
/// </summary>
// islevi: Olayi alici basina satirlara cevirip ekler; vadesi gelmis web satirlarini, satirlar veritabanina
// yazildiktan sonra push eder.
// sistemdeki gorevi: Uretici uygulamalardan gelen bildirimlerin merkezi log servisindeki tuketicisidir; ileri
// tarihli satirlari vakti gelince isci gonderir.
public class NotificationRecordedEventHandler(
    NotificationManager notificationManager,
    NotificationWebDispatcher dispatcher,
    IUnitOfWorkManager unitOfWorkManager)
    : IDistributedEventHandler<NotificationRecordedEto>, ITransientDependency
{
    // Olayi satirlara yazar; push satirlar commit edildikten sonra yapilir, yazma geri alinirsa bildirim gitmez.
    public virtual async Task HandleEventAsync(NotificationRecordedEto eventData)
    {
        var notifications = await notificationManager.HandleRecordedAsync(eventData);
        unitOfWorkManager.Current?.OnCompleted(() => dispatcher.SendManyAsync(notifications));
    }
}
