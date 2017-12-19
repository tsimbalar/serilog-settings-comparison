using Serilog.Core;
using Serilog.Events;
using Serilog.Settings.Comparison.Tests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.Settings.C.Tests.SettingsComparison.Tests
{
    [Collection(docs)]
    public class AdvandedScenarioTests : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"200# Advanced scenarios
The following scenarios are also supported.";

        public AdvandedScenarioTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Fact]
        public void Whatever()
        {
            
        }
    }
}
