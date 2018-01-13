#r ".\TestDummies.dll"
using TestDummies;

LoggerConfiguration
    .WriteTo.Logger(lc => lc
        .Enrich.WithProperty("Prop1", "PropValue1")
        .WriteTo.DummyConsole()
        )
    .WriteTo.Logger(lc => lc
        .Enrich.WithProperty("Prop2", "PropValue2")
        .WriteTo.Dummy()
    );

