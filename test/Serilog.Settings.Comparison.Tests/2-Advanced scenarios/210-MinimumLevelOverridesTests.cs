using Serilog.Core;
using Serilog.Events;
using Serilog.SettingsComparisonTests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    [Collection(docs)]
    public class MinimumLevelOverridesTests : BaseSettingsSupportComparisonTests
    {
        public const string docs = @"210### Minimum level overrides
Minimum level can be overriden (up or down) for specific `SourceContext`s.";

        public MinimumLevelOverridesTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Theory]
        [InlineData("MinimumLevelOverrides.csx")]
        [InlineData("MinimumLevelOverrides.json")]
        [InlineData("MinimumLevelOverrides.config")]
        public void SupportForMinimumLevelOverrides(string fileName)
        {
            WriteDocumentation(fileName);

            var loggerConfig = LoadConfig(fileName);

            LogEvent e = null;
            var logger = loggerConfig
                .WriteTo.Sink(new DelegatingSink(le => e = le)).CreateLogger();

            e = null;
            logger.ForContext(Constants.SourceContextPropertyName, "Microsoft.SomeClass")
                .Warning("Should not be written (Override Microsoft >= Error)");
            Assert.Null(e);
            logger.ForContext(Constants.SourceContextPropertyName, "Microsoft.Extensions.SomeClass")
                .Debug("Should not be written (Override Microsoft.Extensions >= Information)");
            Assert.Null(e);
            logger.ForContext(Constants.SourceContextPropertyName, "System.String")
                .Verbose("Should not be written (Override System >= Debug)");
            Assert.Null(e);

            logger.ForContext(Constants.SourceContextPropertyName, "Microsoft.SomeClass")
                .Error("Should be written (Override Microsoft >= Error)");
            Assert.NotNull(e);
            e = null;
            logger.ForContext(Constants.SourceContextPropertyName, "Microsoft.Extensions.SomeClass")
                .Information("Should be written (Override Microsoft.Extensions >= Information)");
            Assert.NotNull(e);
            e = null;
            logger.ForContext(Constants.SourceContextPropertyName, "System.String")
                .Debug("Should be written (Override System >= Debug)");
            Assert.NotNull(e);
        }
    }
}
