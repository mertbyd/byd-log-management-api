using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSubstitute;
using Byd.CoreExtensions.Interfaces;
using Byd.CoreExtensions.Options;
using Byd.CoreExtensions.Providers;
using Byd.LogManagement.Dtos.ExternalServiceLogs;
using Byd.LogManagement.Etos.ExternalServiceLogs;
using Byd.LogManagement.ExternalServiceLogs.Tracking;
using Byd.LogManagement.Extensions.ExternalServiceLogs;
using Byd.LogManagement.Interfaces.ExternalServiceLogs;
using Shouldly;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;
using Volo.Abp.Tracing;
using Xunit;

namespace Byd.LogManagement.ExternalServiceLogs;

public class ExternalServiceLoggerTests
{
    private static readonly Guid TenantId = Guid.NewGuid();
    private static readonly Guid OrganizationId = Guid.NewGuid();

    [Fact]
    public async Task TrackAsync_RecordsTheScopeTheCallWasMadeIn()
    {
        ExternalServiceLogRecordedEto? recorded = null;
        var store = Substitute.For<IExternalServiceLogStore>();
        await store.SaveAsync(Arg.Do<ExternalServiceLogRecordedEto>(eto => recorded = eto), Arg.Any<CancellationToken>());

        await CreateLogger(store).TrackAsync(
            new ExternalServiceCall("Vilnius stops.lt", "https://www.stops.lt/vilnius/vehicle_positions.pb", "GET"),
            () => Task.FromResult("{}"));

        recorded.ShouldNotBeNull();
        (recorded.TenantId, recorded.TenantName, recorded.OrganizationId, recorded.OrganizationName)
            .ShouldBe((TenantId, "demo", OrganizationId, "Vilnius"));
        var log = recorded.ToExternalServiceLog();
        (log.TenantId, log.TenantName, log.OrganizationId, log.OrganizationName)
            .ShouldBe((TenantId, "demo", OrganizationId, "Vilnius"));
    }

    private static ExternalServiceLogger CreateLogger(IExternalServiceLogStore store)
    {
        var currentTenant = Substitute.For<ICurrentTenant>();
        currentTenant.Id.Returns(TenantId);
        currentTenant.Name.Returns("demo");
        var currentOrganization = Substitute.For<ICurrentOrganization>();
        currentOrganization.Id.Returns(OrganizationId);
        currentOrganization.Name.Returns("Vilnius");
        return new ExternalServiceLogger(
            store,
            new CurrentModuleNameProvider(Options.Create(new CoreExtensionsModuleOptions { ModuleName = "GtfsRealtime" })),
            Substitute.For<ICorrelationIdProvider>(),
            currentTenant,
            currentOrganization,
            Substitute.For<IClock>());
    }
}
