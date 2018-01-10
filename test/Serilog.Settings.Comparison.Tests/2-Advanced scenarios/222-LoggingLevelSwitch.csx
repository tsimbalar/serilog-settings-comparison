#r ".\TestDummies.dll"
using Serilog.Core;
using Serilog.Events;
using TestDummies;

var mySwitch = new LoggingLevelSwitch(LogEventLevel.Warning);

LoggerConfiguration
    .MinimumLevel.ControlledBy(mySwitch)
    .WriteTo.DummyWithLevelSwitch(controlLevelSwitch: mySwitch);
