#r ".\TestDummies.dll"
using System;
using TestDummies;

LoggerConfiguration
    .WriteTo.DummyWithManyParams(
        enumParam: MyEnum.Qux,
        timespanParam: new TimeSpan(2, 3, 4, 5),
        uriParam: new Uri("https://www.serilog.net"));
