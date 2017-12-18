using Serilog.Events;
using Serilog.Settings.Comparison.Tests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.Settings.C.Tests.SettingsComparison.Tests
{
    [Collection(docs)]
    public class MinimumLevelSettingsTests : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"010## Minimum Level
Global Minimum level can be defined.";

        public MinimumLevelSettingsTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("MinimumLevel.csx")]
        [InlineData("MinimumLevel-is.csx")]
        [InlineData("MinimumLevel.json")]
        [InlineData("MinimumLevel-Default.json")]
        [InlineData("MinimumLevel.config")]
        public void SupportForMinimumLevel(string fileName)
        {
            WriteDocumentation(fileName);

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
