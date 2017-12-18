#r "C:\Dev\serilog-settings-comparison\test\Serilog.Settings.Comparison.Tests\bin\Debug\net46\TestDummies.dll"
using Serilog.Events;
using TestDummies;

LoggerConfiguration
    .WriteTo.Dummy(restrictedToMinimumLevel: LogEventLevel.Error);
