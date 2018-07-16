using System;
using Serilog.Events;
using Serilog.SettingsComparisonTests.Support;
using TestDummies.Policies;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class Destructure : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"235### Custom Destructuring
Specific *Destructuring* rules can be specified.";

        public Destructure(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("235-Destructure.csx")]
        [InlineData("235-Destructure.json")]
        [InlineData("235-Destructure.config")]
        public void TestCase(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            LogEvent e = null;
            var logger = loggerConfig
                .WriteTo.Sink(new DelegatingSink(le => e = le)).CreateLogger();

            var nestedObject = new
            {
                A = new
                {
                    B = new
                    {
                        C = new
                        {
                            D = "F"
                        }
                    }
                }
            };

            var inputString = "ABCDEFGH";
            var collection = new[] { 1, 2, 3, 4, 5, 6 };
            var loginData = new LoginData
            {
                Password = "ThisIsSoSecret",
                Username = "tsimbalar"
            };
            var version = new Version(2, 6, 1);

            logger.Information("{@Nested} {@String}, {@Collection}, {@LoginData}, {@Version}",
                nestedObject, inputString, collection, loginData, version);

            Assert.NotNull(e);
            var formattedNested = e.Properties["Nested"].ToString();
            var formattedString = e.Properties["String"].ToString();
            var formattedCollection = e.Properties["Collection"].ToString();
            var formattedLoginData = e.Properties["LoginData"].ToString();
            var formattedVersion = e.Properties["Version"];

            Assert.Contains("C", formattedNested);
            Assert.DoesNotContain("D", formattedNested);

            Assert.Equal("\"AB…\"", formattedString);

            Assert.Contains("3", formattedCollection);
            Assert.DoesNotContain("4", formattedCollection);

            Assert.DoesNotContain("Password", formattedLoginData);
            Assert.DoesNotContain(loginData.Password, formattedLoginData);

            Assert.IsType<ScalarValue>(formattedVersion);
        }
    }
}
