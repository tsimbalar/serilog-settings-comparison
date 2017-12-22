using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class InterfacesAndAbstractClasses : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"330## Interfaces and abstract classes";

        public InterfacesAndAbstractClasses(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Fact]
        public void Whatever()
        {
            
        }
    }
}
