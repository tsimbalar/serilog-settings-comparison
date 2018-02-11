using System.Linq;
using Serilog.Events;
using Serilog.SettingsComparisonTests.Support;
using TestDummies;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class MethodDiscovery : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"310## Method Discovery
Settings providers will discover extension methods for configuration. Remember to add  `using` directives if those extension methods leave in an assembly other than *Serilog*.

Extension methods to the following types are supported :

| Type | C# API | xml prefix | json section
| ---- | ------ | ---------- | ------------
| `LoggerSinkConfiguration` | `config.WriteTo.*` | `serilog:write-to:` | `WriteTo`
| `LoggerAuditSinkConfiguration` | `config.AuditTo.*` | `serilog:audit-to:` | `AuditTo`
| `LoggerEnrichmentConfiguration` | `config.Enrich.*` | `serilog:enrich:` | `Enrich`
| `LoggerFilterConfiguration` | `config.Filter.*` | `serilog:filter:` | `Filter`

";

        public MethodDiscovery(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("310-MethodDiscovery.csx")]
        [InlineData("310-MethodDiscovery.json")]
        [InlineData("310-MethodDiscovery.config")]
        public void TestCase(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            LogEvent e = null;
            var logger = loggerConfig
                .WriteTo.Sink(new DelegatingSink(le => e = le)).CreateLogger();

            logger.Warning("This will be filtered out by Custom dummy filter");
            Assert.Null(e);

            logger.Information("This one will be enriched and written to Dummy sinks");
            Assert.NotNull(e);
            Assert.Equal("UserExtraParam", e.Properties["UserName"].LiteralValue());

            var auditEvent = DummyAuditSink.Emitted.SingleOrDefault();
            Assert.NotNull(auditEvent);
            Assert.Equal("A string param", DummyAuditSink.StringParam);
            Assert.Equal(666, DummyAuditSink.IntParam);

            var sinkEvent = DummySink.Emitted.SingleOrDefault();
            Assert.NotNull(sinkEvent);
        }
    }
}
