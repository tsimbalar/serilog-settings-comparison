using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace TestDummies
{
    public class DummySinkWithComplexParams : ILogEventSink
    {
        public DummySinkWithComplexParams(Poco poco, int[] intArray, string[] stringArray, SubPoco[] objArray)
        {
            Poco = poco;
            IntArray = intArray;
            StringArray = stringArray;
            ObjectArray = objArray;
        }

        [ThreadStatic]
        public static int[] IntArray;

        [ThreadStatic]
        public static string[] StringArray;

        [ThreadStatic]
        public static SubPoco[] ObjectArray;

        [ThreadStatic]
        public static Poco Poco;

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
            IntArray = default(int[]);
            StringArray = default(string[]);
            ObjectArray = default(SubPoco[]);
            Poco = default(Poco);

            _emitted = null;
        }
    }

    public class Poco
    {
        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
        public SubPoco Nested { get; set; }
    }

    public class SubPoco
    {
        public string SubProperty { get; set; }
    }
}
