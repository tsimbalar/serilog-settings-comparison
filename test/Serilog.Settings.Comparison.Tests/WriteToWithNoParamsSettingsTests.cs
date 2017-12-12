using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.Settings.C.Tests.SettingsComparison.Tests
{
    [Collection(docs)]
    public class WriteToWithNoParamsSettingsTests : BaseSettingsSupportComparisonTests
    {
        public const string docs = "Sinks without mandatory arguments can be called.";

        public WriteToWithNoParamsSettingsTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("Tests.WriteToWithNoParams.json")]
        [InlineData("Tests.WriteToWithNoParams-LongForm.json")]
        [InlineData("Tests.WriteToWithNoParams.config")]
        public void SupportForSinksWithoutParameters(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            var logger = loggerConfig.CreateLogger();
            logger.Error("This should be written to Dummy");
            var e = TestDummies.DummySink.Emitted.FirstOrDefault();
            Assert.NotNull(e);
        }
    }
}
