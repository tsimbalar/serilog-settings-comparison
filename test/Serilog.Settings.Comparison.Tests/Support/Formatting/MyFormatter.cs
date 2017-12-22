using System;
using System.IO;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.SettingsComparisonTests.Support.Formatting
{
    internal class MyFormatter : ITextFormatter
    {
        public void Format(LogEvent logEvent, TextWriter output)
        {
            throw new NotImplementedException();
        }
    }
}
