using FluentAssertions;
using TestDummies;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class ComplexValues : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"321### Complex values
Arrays or complex type can also be passed to configuration methods.";

        public ComplexValues(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {

        }

        [Theory]
        [InlineData("321-ComplexValues.csx")]
        [InlineData("321-ComplexValues.json")]
        [InlineData("321-ComplexValues.config", Skip = "Not supported yet in the appSettings XML format.")]
        public void TestCase(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            loggerConfig.CreateLogger();

            DummySinkWithComplexParams.Poco.Should().BeEquivalentTo(new Poco()
            {
                StringProperty = "myString",
                IntProperty = 42,
                Nested = new SubPoco()
                {
                    SubProperty = "Sub"
                }
            });
            Assert.Equal(new[] { 2, 4, 6 }, DummySinkWithComplexParams.IntArray);
            Assert.Equal(new[] { "one", "two", "three" }, DummySinkWithComplexParams.StringArray);
            DummySinkWithComplexParams.ObjectArray.Should().BeEquivalentTo(new[]
            {
                new SubPoco()
                {
                    SubProperty = "Sub1"
                },
                new SubPoco()
                {
                    SubProperty = "Sub2"
                },
            });
        }


    }
}
