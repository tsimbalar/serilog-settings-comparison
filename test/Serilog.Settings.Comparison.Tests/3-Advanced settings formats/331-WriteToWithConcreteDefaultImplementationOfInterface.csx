#r ".\TestDummies.dll"
using System;
using Serilog.Formatting.Json;
using TestDummies;

LoggerConfiguration
    .WriteTo.DummyWithFormatter(formatter: new JsonFormatter());
