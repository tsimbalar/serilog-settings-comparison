#r ".\TestDummies.dll"
using TestDummies;

LoggerConfiguration
    .AuditTo.Dummy(stringParam: "A string param", intParam: 666, nullableIntParam: null);
