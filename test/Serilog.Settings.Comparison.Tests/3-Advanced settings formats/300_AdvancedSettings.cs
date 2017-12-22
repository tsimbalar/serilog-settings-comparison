using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class AdvancedSettings : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"300# Advanced settings formats
Below are the general rules for setting values.";

        public AdvancedSettings(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Fact]
        public void Whatever()
        {
            
        }
    }
}
