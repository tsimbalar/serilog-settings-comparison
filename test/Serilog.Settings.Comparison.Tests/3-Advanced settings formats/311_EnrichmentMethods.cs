using System;
using Serilog.Events;
using Serilog.SettingsComparisonTests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class EnrichmentMethods : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"311### Enrichment Extension Methods
Log events can be enriched with arbitrary `Enrich.With...()` extension methods.";

        public EnrichmentMethods(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        [Theory]
        [InlineData("311-EnrichWithExternalEnricher.csx")]
        [InlineData("311-EnrichWithExternalEnricher.json")]
        [InlineData("311-EnrichWithExternalEnricher.config")]
        public void TestCase(string fileName)
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
