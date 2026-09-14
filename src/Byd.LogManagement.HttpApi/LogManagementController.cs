using Byd.LogManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Byd.LogManagement;

public abstract class LogManagementController : AbpControllerBase
{
    protected LogManagementController()
    {
        LocalizationResource = typeof(LogManagementResource);
    }
}
