using System;
using TestDummies;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class SettingsValueConversions : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"320## Setting values conversions
Values for settings can be simple value types (`string`, `int`, `bool` etc), nullable versions of the previous. `Enum`s can also be parsed by name. Some specific types like `Uri` and `TimeSpan` are also supported.";

        public SettingsValueConversions(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        [Theory]
        [InlineData("320-SettingsValueConversions.csx")]
        [InlineData("320-SettingsValueConversions.json")]
        [InlineData("320-SettingsValueConversions.config")]
        public void TestCase(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            loggerConfig.CreateLogger();

            Assert.Equal(MyEnum.Qux, DummySinkWithParams.EnumParam);
            Assert.Equal(new TimeSpan(2, 3, 4, 5), DummySinkWithParams.TimespanParam);
            Assert.Equal(new Uri("https://www.serilog.net"), DummySinkWithParams.UriParam);
        }


    }
}
