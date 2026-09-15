using Byd.LogManagement.Localization;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace Byd.LogManagement;

public abstract class LogManagementAppService : ApplicationService
{
    protected LogManagementAppService()
    {
        LocalizationResource = typeof(LogManagementResource);
        ObjectMapperContext = typeof(LogManagementApplicationModule);
    }

    // ABP lazy provider uzerinden application bagimliliklarini repository-native sekilde cozer.
    protected TService LazyGetRequiredService<TService>()
        where TService : notnull
    {
        return LazyServiceProvider.LazyGetRequiredService<TService>();
    }

    // Aktif application isteginin iptal belirtecini lazy provider uzerinden acar.
    protected ICancellationTokenProvider CancellationTokenProvider =>
        LazyGetRequiredService<ICancellationTokenProvider>();
}
