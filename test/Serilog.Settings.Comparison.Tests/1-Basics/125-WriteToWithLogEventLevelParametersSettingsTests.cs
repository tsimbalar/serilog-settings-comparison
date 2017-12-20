using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.Settings.C.Tests.SettingsComparison.Tests
{
    [Collection(docs)]
    public class WriteToWithLogEventLevelParametersSettingsTests : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"125### Sinks - `restrictedToMinimumLevel`
Parameters of type `LogEventLevel` such as `restrictedToMinimumLevel` can be provided from the level's name.";

        public WriteToWithLogEventLevelParametersSettingsTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("WriteToWithRestrictedToMinimumLevel.csx")]
        [InlineData("WriteToWithRestrictedToMinimumLevel.json")]
        [InlineData("WriteToWithRestrictedToMinimumLevel.config")]
        public void SupportForLogEventLevelParameters(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            var logger = loggerConfig.CreateLogger();
            logger.Warning("This should not be written because `restrictedToMinimumLevel` = `Error`");
            Assert.Null(TestDummies.DummySink.Emitted.FirstOrDefault());

            logger.Error("This should be written because `restrictedToMinimumLevel` = `Error`");
            Assert.NotNull(TestDummies.DummySink.Emitted.FirstOrDefault());
        }
    }
}
