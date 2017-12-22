using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class AdvancedScenarios : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"200# Advanced scenarios
The following scenarios are also supported.";

        public AdvancedScenarios(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Fact]
        public void Whatever()
        {
            
        }
    }
}
