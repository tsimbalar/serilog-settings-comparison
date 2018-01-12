using Serilog.Context;
using Serilog.Events;
using Serilog.SettingsComparisonTests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class FilterExpressions : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"240### Filtering - Expressions
Filtering can be specified using *filter expressions* thanks to the package *Serilog.Filters.Expressions*.";

        public FilterExpressions(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("240-FilterExpressions.csx")]
        [InlineData("240-FilterExpressions.json")]
        [InlineData("240-FilterExpressions.config")]
        public void TestCase(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            LogEvent e = null;
            var logger = loggerConfig
                .WriteTo.Sink(new DelegatingSink(le => e = le)).CreateLogger();

            logger.ForContext("filter", "exclude").Information("This will not be logged because filter = exclude is set");
            Assert.Null(e);

            logger.ForContext("filter", "keep it !").Information("This will be logged because filter will let it through");
            Assert.NotNull(e);
        }
    }
}
