using Serilog.Events;
using Serilog.SettingsComparisonTests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class EmptySettings : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"101### Empty settings
Loading an empty config file behaves the same as the default `CreateLogger()`. " +
                                   "Minimum Level is *Information*.";

        public EmptySettings(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("101-Empty.csx", false)]
        [InlineData("101-Empty.json", false)]
        [InlineData("101-Empty-EmptySection.json", true)]
        [InlineData("101-Empty.config", false)]
        [InlineData("101-Empty-EmptySection.config", true)]
        public void TestCase(string fileName, bool includeInOutput)
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
