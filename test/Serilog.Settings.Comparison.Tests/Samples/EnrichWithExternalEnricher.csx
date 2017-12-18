#r "C:\Dev\serilog-settings-comparison\test\Serilog.Settings.Comparison.Tests\bin\Debug\net46\TestDummies.dll"
using TestDummies;

LoggerConfiguration
    .Enrich.WithDummyThreadId()
    .Enrich.WithDummyUserName("UserExtraParam");
