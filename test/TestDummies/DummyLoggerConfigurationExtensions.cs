using System;
using Serilog;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Configuration;
using Serilog.Core;
using TestDummies.Console;
using TestDummies.Console.Themes;

namespace TestDummies
{
    public static class DummyLoggerConfigurationExtensions
    {
        public static LoggerConfiguration WithDummyThreadId(this LoggerEnrichmentConfiguration enrich)
        {
            return enrich.With(new DummyEnricher());
        }

        public static LoggerConfiguration WithDummyUserName(this LoggerEnrichmentConfiguration enrich, string extraParam)
        {
            return enrich.With(new DummyEnricher(extraParam));
        }

        public static LoggerConfiguration Dummy(
            this LoggerSinkConfiguration loggerSinkConfiguration
        )
        {
            return loggerSinkConfiguration.Dummy(LevelAlias.Minimum);
        }

        public static LoggerConfiguration Dummy(
            this LoggerSinkConfiguration loggerSinkConfiguration,
            LogEventLevel restrictedToMinimumLevel)
        {
            return loggerSinkConfiguration.Dummy("not provided", -1, restrictedToMinimumLevel: restrictedToMinimumLevel);
        }

        public static LoggerConfiguration Dummy(
            this LoggerSinkConfiguration loggerSinkConfiguration,
            string stringParam,
            int intParam,
            string stringParamWithDefault = "default",
            int? nullableIntParam = 42,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum
            )
        {
            return loggerSinkConfiguration.Sink(new DummySink(stringParam, intParam, stringParamWithDefault, nullableIntParam, null),
                restrictedToMinimumLevel);
        }

        // AuditTo
        public static LoggerConfiguration Dummy(
            this LoggerAuditSinkConfiguration loggerSinkConfiguration,
            string stringParam,
            int intParam,
            string stringParamWithDefault = "default",
            int? nullableIntParam = 42,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
        {
            return loggerSinkConfiguration.Sink(new DummyAuditSink(stringParam, intParam, stringParamWithDefault, nullableIntParam, null),
                restrictedToMinimumLevel);
        }

        public static LoggerConfiguration DummyWithFormatter(
            this LoggerSinkConfiguration loggerSinkConfiguration,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            ITextFormatter formatter = null
        )
        {
            return loggerSinkConfiguration.Sink(new DummySink(null, 0, null, null, formatter),
                restrictedToMinimumLevel);
        }

        public static LoggerConfiguration DummyRollingFile(
            this LoggerAuditSinkConfiguration loggerSinkConfiguration,
            string pathFormat,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            string outputTemplate = null,
            IFormatProvider formatProvider = null)
        {
            return loggerSinkConfiguration.Sink(new DummyRollingFileAuditSink(), restrictedToMinimumLevel);
        }

        public static LoggerConfiguration DummyWithLevelSwitch(
            this LoggerSinkConfiguration loggerSinkConfiguration,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            LoggingLevelSwitch controlLevelSwitch = null)
        {
            return loggerSinkConfiguration.Sink(new DummyWithLevelSwitchSink(controlLevelSwitch), restrictedToMinimumLevel);
        }

        public static LoggerConfiguration DummyConsole(
            this LoggerSinkConfiguration loggerSinkConfiguration,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            ConsoleTheme theme = null)
        {
            return loggerSinkConfiguration.Sink(new DummyConsoleSink(theme), restrictedToMinimumLevel);
        }

        //public static LoggerConfiguration Dummy(
        //    this LoggerSinkConfiguration loggerSinkConfiguration,
        //    Action<LoggerSinkConfiguration> wrappedSinkAction)
        //{
        //    return LoggerSinkConfiguration.Wrap(
        //        loggerSinkConfiguration,
        //        s => new DummyWrappingSink(s),
        //        wrappedSinkAction);
        //}
    }
}
