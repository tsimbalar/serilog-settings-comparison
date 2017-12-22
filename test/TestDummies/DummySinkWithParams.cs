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
        // ReSharper disable ThreadStaticFieldHasInitializer
        public static List<LogEvent> Emitted = new List<LogEvent>();
        // ReSharper restore ThreadStaticFieldHasInitializer

        public void Emit(LogEvent logEvent)
        {
            Emitted.Add(logEvent);
        }
    }
}