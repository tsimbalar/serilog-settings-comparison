#r "C:\Dev\serilog-settings-comparison\test\Serilog.Settings.Comparison.Tests\bin\Debug\net46\TestDummies.dll"
using System;
using TestDummies;


LoggerConfiguration
    .WriteTo.Dummy(
        stringParam: Environment.ExpandEnvironmentVariables("%PATH%"),
        intParam: Int32.Parse(Environment.ExpandEnvironmentVariables("%NUMBER_OF_PROCESSORS%")));
