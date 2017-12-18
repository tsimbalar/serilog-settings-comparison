using Xunit;
using Xunit.Abstractions;

namespace Serilog.Settings.C.Tests.SettingsComparison.Tests
{
    [Collection(docs)]
    public class WriteToWithSimpleParamsSettingsTests : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"220## Simple parameter types
Simple types that are *convertible* from string can be passed. " +
                                   "Empty string can be provided to specify null for nullable parameters. " +
                                   "Parameters with a default value can be omitted.";

        public WriteToWithSimpleParamsSettingsTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("WriteToWithSimpleParams.csx")]
        [InlineData("WriteToWithSimpleParams.json")]
        [InlineData("WriteToWithSimpleParams.config")]
        public void SupportForSimpleTypesParameters(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            loggerConfig.CreateLogger();
            Assert.Equal("A string param", TestDummies.DummySink.StringParam);
            Assert.Equal(666, TestDummies.DummySink.IntParam);
            Assert.Null(TestDummies.DummySink.NullableIntParam);
            Assert.Equal("default", TestDummies.DummySink.StringParamWithDefault);
        }
    }
}
