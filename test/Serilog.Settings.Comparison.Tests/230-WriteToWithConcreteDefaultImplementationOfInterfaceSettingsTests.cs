using Serilog.Formatting.Json;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.Settings.C.Tests.SettingsComparison.Tests
{
    [Collection(docs)]
    public class WriteToWithConcreteDefaultImplementationOfInterfaceSettingsTests : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"230### Interface-typed parameters
For parameters whose type is an `interface`, the full type name of an implementation " +
                                   "can be provided. If the type is not in the `Serilog`, remember to include `using` directives." +
                                   "**TODO** : investigate.... Configuration seems to require the assembly name, but AppSettings doesn't !";

        public WriteToWithConcreteDefaultImplementationOfInterfaceSettingsTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("WriteToWithConcreteDefaultImplementationOfInterface.csx")]
        [InlineData("WriteToWithConcreteDefaultImplementationOfInterface.json")]
        [InlineData("WriteToWithConcreteDefaultImplementationOfInterface.config")]
        public void SupportForInterfaceParamsPassingConcreteClassWithDefaultConstructor(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            loggerConfig.CreateLogger();
            Assert.NotNull(TestDummies.DummySink.Formatter);
            Assert.IsType<JsonFormatter>(TestDummies.DummySink.Formatter);
        }
    }
}
