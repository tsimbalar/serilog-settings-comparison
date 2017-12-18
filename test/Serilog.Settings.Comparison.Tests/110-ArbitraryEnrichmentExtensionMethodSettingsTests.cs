using System;
using Serilog.Events;
using Serilog.Settings.Comparison.Tests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.Settings.C.Tests.SettingsComparison.Tests
{
    [Collection(docs)]
    public class ArbitraryEnrichmentExtensionMethodSettingsTests : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"110## Enrichment Extension Methods
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

        // TODO : implementation class for Interface or Abstract class
        // TODO : static property accessor
        // TODO : out of the box conversions : Uri, TimeSpan ... 
        // TODO : AuditTo
        // TODO : LoggingLevelSwitch
        // TODO : filters

    }
}
