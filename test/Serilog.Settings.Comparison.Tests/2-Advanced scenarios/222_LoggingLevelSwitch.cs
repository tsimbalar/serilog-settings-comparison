using Serilog.Events;
using Serilog.SettingsComparisonTests.Support;
using TestDummies;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class LoggingLevelSwitch : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"222### Sinks - `LoggingLevelSwitch`
Some sinks such as the *Seq* sink accept a `LoggingLevelSwitch` that can be remote-controlled. In those case, the same `LoggingLevelSwitch` instance that is used to control the global minimum level must be used.
The *reference* to the switch is noted with the symbol `$`.";

        public LoggingLevelSwitch(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("222-LoggingLevelSwitch.csx")]
        [InlineData("222-LoggingLevelSwitch.json")]
        [InlineData("222-LoggingLevelSwitch.config")]
        public void TestCase(string fileName)
        {
            WriteDocumentation(fileName);

            LogEvent evt = null;
            var loggerConfig = LoadConfig(fileName)
                .WriteTo.Sink(new DelegatingSink(e => evt = e));

            var logger = loggerConfig.CreateLogger();
            Assert.False(DummyWithLevelSwitchSink.ControlLevelSwitch == null, "Sink ControlLevelSwitch should have been initialized");

            var controlSwitch = DummyWithLevelSwitchSink.ControlLevelSwitch;
            Assert.NotNull(controlSwitch);

            logger.Information("A message that will not be logged");
            Assert.True(evt is null, "LoggingLevelSwitch initial level was Warning. It should not log Information messages");

            controlSwitch.MinimumLevel = LogEventLevel.Debug;
            logger.Debug("A message that will be logged");
            Assert.True(evt != null, "LoggingLevelSwitch level was changed to Debug. It should log Debug messages");
        }
    }
}
