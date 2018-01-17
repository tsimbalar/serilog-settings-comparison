using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace TestDummies
{
    public class DummyWithLevelSwitchSink : ILogEventSink
    {
        public DummyWithLevelSwitchSink(LoggingLevelSwitch loggingControlLevelSwitch)
        {
            ControlLevelSwitch = loggingControlLevelSwitch;
        }

        [ThreadStatic]
        public static LoggingLevelSwitch ControlLevelSwitch;

        [ThreadStatic]
        static List<LogEvent> _emitted;

        public static List<LogEvent> Emitted
        {
            get
            {
                if (_emitted == null)
                {
                    _emitted = new List<LogEvent>();
                }
                return _emitted;
            }
        }

        public void Emit(LogEvent logEvent)
        {
            Emitted.Add(logEvent);
        }

        public static void Reset()
        {
            ControlLevelSwitch = default(LoggingLevelSwitch);
            _emitted = null;
        }
    }
}
