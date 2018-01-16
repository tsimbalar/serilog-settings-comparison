#r ".\TestDummies.dll"
using Serilog.Events;
using TestDummies;

LoggerConfiguration
    .Filter.ByExcludingLevel(LogEventLevel.Warning)
    .Enrich.WithDummyUserName("UserExtraParam")
    .AuditTo.Dummy(stringParam: "A string param", intParam: 666)
    .WriteTo.Dummy()
    ;
