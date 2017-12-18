#r ".\TestDummies.dll"
using TestDummies;

LoggerConfiguration
    .WriteTo.Dummy(stringParam: "A string param", intParam: 666, nullableIntParam: null);
