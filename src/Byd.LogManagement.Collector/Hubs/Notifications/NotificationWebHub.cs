using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Byd.CoreExtensions.Interfaces;
using Byd.LogManagement.Consts;
using Volo.Abp.AspNetCore.SignalR;

namespace Byd.LogManagement.Hubs.Notifications;

/// <summary>
/// Tarayicinin anlik web bildirimi almak icin baglandigi SignalR hub'idir.
/// </summary>
// islevi: Acilan baglantiyi tenant grubuna, token'da organizasyon varsa organizasyon grubuna ekler; kullaniciya ozel
// mesaji SignalR kullanici kimligiyle kendisi eslestirir.
// sistemdeki gorevi: SignalR gondericisinin kullaniciya, organizasyona ve tenant'a gonderim yaptigi baglantilarin giris
// noktasidir; uc eslemesi hub'i WebSocket ve SSE tasimalariyla esler.
[Authorize]
[DisableAutoHubMap]
public class NotificationWebHub : AbpHub
{
    private ICurrentOrganization CurrentOrganization => LazyServiceProvider.LazyGetRequiredService<ICurrentOrganization>();

    // Baglantiyi tenant grubuna ve organizasyon grubuna ekler; yayin bildirimi kapsamina gore bu gruplardan birine gider.
    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"{NotificationConsts.TenantGroupPrefix}:{CurrentTenant.Id}");
        if (CurrentOrganization.Id is not null)
            await Groups.AddToGroupAsync(Context.ConnectionId,
                $"{NotificationConsts.OrganizationGroupPrefix}:{CurrentOrganization.Id}");
        await base.OnConnectedAsync();
    }
}
