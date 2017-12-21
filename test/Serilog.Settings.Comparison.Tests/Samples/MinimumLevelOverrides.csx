using Serilog.Events;

LoggerConfiguration
  .MinimumLevel.Verbose()
  .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
  .MinimumLevel.Override("Microsoft.Extensions", LogEventLevel.Information)
  .MinimumLevel.Override("System", LogEventLevel.Debug)
  ;
