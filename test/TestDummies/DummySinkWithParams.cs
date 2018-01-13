using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace TestDummies
{
    public class DummySinkWithParams : ILogEventSink
    {
        public DummySinkWithParams(MyEnum enumParam, TimeSpan timespanParam, Uri uriParam)
        {
            EnumParam = enumParam;
            TimespanParam = timespanParam;
            UriParam = uriParam;
        }

        [ThreadStatic]
        public static MyEnum EnumParam;

        [ThreadStatic]
        public static TimeSpan TimespanParam;

        [ThreadStatic]
        public static Uri UriParam;

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
    }
}
