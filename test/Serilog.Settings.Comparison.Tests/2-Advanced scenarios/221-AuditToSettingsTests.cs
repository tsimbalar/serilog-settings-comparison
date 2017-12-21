using Serilog.Core;
using Serilog.Events;
using Serilog.SettingsComparisonTests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class AuditToSettingTests : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"221### Sinks - `AuditTo`
Some sinks provide *Audit* functionality via the configuration method `.AuditTo.MySink()`. This is also supported via configuration.";

        public AuditToSettingTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("AuditToWithSimpleParams.csx")]
        [InlineData("AuditToWithSimpleParams.json")]
        [InlineData("AuditToWithSimpleParams.config")]
        public void SupportForAuditToWithSimpleParams(string fileName)
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
