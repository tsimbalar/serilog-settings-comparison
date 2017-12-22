using Serilog.SettingsComparisonTests.Support.Formatting;
using TestDummies.Console;
using TestDummies.Console.Themes;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class StaticProperty : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"332### Public static properties
For parameters whose type is an `interface` or an `abstract class`, you can reference a static property that exposes an instance of that interface. Use the full containing type name followed by `::` and the `public static` property name.";

        public StaticProperty(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("332-ImplementationViaStaticProperty.csx")]
        [InlineData("332-ImplementationViaStaticProperty.json")]
        [InlineData("332-ImplementationViaStaticProperty.config")]
        public void TestCase(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            loggerConfig.CreateLogger();
            Assert.NotNull(TestDummies.DummySink.Formatter);
            Assert.Equal(CustomFormatters.Formatter, TestDummies.DummySink.Formatter);

            Assert.NotNull(DummyConsoleSink.Theme);
            Assert.Equal(ConsoleThemes.Theme1, DummyConsoleSink.Theme);
        }
    }
}
