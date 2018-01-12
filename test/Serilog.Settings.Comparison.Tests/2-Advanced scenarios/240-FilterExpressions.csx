#r ".\Serilog.Filters.Expressions.dll"

LoggerConfiguration
    .Filter.ByExcluding("filter = 'exclude'");
