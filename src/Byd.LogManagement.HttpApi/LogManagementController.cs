using Byd.LogManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.DependencyInjection;

namespace Byd.LogManagement;

public abstract class LogManagementController : AbpControllerBase
{
    protected LogManagementController()
    {
        LocalizationResource = typeof(LogManagementResource);
    }

    // ABP lazy provider uzerinden controller servislerini repository-native sekilde cozer.
    protected TService LazyGetRequiredService<TService>()
        where TService : notnull
    {
        return LazyServiceProvider.LazyGetRequiredService<TService>();
    }
}
