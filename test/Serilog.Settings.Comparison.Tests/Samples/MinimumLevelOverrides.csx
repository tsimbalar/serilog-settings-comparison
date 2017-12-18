using Serilog.Events;

LoggerConfiguration
  .MinimumLevel.Debug()
  .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
  .MinimumLevel.Override("Microsoft.Extensions", LogEventLevel.Information)
  .MinimumLevel.Override("System", LogEventLevel.Debug)
  ;
