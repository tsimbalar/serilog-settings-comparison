using Serilog.Formatting;

namespace Serilog.SettingsComparisonTests.Support.Formatting
{
    public static class CustomFormatters
    {
        public static ITextFormatter Formatter {get;} = new MyFormatter();
    }
}
