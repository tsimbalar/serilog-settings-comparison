#r ".\TestDummies.dll"
using TestDummies;

LoggerConfiguration
    .Enrich.WithDummyThreadId()
    .Enrich.WithDummyUserName("UserExtraParam");
