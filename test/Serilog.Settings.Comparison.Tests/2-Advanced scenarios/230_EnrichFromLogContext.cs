﻿using Serilog.Context;
using Serilog.Events;
using Serilog.SettingsComparisonTests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class EnrichFromLogContext : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"230### Enrichment from `LogContext`
Log events can be enriched with `LogContext`.";

        public EnrichFromLogContext(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("230-EnrichFromLogContext.csx")]
        [InlineData("230-EnrichFromLogContext.json")]
        [InlineData("230-EnrichFromLogContext.config")]
        public void TestCase(string fileName)
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
