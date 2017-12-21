using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class Basics : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"100## Basics";

        public Basics(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Fact]
        public void Whatever()
        {
            
        }
    }
}
