using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Byd.CoreExtensions.Interfaces;
using Byd.CoreExtensions.Providers;
using Byd.LogManagement.Attributes;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Etos.ActivityLogs;
using Byd.LogManagement.Interfaces.ActivityLogs;
using SystemStandards.Results;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;

namespace Byd.LogManagement.ActivityLogs.Tracking;

/// <summary>
/// TrackActivity isaretli action basariyla bittiginde etkinlik kaydini store'a verir.
/// </summary>
// islevi: Kalip anahtarini, kullanici adi ve donen kaydin alanlarindan olusan degerleri ve uretici modulun dil kaliplarini olaya koyar.
// sistemdeki gorevi: Uretici uygulamada etkinligin yakalandigi tek noktadir; cumle burada kurulmaz, listelerken kurulur.
public class ActivityTrackingFilter(
    IActivityLogStore activityLogStore,
    IStringLocalizerFactory stringLocalizerFactory,
    ICurrentUser currentUser,
    ICurrentTenant currentTenant,
    ICurrentOrganization currentOrganization,
    CurrentModuleNameProvider moduleNameProvider) : IAsyncActionFilter, ITransientDependency
{
    // Action'i calistirir; isaretli, oturumlu ve basarili sonucu etkinlik kaydina cevirir.
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var executed = await next();

        var activity = context.ActionDescriptor.EndpointMetadata.OfType<TrackActivityAttribute>().FirstOrDefault();
        if (activity is null || !currentUser.IsAuthenticated ||
            executed.Result is not ObjectResult { Value: IResult { IsSuccess: true } result })
        {
            return;
        }

        var record = result.GetValue();
        object[] arguments = [currentUser.UserName!, .. activity.Values.Select(name => Read(record, name)?.ToString() ?? string.Empty)];
        await activityLogStore.SaveAsync(new ActivityRecordedEto
        {
            TenantId = currentTenant.Id,
            TenantName = currentTenant.Name,
            OrganizationId = currentOrganization.Id,
            OrganizationName = currentOrganization.Name,
            UserId = currentUser.GetId(),
            UserName = currentUser.UserName!,
            ExternalId = Read(record, nameof(IEntityDto<Guid>.Id)) as Guid?,
            ApplicationName = moduleNameProvider.Value,
            Kind = activity.Kind,
            Description = Describe(activity, arguments)
        });
    }

    // Uretici modulun kalibini etkinlik dillerinin her birinde kullanici adi ve degerlerle doldurur.
    private Dictionary<string, string> Describe(TrackActivityAttribute activity, object[] arguments)
    {
        var localizer = stringLocalizerFactory.Create(activity.ResourceType);
        var description = new Dictionary<string, string>();
        foreach (var culture in ActivityLogConsts.DescriptionCultureNames)
        {
            using (CultureHelper.Use(culture))
            {
                description[culture] = localizer[activity.LocalizationKey, arguments];
            }
        }

        return description;
    }

    // Donen kaydin verilen adli alanini okur.
    private static object? Read(object? record, string name) => record?.GetType().GetProperty(name)?.GetValue(record);
}
