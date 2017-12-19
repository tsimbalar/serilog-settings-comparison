using TestDummies.Console;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.Settings.C.Tests.SettingsComparison.Tests
{
    [Collection(docs)]
    public class WriteToWithConcreteDefaultImplementationOfAbstractClassSettingsTests : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"231### Abstract-class-typed parameters
For parameters whose type is an `abstract class`, the full type name of an implementation " +
                                   "can be provided. If the type is not in the `Serilog`, remember to include `using` directives.";

        public WriteToWithConcreteDefaultImplementationOfAbstractClassSettingsTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("WriteToWithConcreteDefaultImplementationOfAbstractClass.csx")]
        [InlineData("WriteToWithConcreteDefaultImplementationOfAbstractClass.json", Skip = "Not released yet in JSON format")]
        [InlineData("WriteToWithConcreteDefaultImplementationOfAbstractClass.config")]
        public void SupportForAbstractClassParamsPassingConcreteClassWithDefaultConstructor(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            loggerConfig.CreateLogger();
            Assert.NotNull(DummyConsoleSink.Theme);
            Assert.IsType<CustomConsoleTheme>(DummyConsoleSink.Theme);
        }
    }
}
