#r ".\TestDummies.dll"
using System;
using TestDummies;
using TestDummies.Console;
using TestDummies.Console.Themes;

LoggerConfiguration
    .WriteTo.DummyConsole(theme: ConsoleThemes.Theme1);
