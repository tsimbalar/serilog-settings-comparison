# Serilog Settings documentation
Serilog is typically configured via code though its *configuration APIs*, but it is quite common to specify the settings also from some sort of configuration file. 

For that purpose, several *Settings providers* exist, that mimic the *code-based* API and allow supplying values from external sources : 
- *Serilog.Settings.AppSettings* allows to read configuration from the `<appSettings>` section of an `App.config` or `Web.config` file,
- *Serilog.Settings.Configuration* relies on *Microsoft.Logging.Configuration* to read settings from sources in *JSON*, *XML* or anything that can be plugged in the *ConfigurationProvider* APIs

To use *Serilog.Settings.AppSettings*, install the Nuget package, and configure you logger like : 
```csharp
new LoggerConfiguration().ReadFrom.AppSettings()
    // snip ...
    .CreateLogger();
```

To use *Serilog.Settings.Configuration*, install the Nuget package, and configure you logger like : 
```csharp
var config = new ConfigurationBuilder()
    .AddJsonFile(fileName, optional: false) // or possibly other sources
    .Build();

new LoggerConfiguration().ReadFrom.Configuration(config)
    // snip ...
    .CreateLogger();
```


You will find below some snippets of common configuration code in *C#* with the equivalent settings in *JSON* and *XML*.

## Basics


### Empty settings
Loading an empty config file behaves the same as the default `CreateLogger()`. Minimum Level is *Information*.


<!--
- in **C#** (ex : `101-Empty.csx`)

```csharp

```

-->

<!--
- in **JSON** (ex : `101-Empty.json`)

```json
{
}
```

-->

- in **JSON** (ex : `101-Empty-EmptySection.json`)

```json
{
  "Serilog": {}
}
```


<!--
- in **XML** (ex : `101-Empty.config`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
</configuration>
```

-->

- in **XML** (ex : `101-Empty-EmptySection.config`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
  </appSettings>
</configuration>
```


### Minimum Level
Global Minimum level can be defined.


- in **C#** (ex : `110-MinimumLevel.csx`)

```csharp
LoggerConfiguration
    .MinimumLevel.Warning();

```


<!--
- in **C#** (ex : `110-MinimumLevel-is.csx`)

```csharp
using Serilog.Events;

LoggerConfiguration
    .MinimumLevel.Is(LogEventLevel.Warning);

```

-->

- in **JSON** (ex : `110-MinimumLevel.json`)

```json
{
  "Serilog": {
    "MinimumLevel": "Warning"
  }
}
```


<!--
- in **JSON** (ex : `110-MinimumLevel-Default.json`)

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Warning"
    }
  }
}
```

-->

- in **XML** (ex : `110-MinimumLevel.config`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:minimum-level" value="Warning" />
  </appSettings>
</configuration>
```


### Sinks - Basics
You can configure usage of a given *Sink* by specifying the name of the method or extension method that you would usually use after `WriteTo.*`.
You may need to explicitly add a `using` directive to look for extension methods in a separate assembly or Nuget package.


- in **C#** (ex : `120-WriteToWithNoParams.csx`)

```csharp
#r ".\TestDummies.dll"
using System;
using TestDummies;

LoggerConfiguration
    .WriteTo.Dummy();

```


- in **JSON** (ex : `120-WriteToWithNoParams.json`)

```json
{
  "Serilog": {
    "Using": [ "TestDummies" ],
    "WriteTo": [ "Dummy" ]
  }
}

```


<!--
- in **JSON** (ex : `120-WriteToWithNoParams-LongForm.json`)

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

-->

- in **XML** (ex : `120-WriteToWithNoParams.config`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:TestDummies" value="TestDummies" />
    <add key="serilog:write-to:Dummy" />
  </appSettings>
</configuration>
```


### Sinks - `restrictedToMinimumLevel`
Parameters of type `LogEventLevel` such as `restrictedToMinimumLevel` can be provided from the level's name.


- in **C#** (ex : `125-WriteToRestrictedToMinimumLevel.csx`)

```csharp
#r ".\TestDummies.dll"
using Serilog.Events;
using TestDummies;

LoggerConfiguration
    .WriteTo.Dummy(restrictedToMinimumLevel: LogEventLevel.Error);

```


- in **JSON** (ex : `125-WriteToRestrictedToMinimumLevel.json`)

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


- in **XML** (ex : `125-WriteToRestrictedToMinimumLevel.config`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:TestDummies" value="TestDummies" />
    <add key="serilog:write-to:Dummy.restrictedToMinimumLevel" value="Error" />
  </appSettings>
</configuration>
```


### Sinks - Simple parameter types
Simple types that are *convertible* from string can be passed. Empty string can be provided to specify null for nullable parameters. Parameters with a default value can be omitted.


- in **C#** (ex : `128-WriteToWithSimpleParams.csx`)

```csharp
#r ".\TestDummies.dll"
using TestDummies;

LoggerConfiguration
    .WriteTo.Dummy(stringParam: "A string param", intParam: 666, nullableIntParam: null);

```


- in **JSON** (ex : `128-WriteToWithSimpleParams.json`)

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


- in **XML** (ex : `128-WriteToWithSimpleParams.config`)

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


### Property-Enrichment
Log events can be enriched with arbitrary properties.


- in **C#** (ex : `130-EnrichWithProperty.csx`)

```csharp
LoggerConfiguration
    .Enrich.WithProperty("AppName", "MyApp")
    .Enrich.WithProperty("ServerName", "MyServer");

```


- in **JSON** (ex : `130-EnrichWithProperty.json`)

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


- in **XML** (ex : `130-EnrichWithProperty.config`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:enrich:with-property:AppName" value="MyApp" />
    <add key="serilog:enrich:with-property:ServerName" value="MyServer" />
  </appSettings>
</configuration>
```


# Advanced scenarios
The following scenarios are also supported.


### Minimum level overrides
Minimum level can be overriden (up or down) for specific `SourceContext`s.


- in **C#** (ex : `210-MinimumLevelOverrides.csx`)

```csharp
using Serilog.Events;

LoggerConfiguration
  .MinimumLevel.Verbose()
  .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
  .MinimumLevel.Override("Microsoft.Extensions", LogEventLevel.Information)
  .MinimumLevel.Override("System", LogEventLevel.Debug)
  ;

```


- in **JSON** (ex : `210-MinimumLevelOverrides.json`)

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


- in **XML** (ex : `210-MinimumLevelOverrides.config`)

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


### Sinks - `AuditTo`
Some sinks provide *Audit* functionality via the configuration method `.AuditTo.MySink()`. This is also supported via configuration.


- in **C#** (ex : `221-AuditToWithSimpleParams.csx`)

```csharp
#r ".\TestDummies.dll"
using TestDummies;

LoggerConfiguration
    .AuditTo.Dummy(stringParam: "A string param", intParam: 666, nullableIntParam: null);

```


- in **JSON** (ex : `221-AuditToWithSimpleParams.json`)

```json
{
  "Serilog": {
    "Using": [ "TestDummies" ],
    "AuditTo": [
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


- in **XML** (ex : `221-AuditToWithSimpleParams.config`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:TestDummies" value="TestDummies" />
    <add key="serilog:audit-to:Dummy.stringParam" value="A string param" />
    <add key="serilog:audit-to:Dummy.intParam" value="666" />
    <add key="serilog:audit-to:Dummy.nullableIntParam" value="" />
  </appSettings>
</configuration>

```


### Enrichment from `LogContext`
Log events can be enriched with `LogContext`.


- in **C#** (ex : `230-EnrichFromLogContext.csx`)

```csharp
LoggerConfiguration.Enrich.FromLogContext();

```


- in **JSON** (ex : `230-EnrichFromLogContext.json`)

```json
{
  "Serilog": {
    "Enrich": [ "FromLogContext" ]
  }
}

```


- in **XML** (ex : `230-EnrichFromLogContext.config`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:enrich:FromLogContext" value="" />
  </appSettings>
</configuration>

```


# Advanced settings formats
Below are the general rules for setting values.


## Method Discovery


### Enrichment Extension Methods
Log events can be enriched with arbitrary `Enrich.With...()` extension methods.


- in **C#** (ex : `311-EnrichWithExternalEnricher.csx`)

```csharp
#r ".\TestDummies.dll"
using TestDummies;

LoggerConfiguration
    .Enrich.WithDummyThreadId()
    .Enrich.WithDummyUserName("UserExtraParam");

```


- in **JSON** (ex : `311-EnrichWithExternalEnricher.json`)

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


- in **XML** (ex : `311-EnrichWithExternalEnricher.config`)

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


## Interfaces and abstract classes


### Interface-typed parameters
For parameters whose type is an `interface`, the full type name of an implementation can be provided. If the type is not in the `Serilog`, remember to include `using` directives.**TODO** : investigate.... Configuration seems to require the assembly name, but AppSettings doesn't !


- in **C#** (ex : `331-WriteToWithConcreteDefaultImplementationOfInterface.csx`)

```csharp
#r ".\TestDummies.dll"
using System;
using Serilog.Formatting.Json;
using TestDummies;

LoggerConfiguration
    .WriteTo.DummyWithFormatter(formatter: new JsonFormatter());

```


- in **JSON** (ex : `331-WriteToWithConcreteDefaultImplementationOfInterface.json`)

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


- in **XML** (ex : `331-WriteToWithConcreteDefaultImplementationOfInterface.config`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:TestDummies" value="TestDummies" />
    <add key="serilog:write-to:DummyWithFormatter.formatter" value="Serilog.Formatting.Json.JsonFormatter" />
  </appSettings>
</configuration>

```


### Abstract-class-typed parameters
For parameters whose type is an `abstract class`, the full type name of an implementation can be provided. If the type is not in the `Serilog`, remember to include `using` directives.


- in **C#** (ex : `332-WriteToWithConcreteDefaultImplementationOfAbstractClass.csx`)

```csharp
#r ".\TestDummies.dll"
using System;
using TestDummies;
using TestDummies.Console;

LoggerConfiguration
    .WriteTo.DummyConsole(theme: new CustomConsoleTheme());

```


- in **XML** (ex : `332-WriteToWithConcreteDefaultImplementationOfAbstractClass.config`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:TestDummies" value="TestDummies" />
    <add key="serilog:write-to:DummyConsole.theme" value="TestDummies.Console.CustomConsoleTheme, TestDummies" />
  </appSettings>
</configuration>

```


## Environment variable expansion
Values like `%ENV_VARIABLE%` are replaced by the value of the environment variable `ENV_VARIABLE`.
This can be used, for instance, to provide environment-dependent property-enrichment (ex: `%COMPUTERNAME%`) or paths (ex: %TEMP%).


- in **C#** (ex : `390-EnvironmentVariableExpansion.csx`)

```csharp
#r ".\TestDummies.dll"
using System;
using TestDummies;


LoggerConfiguration
    .WriteTo.Dummy(
        stringParam: Environment.ExpandEnvironmentVariables("%PATH%"),
        intParam: Int32.Parse(Environment.ExpandEnvironmentVariables("%NUMBER_OF_PROCESSORS%")));

```


- in **JSON** (ex : `390-EnvironmentVariableExpansion.json`)

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


- in **XML** (ex : `390-EnvironmentVariableExpansion.config`)

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


