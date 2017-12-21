using Serilog.Events;
using Serilog.SettingsComparisonTests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class PropertyEnrichment : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"130### Property-Enrichment
Log events can be enriched with arbitrary properties.";

        public PropertyEnrichment(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("EnrichWithProperty.csx")]
        [InlineData("EnrichWithProperty.json")]
        [InlineData("EnrichWithProperty.config")]
        public void TestCase(string fileName)
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
