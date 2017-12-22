using TestDummies.Console;
using TestDummies.Console.Themes;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class AbstractClassViaStaticProperty : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"336";

        public AbstractClassViaStaticProperty(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("336-WriteToWithAbstractClassStaticProperty.csx")]
        [InlineData("336-WriteToWithAbstractClassStaticProperty.json")]
        [InlineData("336-WriteToWithAbstractClassStaticProperty.config")]
        public void TestCase(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            loggerConfig.CreateLogger();
            Assert.NotNull(DummyConsoleSink.Theme);
            Assert.Equal(ConsoleThemes.Theme1, DummyConsoleSink.Theme);
        }
    }
}
