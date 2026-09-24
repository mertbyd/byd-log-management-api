using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Byd.LogManagement.Consts;
using Volo.Abp.AspNetCore.SignalR;

namespace Byd.LogManagement.Hubs.Notifications;

/// <summary>
/// Tarayicinin anlik web bildirimi almak icin baglandigi SignalR hub'idir.
/// </summary>
// islevi: Acilan baglantiyi tenant grubuna ekler; kullaniciya ozel mesaji SignalR kullanici kimligiyle kendisi eslestirir.
// sistemdeki gorevi: SignalR gondericisinin kullaniciya ve tenant'a gonderim yaptigi baglantilarin giris noktasidir.
[Authorize]
[HubRoute(NotificationRoutes.Hub)]
public class NotificationWebHub : AbpHub
{
    // Baglantiyi tenant grubuna ekler; tenant bildirimi bu gruba gider.
    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"{NotificationConsts.TenantGroupPrefix}:{CurrentTenant.Id}");
        await base.OnConnectedAsync();
    }
}
