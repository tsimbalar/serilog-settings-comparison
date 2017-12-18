#r "C:\Dev\serilog-settings-comparison\test\Serilog.Settings.Comparison.Tests\bin\Debug\net46\TestDummies.dll"
using System;
using Serilog.Formatting.Json;
using TestDummies;

LoggerConfiguration
    .WriteTo.DummyWithFormatter(formatter: new JsonFormatter());
