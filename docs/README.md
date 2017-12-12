## Something
Parameters of type `LogEventLevel` such as `restrictedToMinimumLevel` can be provided


ex: `Tests.WriteToWithRestrictedToMinimumLevel.json`

```json
{
  "Serilog": {
    "Using": [ "TestDummies" ],
    "WriteTo": [
      {
        "Name": "Dummy",
        "Args": {
          "restrictedToMinimumLevel": "Error"
        }
      }
    ]
  }
}
```


ex: `Tests.WriteToWithRestrictedToMinimumLevel.config`

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:TestDummies" value="TestDummies" />
    <add key="serilog:write-to:Dummy.restrictedToMinimumLevel" value="Error" />
  </appSettings>
</configuration>
```


## Something
Minimum level can be overriden (up or down) for specific `SourceContext`s.


ex: `Tests.MinimumLevelOverrides.json`

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Verbose",
      "Override": {
        "Microsoft": "Error",
        "Microsoft.Extensions": "Information",
        "System": "Debug"
      }
    }
  }
}
```


ex: `Tests.MinimumLevelOverrides.config`

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:minimum-level" value="Verbose" />
    <add key="serilog:minimum-level:override:Microsoft" value="Error" />
    <add key="serilog:minimum-level:override:Microsoft.Extensions" value="Information" />
    <add key="serilog:minimum-level:override:System" value="Debug" />
  </appSettings>
</configuration>
```


## Something
Simple types that are *convertible* from string can be passed. Empty string can be provided to specify null for nullable parameters. Parameters with a default value can be omitted.


ex: `Tests.WriteToWithSimpleParams.json`

```json
{
  "Serilog": {
    "Using": [ "TestDummies" ],
    "WriteTo": [
      {
        "Name": "Dummy",
        "Args": {
          "stringParam": "A string param",
          "intParam": 666,
          "nullableIntParam": ""
        }
      }
    ]
  }
}
```


ex: `Tests.WriteToWithSimpleParams.config`

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:TestDummies" value="TestDummies" />
    <add key="serilog:write-to:Dummy.stringParam" value="A string param" />
    <add key="serilog:write-to:Dummy.intParam" value="666" />
    <add key="serilog:write-to:Dummy.nullableIntParam" value="" />
  </appSettings>
</configuration>
```


## Something
Values like `%ENV_VARIABLE%` are replaced by the value of the environment variable `ENV_VARIABLE`.


ex: `Tests.EnvironmentVariableExpansion.json`

```json
{
  "Serilog": {
    "Using": [ "TestDummies" ],
    "WriteTo": [
      {
        "Name": "Dummy",
        "Args": {
          "stringParam": "%PATH%",
          "intParam": "%NUMBER_OF_PROCESSORS%"
        }
      }
    ]
  }
}
```


ex: `Tests.EnvironmentVariableExpansion.config`

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:TestDummies" value="TestDummies" />
    <add key="serilog:write-to:Dummy.stringParam" value="%PATH%" />
    <add key="serilog:write-to:Dummy.intParam" value="%NUMBER_OF_PROCESSORS%" />
  </appSettings>
</configuration>
```


## Something
For parameters whose type is an `interface`, the full type name of an implementation can be provided. If the type is not in the `Serilog`, remember to include `using` directives.**TODO** : investigate.... Configuration seems to require the assembly name, but AppSettings doesn't !


ex: `Tests.WriteToWithConcreteDefaultImplementationOfInterface.json`

```json
{
  "Serilog": {
    "Using": [ "TestDummies" ],
    "WriteTo": [
      {
        "Name": "DummyWithFormatter",
        "Args": {
          "formatter": "Serilog.Formatting.Json.JsonFormatter, Serilog"
        }
      }
    ]
  }
}

```


ex: `Tests.WriteToWithConcreteDefaultImplementationOfInterface.config`

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:TestDummies" value="TestDummies" />
    <add key="serilog:write-to:DummyWithFormatter.formatter" value="Serilog.Formatting.Json.JsonFormatter" />
  </appSettings>
</configuration>

```


## Something
Loading an empty config file behaves the same as the default `CreateLogger()`. Minimum Level is *Information*.


ex: `Tests.Empty.csx`

```csharp

```


ex: `Tests.Empty.json`

```json
{
}
```


ex: `Tests.Empty-EmptySection.json`

```json
{
  "Serilog": {}
}
```


ex: `Tests.Empty.config`

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
</configuration>
```


ex: `Tests.Empty-EmptySection.config`

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
  </appSettings>
</configuration>
```


## Something
Sinks without mandatory arguments can be called.


ex: `Tests.WriteToWithNoParams.json`

```json
{
  "Serilog": {
    "Using": [ "TestDummies" ],
    "WriteTo": [ "Dummy" ]
  }
}

```


ex: `Tests.WriteToWithNoParams-LongForm.json`

```json
{
  "Serilog": {
    "Using": [ "TestDummies" ],
    "WriteTo": [
      { "Name": "Dummy" }
    ]
  }
}
```


ex: `Tests.WriteToWithNoParams.config`

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:TestDummies" value="TestDummies" />
    <add key="serilog:write-to:Dummy" />
  </appSettings>
</configuration>
```


## Something
Log events can be enriched with arbitrary properties.


ex: `Tests.EnrichWithProperty.json`

```json
{
  "Serilog": {
    "Properties": {
      "AppName": "MyApp",
      "ServerName": "MyServer"
    }
  }
}
```


ex: `Tests.EnrichWithProperty.config`

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:enrich:with-property:AppName" value="MyApp" />
    <add key="serilog:enrich:with-property:ServerName" value="MyServer" />
  </appSettings>
</configuration>
```


## Something
Global Minimum level can be defined.


ex: `Tests.MinimumLevel.csx`

```csharp
LoggerConfiguration
    .MinimumLevel.Warning();

```


ex: `Tests.MinimumLevel-is.csx`

```csharp
using Serilog.Events;

LoggerConfiguration
    .MinimumLevel.Is(LogEventLevel.Warning);

```


ex: `Tests.MinimumLevel.json`

```json
{
  "Serilog": {
    "MinimumLevel": "Warning"
  }
}
```


ex: `Tests.MinimumLevel-Default.json`

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Warning"
    }
  }
}
```


ex: `Tests.MinimumLevel.config`

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:minimum-level" value="Warning" />
  </appSettings>
</configuration>
```


## Something
Log events can be enriched with arbitrary `Enrich.With...()` extension methods.


ex: `Tests.EnrichWithExternalEnricher.csx`

```csharp
#r "C:\Dev\serilog-settings-comparison\test\Serilog.Settings.Comparison.Tests\bin\Debug\net46\TestDummies.dll"
using TestDummies;

LoggerConfiguration
    .Enrich.WithDummyThreadId()
    .Enrich.WithDummyUserName("UserExtraParam");

```


ex: `Tests.EnrichWithExternalEnricher.json`

```json
{
  "Serilog": {
    "Using": [ "TestDummies" ],
    "Enrich": [
      "WithThreadId",
      {
        "Name": "WithDummyUserName",
        "Args": {
          "extraParam": "UserExtraParam"
        }
      }
    ]
  }
}
```


ex: `Tests.EnrichWithExternalEnricher.config`

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:TestDummies" value="TestDummies" />
    <add key="serilog:enrich:WithDummyThreadId" value="" />
    <add key="serilog:enrich:WithDummyUserName.extraParam" value="UserExtraParam" />
  </appSettings>
</configuration>
```


## Something
Log events can be enriched with LogContext.


ex: `Tests.EnrichFromLogContext.csx`

```csharp
LoggerConfiguration.Enrich.FromLogContext();

```


ex: `Tests.EnrichFromLogContext.json`

```json
{
  "Serilog": {
    "Enrich": [ "FromLogContext" ]
  }
}

```


ex: `Tests.EnrichFromLogContext.config`

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:enrich:FromLogContext" value="" />
  </appSettings>
</configuration>

```


