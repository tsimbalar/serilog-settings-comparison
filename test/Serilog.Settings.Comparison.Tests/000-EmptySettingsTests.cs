using Serilog.Events;
using Serilog.Settings.Comparison.Tests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.Settings.C.Tests.SettingsComparison.Tests
{
    [Collection(docs)]
    public class EmptySettingsTests : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"000## Empty settings
Loading an empty config file behaves the same as the default `CreateLogger()`. " +
                                   "Minimum Level is *Information*.";

        public EmptySettingsTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("Tests.Empty.csx")]
        [InlineData("Tests.Empty.json")]
        [InlineData("Tests.Empty-EmptySection.json")]
        [InlineData("Tests.Empty.config")]
        [InlineData("Tests.Empty-EmptySection.config")]
        public void EmptyConfigFile(string fileName)
        {
            WriteDocumentation(fileName);

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
