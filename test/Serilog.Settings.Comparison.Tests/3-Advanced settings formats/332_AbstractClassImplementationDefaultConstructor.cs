using TestDummies.Console;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class AbstractClassImplementationDefaultConstructor : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"332";

        public AbstractClassImplementationDefaultConstructor(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("332-WriteToWithConcreteDefaultImplementationOfAbstractClass.csx")]
        [InlineData("332-WriteToWithConcreteDefaultImplementationOfAbstractClass.json")]
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
