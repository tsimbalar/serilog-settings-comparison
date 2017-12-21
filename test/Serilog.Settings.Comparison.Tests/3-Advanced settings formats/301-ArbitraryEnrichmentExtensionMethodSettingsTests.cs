using System;
using Serilog.Events;
using Serilog.SettingsComparisonTests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class ArbitraryEnrichmentExtensionMethodSettingsTests : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"301## Method Discovery
### Enrichment Extension Methods
Log events can be enriched with arbitrary `Enrich.With...()` extension methods.";

        public ArbitraryEnrichmentExtensionMethodSettingsTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        [Theory]
        [InlineData("EnrichWithExternalEnricher.csx")]
        [InlineData("EnrichWithExternalEnricher.json")]
        [InlineData("EnrichWithExternalEnricher.config")]
        public void SupportForArbitraryEnrichmentExtensionMethod(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            LogEvent e = null;
            var logger = loggerConfig
                .WriteTo.Sink(new DelegatingSink(le => e = le)).CreateLogger();

            logger.Information("This will be enriched with 2 properties");
            Assert.NotNull(e);
            Assert.Equal(Guid.Empty.ToString(), e.Properties["ThreadId"].LiteralValue());
            Assert.Equal("UserExtraParam", e.Properties["UserName"].LiteralValue());
        }


    }
}
