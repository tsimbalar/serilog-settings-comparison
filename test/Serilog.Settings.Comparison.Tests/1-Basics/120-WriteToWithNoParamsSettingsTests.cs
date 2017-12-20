using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.Settings.C.Tests.SettingsComparison.Tests
{
    [Collection(docs)]
    public class WriteToWithNoParamsSettingsTests : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"120### Sinks - Basics
You can configure usage of a given *Sink* by specifying the name of the method or extension method that you would usually use after `WriteTo.*`.
You may need to explicitly add a `using` directive to look for extension methods in a separate assembly or Nuget package.";

        public WriteToWithNoParamsSettingsTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("WriteToWithNoParams.csx")]
        [InlineData("WriteToWithNoParams.json")]
        [InlineData("WriteToWithNoParams-LongForm.json")]
        [InlineData("WriteToWithNoParams.config")]
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
