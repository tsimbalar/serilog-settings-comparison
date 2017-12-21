#r ".\TestDummies.dll"
using Serilog.Events;
using TestDummies;

LoggerConfiguration
    .WriteTo.Dummy(restrictedToMinimumLevel: LogEventLevel.Error);
