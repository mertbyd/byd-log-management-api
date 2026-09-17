using Byd.LogManagement.Localization;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

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
}
