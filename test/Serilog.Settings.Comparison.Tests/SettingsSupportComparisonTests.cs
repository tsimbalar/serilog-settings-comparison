using System;
using System.Linq;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Settings.Code;
using Serilog.Settings.Comparison.Tests.Support;
using Xunit;
using Xunit.Abstractions;

namespace Serilog.Settings.C.Tests.SettingsComparison.Tests
{
    public class SettingsSupportComparisonTests
    {
        readonly ITestOutputHelper _outputHelper;

        public SettingsSupportComparisonTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Theory]
        [InlineData("Tests.Empty.csx")]
        [InlineData("Tests.Empty.json")]
        [InlineData("Tests.Empty-EmptySection.json")]
        [InlineData("Tests.Empty.config")]
        [InlineData("Tests.Empty-EmptySection.config")]
        public void EmptyConfigFile(string fileName)
        {
            var docs = "Loading an empty config file behaves the same as the default `CreateLogger()`. " +
                       "Minimum Level is *Information*.";
            WriteDocumentation(docs, fileName);

            var loggerConfig = LoadConfig(fileName);

            LogEvent e = null;
            var logger = loggerConfig
                .WriteTo.Sink(new DelegatingSink(le => e = le)).CreateLogger();

            e = null;
            logger.Debug("Should not be written (default min level is Information)");
            Assert.Null(e);
            logger.Information("Should be written");
            Assert.NotNull(e);
        }

        [Theory]
        [InlineData("Tests.MinimumLevel.csx")]
        [InlineData("Tests.MinimumLevel-is.csx")]
        [InlineData("Tests.MinimumLevel.json")]
        [InlineData("Tests.MinimumLevel-Default.json")]
        [InlineData("Tests.MinimumLevel.config")]
        public void SupportForMinimumLevel(string fileName)
        {
            var docs = "Global Minimum level can be defined.";

            WriteDocumentation(docs, fileName);

            var loggerConfig = LoadConfig(fileName);

            LogEvent e = null;
            var logger = loggerConfig
                .WriteTo.Sink(new DelegatingSink(le => e = le)).CreateLogger();

            e = null;
            logger.Information("Should not be written");
            Assert.Null(e);
            logger.Warning("Should be written");
            Assert.NotNull(e);
        }

        [Theory]
        [InlineData("Tests.MinimumLevelOverrides.json")]
        [InlineData("Tests.MinimumLevelOverrides.config")]
        public void SupportForMinimumLevelOverrides(string fileName)
        {
            var docs = "Minimum level can be overriden (up or down) for specific `SourceContext`s.";

            WriteDocumentation(docs, fileName);

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


        [Theory]
        [InlineData("Tests.WriteToWithNoParams.json")]
        [InlineData("Tests.WriteToWithNoParams-LongForm.json")]
        [InlineData("Tests.WriteToWithNoParams.config")]
        public void SupportForSinksWithoutParameters(string fileName)
        {
            var docs = "Sinks without mandatory arguments can be called.";

            WriteDocumentation(docs, fileName);

            var loggerConfig = LoadConfig(fileName);

            var logger = loggerConfig.CreateLogger();
            logger.Error("This should be written to Dummy");
            var e = TestDummies.DummySink.Emitted.FirstOrDefault();
            Assert.NotNull(e);
        }

        [Theory]
        [InlineData("Tests.WriteToWithRestrictedToMinimumLevel.json")]
        [InlineData("Tests.WriteToWithRestrictedToMinimumLevel.config")]
        public void SupportForLogEventLevelParameters(string fileName)
        {
            var docs = "Parameters of type `LogEventLevel` such as `restrictedToMinimumLevel` can be provided";

            WriteDocumentation(docs, fileName);

            var loggerConfig = LoadConfig(fileName);

            var logger = loggerConfig.CreateLogger();
            logger.Warning("This should not be written because `restrictedToMinimumLevel` = `Error`");
            Assert.Null(TestDummies.DummySink.Emitted.FirstOrDefault());

            logger.Error("This should be written because `restrictedToMinimumLevel` = `Error`");
            Assert.NotNull(TestDummies.DummySink.Emitted.FirstOrDefault());
        }

        [Theory]
        [InlineData("Tests.WriteToWithSimpleParams.json")]
        [InlineData("Tests.WriteToWithSimpleParams.config")]
        public void SupportForSimpleTypesParameters(string fileName)
        {
            var docs = "Simple types that are *convertible* from string can be passed. " +
                       "Empty string can be provided to specify null for nullable parameters. " +
                       "Parameters with a default value can be omitted.";

            WriteDocumentation(docs, fileName);

            var loggerConfig = LoadConfig(fileName);

            loggerConfig.CreateLogger();
            Assert.Equal("A string param", TestDummies.DummySink.StringParam);
            Assert.Equal(666, TestDummies.DummySink.IntParam);
            Assert.Null(TestDummies.DummySink.NullableIntParam);
            Assert.Equal("default", TestDummies.DummySink.StringParamWithDefault);
        }

        [Theory]
        [InlineData("Tests.WriteToWithConcreteDefaultImplementationOfInterface.json")]
        [InlineData("Tests.WriteToWithConcreteDefaultImplementationOfInterface.config")]
        public void SupportForInterfaceParamsPassingConcreteClassWithDefaultConstructor(string fileName)
        {
            var docs = "For parameters whose type is an `interface`, the full type name of an implementation " +
                       "can be provided. If the type is not in the `Serilog`, remember to include `using` directives." +
                       "**TODO** : investigate.... Configuration seems to require the assembly name, but AppSettings doesn't !";

            WriteDocumentation(docs, fileName);

            var loggerConfig = LoadConfig(fileName);

            loggerConfig.CreateLogger();
            Assert.NotNull(TestDummies.DummySink.Formatter);
            Assert.IsType<JsonFormatter>(TestDummies.DummySink.Formatter);
        }

        [Theory]
        [InlineData("Tests.EnvironmentVariableExpansion.json")]
        [InlineData("Tests.EnvironmentVariableExpansion.config")]
        public void SupportEnvironmentVariableExpansion(string fileName)
        {
            var docs = "Values like `%ENV_VARIABLE%` are replaced by the value of the environment variable `ENV_VARIABLE`.";

            WriteDocumentation(docs, fileName);

            var loggerConfig = LoadConfig(fileName);

            loggerConfig.CreateLogger();
            Assert.NotEqual("%PATH%", TestDummies.DummySink.StringParam);
            Assert.Equal(Environment.GetEnvironmentVariable("PATH"), TestDummies.DummySink.StringParam);
            Assert.NotEqual(0, TestDummies.DummySink.IntParam);
            Assert.Equal(Convert.ToInt32(Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS")), TestDummies.DummySink.IntParam);
        }

        [Theory]
        [InlineData("Tests.EnrichWithProperty.json")]
        [InlineData("Tests.EnrichWithProperty.config")]
        public void SupportForPropertyEnrichment(string fileName)
        {
            var docs = "Log events can be enriched with arbitrary properties.";

            WriteDocumentation(docs, fileName);

            var loggerConfig = LoadConfig(fileName);

            LogEvent e = null;
            var logger = loggerConfig
                .WriteTo.Sink(new DelegatingSink(le => e = le)).CreateLogger();

            logger.Information("This will be enriched with 2 properties");
            Assert.NotNull(e);
            Assert.Equal("MyApp", e.Properties["AppName"].LiteralValue());
            Assert.Equal("MyServer", e.Properties["ServerName"].LiteralValue());
        }

        [Theory]
        [InlineData("Tests.EnrichFromLogContext.csx")]
        [InlineData("Tests.EnrichFromLogContext.json")]
        [InlineData("Tests.EnrichFromLogContext.config")]
        public void SupportForOutOfTheBoxEnrichmentExtensionMethod(string fileName)
        {
            var docs = "Log events can be enriched with LogContext.";

            WriteDocumentation(docs, fileName);

            var loggerConfig = LoadConfig(fileName);

            LogEvent e = null;
            var logger = loggerConfig
                .WriteTo.Sink(new DelegatingSink(le => e = le)).CreateLogger();
            using (LogContext.PushProperty("LogContextProperty", "value"))
            {
                logger.Information("This will be enriched with a property");
            }

            Assert.NotNull(e);
            Assert.Equal("value", e.Properties["LogContextProperty"].LiteralValue());
        }

        [Theory]
        [InlineData("Tests.EnrichWithExternalEnricher.csx")]
        [InlineData("Tests.EnrichWithExternalEnricher.json")]
        [InlineData("Tests.EnrichWithExternalEnricher.config")]
        public void SupportForArbitraryEnrichmentExtensionMethod(string fileName)
        {
            var docs = "Log events can be enriched with arbitrary `Enrich.With...()` extension methods.";

            WriteDocumentation(docs, fileName);

            var loggerConfig = LoadConfig(fileName);

            LogEvent e = null;
            var logger = loggerConfig
                .WriteTo.Sink(new DelegatingSink(le => e = le)).CreateLogger();

            logger.Information("This will be enriched with 2 properties");
            Assert.NotNull(e);
            Assert.Equal(Guid.Empty.ToString(), e.Properties["ThreadId"].LiteralValue());
            Assert.Equal("UserExtraParam", e.Properties["UserName"].LiteralValue());
        }

        // TODO : implementation class for Interface or Abstract class
        // TODO : static property accessor
        // TODO : out of the box conversions : Uri, TimeSpan ... 
        // TODO : AuditTo
        // TODO : LoggingLevelSwitch
        // TODO : filters

        void WriteDocumentation(string markdownDescription, string fileName)
        {
            var fullFilePath = GetTestFileFullPath(fileName);

            _outputHelper.WriteLine(markdownDescription);
            _outputHelper.WriteLine("");
            _outputHelper.WriteLine($"ex: {fileName}");
            _outputHelper.WriteLine("");
            _outputHelper.WriteLine("```" + GetMarkdownSnippetLanguage(fileName));
            _outputHelper.WriteLine(File.ReadAllText(fullFilePath));
            _outputHelper.WriteLine("```");
            _outputHelper.WriteLine("");
        }

        static string GetMarkdownSnippetLanguage(string fileName)
        {
            var mdOuputFormat = fileName.Split('.').Last();
            if (mdOuputFormat == "csx") mdOuputFormat = "csharp";
            if (mdOuputFormat == "config") mdOuputFormat = "xml";
            return mdOuputFormat;
        }

        static LoggerConfiguration LoadConfig(string fileName)
        {
            var fullFilePath = GetTestFileFullPath(fileName);
            if (fileName.EndsWith(".json")) return LoadJsonConfig(fullFilePath);
            if (fileName.EndsWith(".config")) return LoadXmlConfig(fullFilePath);
            if (fileName.EndsWith(".csx")) return LoadCSharpConfig(fullFilePath);
            throw new ArgumentException($"Only .json and .config are supported. Provided value : {fileName}", nameof(fileName));
        }

        static string GetTestFileFullPath(string fileName)
        {
            var fullFilePath = Path.Combine("Samples", fileName);
            return fullFilePath;
        }

        static LoggerConfiguration LoadXmlConfig(string fileName)
        {
            var xmlConfig = new LoggerConfiguration().ReadFrom.AppSettings(filePath: fileName);
            return xmlConfig;
        }

        static LoggerConfiguration LoadJsonConfig(string fileName)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(fileName, optional: false)
                .Build();

            var jsonConfig = new LoggerConfiguration().ReadFrom.Configuration(config);
            return jsonConfig;
        }

        static LoggerConfiguration LoadCSharpConfig(string fileName)
        {
            var code = File.ReadAllText(fileName);

            var jsonConfig = new LoggerConfiguration().ReadFrom.CodeString(code);
            return jsonConfig;
        }
    }
}
