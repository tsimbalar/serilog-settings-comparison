using Serilog.Context;
using Serilog.Events;
using Serilog.Settings.Comparison.Tests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.Settings.C.Tests.SettingsComparison.Tests
{
    [Collection(docs)]
    public class OutOfTheBoxEnrichmentExtensionMethodSettingsTests : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"105### LogContext
Log events can be enriched with LogContext.";

        public OutOfTheBoxEnrichmentExtensionMethodSettingsTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("EnrichFromLogContext.csx")]
        [InlineData("EnrichFromLogContext.json")]
        [InlineData("EnrichFromLogContext.config")]
        public void SupportForOutOfTheBoxEnrichmentExtensionMethod(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            LogEvent e = null;
            var logger = loggerConfig
                .WriteTo.Sink(new DelegatingSink(le => e = le)).CreateLogger();
            using (LogContext.PushProperty("LogContextProperty", "value"))
            {
                logger.Information("This will be enriched with a property");
            }

            Assert.NotNull(e);
            Assert.Equal("value", e.Properties["LogContextProperty"].LiteralValue());
        }
    }
}
