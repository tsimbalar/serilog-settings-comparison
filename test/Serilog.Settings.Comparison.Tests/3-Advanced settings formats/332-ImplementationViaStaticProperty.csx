#r ".\TestDummies.dll"
#r ".\Serilog.Settings.Comparison.Tests.dll"
using System;
using Serilog.Formatting.Json;
using TestDummies;
using TestDummies.Console;
using TestDummies.Console.Themes;
using Serilog.SettingsComparisonTests.Support.Formatting;

LoggerConfiguration
    .WriteTo.DummyWithFormatter(formatter: CustomFormatters.Formatter)
    .WriteTo.DummyConsole(theme: ConsoleThemes.Theme1);
