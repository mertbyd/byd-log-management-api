using Volo.Abp.Modularity;

namespace Byd.LogManagement;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class LogManagementApplicationTestBase<TStartupModule> : LogManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
