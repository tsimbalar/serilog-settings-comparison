#r ".\TestDummies.dll"
using System;
using TestDummies;
using TestDummies.Console;

LoggerConfiguration
    .WriteTo.DummyConsole(theme: new CustomConsoleTheme());
