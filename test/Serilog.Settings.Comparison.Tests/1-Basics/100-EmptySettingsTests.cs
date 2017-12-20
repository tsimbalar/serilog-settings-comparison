using Serilog.Events;
using Serilog.Settings.Comparison.Tests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.Settings.C.Tests.SettingsComparison.Tests
{
    [Collection(docs)]
    public class EmptySettingsTests : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"100## Basics
### Empty settings
Loading an empty config file behaves the same as the default `CreateLogger()`. " +
                                   "Minimum Level is *Information*.";

        public EmptySettingsTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("Empty.csx", false)]
        [InlineData("Empty.json", false)]
        [InlineData("Empty-EmptySection.json", true)]
        [InlineData("Empty.config", false)]
        [InlineData("Empty-EmptySection.config", true)]
        public void EmptyConfigFile(string fileName, bool includeInOutput)
        {
            WriteDocumentation(fileName, includeInOutput);

            var loggerConfig = LoadConfig(fileName);

            LogEvent e = null;
            var logger = loggerConfig
                .WriteTo.Sink(new DelegatingSink(le => e = le)).CreateLogger();

            e = null;
            logger.Debug("Should not be written (default min level is Information)");
            Assert.Null(e);
            logger.Information("Should be written");
            Assert.NotNull(e);
        }
    }
}
