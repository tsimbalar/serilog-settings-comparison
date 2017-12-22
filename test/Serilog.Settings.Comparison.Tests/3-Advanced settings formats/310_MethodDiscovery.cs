using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class MethodDiscovery : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"310## Method Discovery";

        public MethodDiscovery(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Fact]
        public void Whatever()
        {
            
        }
    }
}
