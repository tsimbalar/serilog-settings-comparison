using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class AuditToSink : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"221### Sinks - `AuditTo`
Some sinks provide *Audit* functionality via the configuration method `.AuditTo.MySink()`. This is also supported via configuration.";

        public AuditToSink(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("221-AuditToWithSimpleParams.csx")]
        [InlineData("221-AuditToWithSimpleParams.json", Skip = "Not implemented yet in Serilog.Settings.Configuration ! [![GitHub issue state](https://img.shields.io/github/issues/detail/s/serilog/serilog-settings-configuration/80.svg)](https://github.com/serilog/serilog-settings-configuration/issues/80)")]
        [InlineData("221-AuditToWithSimpleParams.config")]
        public void TestCase(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            loggerConfig.CreateLogger();
            Assert.Equal("A string param", TestDummies.DummyAuditSink.StringParam);
            Assert.Equal(666, TestDummies.DummyAuditSink.IntParam);
            Assert.Null(TestDummies.DummyAuditSink.NullableIntParam);
            Assert.Equal("default", TestDummies.DummyAuditSink.StringParamWithDefault);
        }
    }
}
