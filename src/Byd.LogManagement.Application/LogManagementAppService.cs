using Byd.LogManagement.Localization;
using Volo.Abp.Application.Services;

namespace Byd.LogManagement;

public abstract class LogManagementAppService : ApplicationService
{
    protected LogManagementAppService()
    {
        LocalizationResource = typeof(LogManagementResource);
        ObjectMapperContext = typeof(LogManagementApplicationModule);
    }
}
