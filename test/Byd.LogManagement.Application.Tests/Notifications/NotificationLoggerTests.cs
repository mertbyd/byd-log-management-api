using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSubstitute;
using Byd.CoreExtensions.Interfaces;
using Byd.CoreExtensions.Options;
using Byd.CoreExtensions.Providers;
using Byd.LogManagement.Etos.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Byd.LogManagement.Notifications.Tracking;
using Shouldly;
using Volo.Abp.Json;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;
using Volo.Abp.Users;
using Xunit;

namespace Byd.LogManagement.Notifications;

public class NotificationLoggerTests
{
    private static readonly Guid TenantId = Guid.NewGuid();
    private static readonly Guid OrganizationId = Guid.NewGuid();

    [Fact]
    public async Task StreamAsync_SendsThePayloadInTheScopeItWasProduced()
    {
        NotificationStreamedEto? streamed = null;
        var store = Substitute.For<INotificationStore>();
        await store.StreamAsync(Arg.Do<NotificationStreamedEto>(eto => streamed = eto), Arg.Any<CancellationToken>());
        var payload = new { IsFullDataset = true };
        var jsonSerializer = Substitute.For<IJsonSerializer>();
        jsonSerializer.Serialize(payload).Returns("{\"isFullDataset\":true}");

        await CreateLogger(store, jsonSerializer).StreamAsync("TripUpdates", payload);

        streamed.ShouldNotBeNull();
        (streamed.TenantId, streamed.OrganizationId, streamed.ModuleName, streamed.Name, streamed.Payload)
            .ShouldBe((TenantId, OrganizationId, "GtfsRealtime", "TripUpdates", "{\"isFullDataset\":true}"));
        await store.DidNotReceiveWithAnyArgs().SaveAsync(default!);
    }

    private static NotificationLogger CreateLogger(INotificationStore store, IJsonSerializer jsonSerializer)
    {
        var currentTenant = Substitute.For<ICurrentTenant>();
        currentTenant.Id.Returns(TenantId);
        var currentOrganization = Substitute.For<ICurrentOrganization>();
        currentOrganization.Id.Returns(OrganizationId);
        return new NotificationLogger(
            store,
            new CurrentModuleNameProvider(Options.Create(new CoreExtensionsModuleOptions { ModuleName = "GtfsRealtime" })),
            currentTenant,
            currentOrganization,
            Substitute.For<ICurrentUser>(),
            Substitute.For<IClock>(),
            jsonSerializer);
    }
}
