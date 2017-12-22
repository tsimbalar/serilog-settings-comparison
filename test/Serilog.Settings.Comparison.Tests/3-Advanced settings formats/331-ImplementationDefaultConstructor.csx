#r ".\TestDummies.dll"
using System;
using Serilog.Formatting.Json;
using TestDummies;
using TestDummies.Console;

LoggerConfiguration
    .WriteTo.DummyWithFormatter(formatter: new JsonFormatter())
    .WriteTo.DummyConsole(theme: new CustomConsoleTheme());
