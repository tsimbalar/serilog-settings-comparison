#r ".\TestDummies.dll"
using System;
using TestDummies;


LoggerConfiguration
    .WriteTo.Dummy(
        stringParam: Environment.ExpandEnvironmentVariables("%PATH%"),
        intParam: Int32.Parse(Environment.ExpandEnvironmentVariables("%NUMBER_OF_PROCESSORS%")));
