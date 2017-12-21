using Serilog.Events;
using Serilog.SettingsComparisonTests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class MinimumLevel : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"110### Minimum Level
Global Minimum level can be defined.";

        public MinimumLevel(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("MinimumLevel.csx", true)]
        [InlineData("MinimumLevel-is.csx", false)]
        [InlineData("MinimumLevel.json", true)]
        [InlineData("MinimumLevel-Default.json", false)]
        [InlineData("MinimumLevel.config", true)]
        public void TestCase(string fileName, bool includeInOutput)
        {
            WriteDocumentation(fileName, includeInOutput);

            var loggerConfig = LoadConfig(fileName);

            LogEvent e = null;
            var logger = loggerConfig
                .WriteTo.Sink(new DelegatingSink(le => e = le)).CreateLogger();

            e = null;
            logger.Information("Should not be written");
            Assert.Null(e);
            logger.Warning("Should be written");
            Assert.NotNull(e);
        }
    }
}
