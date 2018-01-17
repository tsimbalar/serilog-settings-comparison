using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace TestDummies
{
    /// <summary>
    /// Just a dummy sink that allows to verify that the proper params are passed from configuration
    /// </summary>
    public class DummyAuditSink : ILogEventSink
    {

        public DummyAuditSink(string stringParam, int intParam, string stringParamWithDefault, int? nullableIntParam, ITextFormatter formatter)
        {
            StringParam = stringParam;
            IntParam = intParam;
            StringParamWithDefault = stringParamWithDefault;
            NullableIntParam = nullableIntParam;
            Formatter = formatter;
        }

        [ThreadStatic]
        public static ITextFormatter Formatter;
        [ThreadStatic]
        public static string StringParam;
        [ThreadStatic]
        public static int IntParam;
        [ThreadStatic]
        public static string StringParamWithDefault;
        [ThreadStatic]
        public static int? NullableIntParam;


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
            Formatter = null;
            IntParam = default(int);
            NullableIntParam = null;
            StringParam = null;
            StringParamWithDefault = null;
            _emitted = null;
        }
    }
}
