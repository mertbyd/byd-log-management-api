using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Etos.Notifications;
using Byd.LogManagement.Extensions.Notifications;
using Byd.LogManagement.Hubs.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Byd.LogManagement.Handlers.Notifications;

/// <summary>
/// Uretici uygulamanin canli akisini satir yazmadan web istemcilerine iter.
/// </summary>
// islevi: Olaydaki akisi kapsamindaki yayin grubuna sabit olay adiyla gonderir.
// sistemdeki gorevi: Canli akisin merkezi log servisindeki tuketicisidir; olay inbox'a alinmadan gelir, bildirim satiri
// yazilmaz. Baglanti o an yoksa mesaj kaybolur, yerini bir sonraki akis alir.
public class NotificationStreamedEventHandler(IHubContext<NotificationWebHub> hubContext)
    : IDistributedEventHandler<NotificationStreamedEto>, ITransientDependency
{
    // Akisi organizasyon kapsamindaysa organizasyonun, degilse tenant'in baglantilarina iletir.
    public virtual Task HandleEventAsync(NotificationStreamedEto eventData)
        => hubContext.Clients.Group(NotificationWebHub.BroadcastGroup(eventData.TenantId, eventData.OrganizationId))
            .SendAsync(NotificationConsts.StreamMessageName, eventData.ToStreamMessage());
}
