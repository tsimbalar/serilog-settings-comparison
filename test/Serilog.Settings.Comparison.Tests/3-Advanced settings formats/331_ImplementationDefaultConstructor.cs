using Serilog.Formatting.Json;
using TestDummies.Console;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class ImplementationDefaultConstructor : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"331### Full type name of implementation with default constructor
For parameters whose type is an `interface` or an `abstract class`, the full type name of an implementation " +
                                   "can be provided. If the type is not in the `Serilog` assembly, remember to include `using` directives." +
                                   "**TODO** : investigate.... Configuration seems to require the assembly name, but AppSettings doesn't !";

        public ImplementationDefaultConstructor(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("331-ImplementationDefaultConstructor.csx")]
        [InlineData("331-ImplementationDefaultConstructor.json")]
        [InlineData("331-ImplementationDefaultConstructor.config")]
        public void TestCase(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            loggerConfig.CreateLogger();

            Assert.NotNull(TestDummies.DummySink.Formatter);
            Assert.IsType<JsonFormatter>(TestDummies.DummySink.Formatter);

            Assert.NotNull(DummyConsoleSink.Theme);
            Assert.IsType<CustomConsoleTheme>(DummyConsoleSink.Theme);
        }
    }
}
