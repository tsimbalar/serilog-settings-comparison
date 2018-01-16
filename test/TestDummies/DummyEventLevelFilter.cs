using Serilog.Core;
using Serilog.Events;

namespace TestDummies
{
    public class DummyEventLevelFilter : ILogEventFilter
    {
        LogEventLevel excludedLevel;

        public DummyEventLevelFilter(LogEventLevel excludedLevel)
        {
            this.excludedLevel = excludedLevel;
        }

        public bool IsEnabled(LogEvent logEvent)
        {
            return logEvent.Level != excludedLevel;
        }
    }
}