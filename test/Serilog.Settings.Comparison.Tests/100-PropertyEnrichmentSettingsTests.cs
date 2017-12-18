using Serilog.Events;
using Serilog.Settings.Comparison.Tests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.Settings.C.Tests.SettingsComparison.Tests
{
    [Collection(docs)]
    public class PropertyEnrichmentSettingsTests : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"100## Enrichment
### Property enrichment
Log events can be enriched with arbitrary properties.";

        public PropertyEnrichmentSettingsTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("EnrichWithProperty.csx")]
        [InlineData("EnrichWithProperty.json")]
        [InlineData("EnrichWithProperty.config")]
        public void SupportForPropertyEnrichment(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            LogEvent e = null;
            var logger = loggerConfig
                .WriteTo.Sink(new DelegatingSink(le => e = le)).CreateLogger();

            logger.Information("This will be enriched with 2 properties");
            Assert.NotNull(e);
            Assert.Equal("MyApp", e.Properties["AppName"].LiteralValue());
            Assert.Equal("MyServer", e.Properties["ServerName"].LiteralValue());
        }
    }
}
