using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Byd.LogManagement.Etos.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Byd.LogManagement.Managers.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace Byd.LogManagement.Handlers.Notifications;

/// <summary>
/// Gelen bildirim olayini merkezi tabloya yazar ve anlik olani kanallarina push eder.
/// </summary>
// islevi: Olayi kanal ve alici basina satirlara cevirip ekler; vadesi gelmis satirlari, satirlar veritabanina
// yazildiktan sonra dagiticiya verir.
// sistemdeki gorevi: Uretici uygulamalardan gelen bildirimlerin merkezi log servisindeki tuketicisidir; ileri
// tarihli satirlari vakti gelince isci gonderir.
public class NotificationRecordedEventHandler(
    NotificationManager notificationManager,
    IServiceScopeFactory serviceScopeFactory,
    IUnitOfWorkManager unitOfWorkManager)
    : IDistributedEventHandler<NotificationRecordedEto>, ITransientDependency
{
    // Olayi satirlara yazar; push satirlar commit edildikten sonra yapilir, yazma geri alinirsa bildirim gitmez.
    // Dagitici kendi kapsamindan cozulur: olay kutusu isleyicisi bu isleyicinin kapsamini commit'ten once kapatir,
    // yakalanmis bir dagitici commit sirasinda kapanmis kapsamdan servis cozmeye calisir ve hata verir.
    public virtual async Task HandleEventAsync(NotificationRecordedEto eventData)
    {
        var due = await notificationManager.HandleRecordedAsync(eventData);
        unitOfWorkManager.Current?.OnCompleted(async () =>
        {
            using var scope = serviceScopeFactory.CreateScope();
            await scope.ServiceProvider.GetRequiredService<INotificationDispatcher>().DispatchAsync(due);
        });
    }
}
