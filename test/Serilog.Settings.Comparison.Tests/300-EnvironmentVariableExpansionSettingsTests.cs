using System;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.Settings.C.Tests.SettingsComparison.Tests
{
    [Collection(docs)]
    public class EnvironmentVariableExpansionSettingsTests : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"300## Environment variable expansion
Values like `%ENV_VARIABLE%` are replaced by the value of the environment variable `ENV_VARIABLE`.";

        public EnvironmentVariableExpansionSettingsTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("Tests.EnvironmentVariableExpansion.csx")]
        [InlineData("Tests.EnvironmentVariableExpansion.json")]
        [InlineData("Tests.EnvironmentVariableExpansion.config")]
        public void SupportEnvironmentVariableExpansion(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            loggerConfig.CreateLogger();
            Assert.NotEqual("%PATH%", TestDummies.DummySink.StringParam);
            Assert.Equal(Environment.GetEnvironmentVariable("PATH"), TestDummies.DummySink.StringParam);
            Assert.NotEqual(0, TestDummies.DummySink.IntParam);
            Assert.Equal(Convert.ToInt32(Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS")), TestDummies.DummySink.IntParam);
        }
    }
}
