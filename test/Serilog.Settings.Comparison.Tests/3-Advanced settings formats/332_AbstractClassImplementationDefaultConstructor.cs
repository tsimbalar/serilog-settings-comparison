using TestDummies.Console;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class AbstractClassImplementationDefaultConstructor : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"332### Abstract-class-typed parameters
For parameters whose type is an `abstract class`, the full type name of an implementation " +
                                   "can be provided. If the type is not in the `Serilog`, remember to include `using` directives.";

        public AbstractClassImplementationDefaultConstructor(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("332-WriteToWithConcreteDefaultImplementationOfAbstractClass.csx")]
        [InlineData("332-WriteToWithConcreteDefaultImplementationOfAbstractClass.json")]//, Skip = "Not released yet in JSON format")]
        [InlineData("332-WriteToWithConcreteDefaultImplementationOfAbstractClass.config")]
        public void TestCase(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            loggerConfig.CreateLogger();
            Assert.NotNull(DummyConsoleSink.Theme);
            Assert.IsType<CustomConsoleTheme>(DummyConsoleSink.Theme);
        }
    }
}
