using Serilog.SettingsComparisonTests.Support.Formatting;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class InterfaceViaStaticProperty : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"335### Public static properties
For parameters whose type is an `interface` or an `abstract class`, you can reference a static property that exposes an instance of that interface. Use the full containing type name followed by `::` and the `public static` property name.";

        public InterfaceViaStaticProperty(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("335-WriteToWithInterfaceStaticProperty.csx")]
        [InlineData("335-WriteToWithInterfaceStaticProperty.json")]
        [InlineData("335-WriteToWithInterfaceStaticProperty.config")]
        public void TestCase(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            loggerConfig.CreateLogger();
            Assert.NotNull(TestDummies.DummySink.Formatter);
            Assert.Equal(CustomFormatters.Formatter, TestDummies.DummySink.Formatter);
        }
    }
}
