using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class WriteToSink : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"120### Sinks - Basics
You can configure usage of a given *Sink* by specifying the name of the method or extension method that you would usually use after `WriteTo.*`.
You may need to explicitly add a `using` directive to look for extension methods in a separate assembly or Nuget package.";

        public WriteToSink(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("120-WriteToWithNoParams.csx", true)]
        [InlineData("120-WriteToWithNoParams.json", true)]
        [InlineData("120-WriteToWithNoParams-LongForm.json", false)]
        [InlineData("120-WriteToWithNoParams.config", true)]
        public void TestCase(string fileName, bool includeInOutput)
        {
            WriteDocumentation(fileName, includeInOutput);

            var loggerConfig = LoadConfig(fileName);

            var logger = loggerConfig.CreateLogger();
            logger.Error("This should be written to Dummy");
            var e = TestDummies.DummySink.Emitted.FirstOrDefault();
            Assert.NotNull(e);
        }
    }
}
