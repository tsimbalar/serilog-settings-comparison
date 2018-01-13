using System.Linq;
using Serilog.Events;
using Serilog.SettingsComparisonTests.Support;
using TestDummies;
using TestDummies.Console;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class SubLoggers : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"250### Sub-loggers / child loggers
When conditional configuration is needed depending on the sinks, sub-loggers can be used. [More about sub-loggers](https://nblumhardt.com/2016/07/serilog-2-write-to-logger/)";

        public SubLoggers(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("250-SubLoggers.csx")]
        [InlineData("250-SubLoggers.json")]
        [InlineData("250-SubLoggers.config", Skip = "Not supported yet in the appSettings XML format. [![GitHub issue state](https://img.shields.io/github/issues/detail/s/serilog/serilog/1072.svg)](https://github.com/serilog/serilog/issues/1072)")]
        public void TestCase(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            LogEvent e = null;
            var logger = loggerConfig
                .WriteTo.Sink(new DelegatingSink(le => e = le)).CreateLogger();

            logger.Information("This message should end up in 3 different sinks with different enrichments in each");

            Assert.NotNull(e);
            Assert.False(e.Properties.ContainsKey("Prop1"), "Property `Prop1` should not exist");
            Assert.False(e.Properties.ContainsKey("Prop2"), "Property `Prop2` should not exist");

            var consoleEvent = DummyConsoleSink.Emitted.SingleOrDefault();
            Assert.NotNull(consoleEvent);
            Assert.Equal("PropValue1", consoleEvent.Properties["Prop1"].LiteralValue());

            var dummySinkEvent = DummySink.Emitted.SingleOrDefault();
            Assert.NotNull(dummySinkEvent);
            Assert.Equal("PropValue2", dummySinkEvent.Properties["Prop2"].LiteralValue());
        }
    }
}
