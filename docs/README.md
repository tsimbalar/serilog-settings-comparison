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


### Sinks - `LoggingLevelSwitch`
Some sinks such as the *Seq* sink accept a `LoggingLevelSwitch` that can be remote-controlled. In those case, the same `LoggingLevelSwitch` instance that is used to control the global minimum level must be used.
The *reference* to the switch is noted with the symbol `$`.


- in **C#** (ex : `222-LoggingLevelSwitch.csx`)

```csharp
#r ".\TestDummies.dll"
using Serilog.Core;
using Serilog.Events;
using TestDummies;

var mySwitch = new LoggingLevelSwitch(LogEventLevel.Warning);

LoggerConfiguration
    .MinimumLevel.ControlledBy(mySwitch)
    .WriteTo.DummyWithLevelSwitch(controlLevelSwitch: mySwitch);
```


- in **JSON** (ex : `222-LoggingLevelSwitch.json`)

```json
{
  "Serilog": {
    "Using": [ "TestDummies" ],
    "LevelSwitches": { "$mySwitch": "Warning" },
    "MinimumLevel": {
      "ControlledBy": "$mySwitch"
    },
    "WriteTo": [
      {
        "Name": "DummyWithLevelSwitch",
        "Args": {
          "controlLevelSwitch": "$mySwitch"
        }
      }
    ]
  }
}
```


- in **XML** (ex : `222-LoggingLevelSwitch.config`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:TestDummies" value="TestDummies" />
    <add key="serilog:level-switch:$mySwitch" value="Warning" />
    <add key="serilog:minimum-level:controlled-by" value="$mySwitch" />
    <add key="serilog:write-to:DummyWithLevelSwitch.controlLevelSwitch" value="$mySwitch" />
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


### Custom Destructuring
Specific *Destructuring* rules can be specified.


- in **C#** (ex : `235-Destructure.csx`)

```csharp
#r ".\TestDummies.dll"
using TestDummies;
using TestDummies.Policies;

LoggerConfiguration
    .Destructure.ToMaximumDepth(maximumDestructuringDepth: 3)
    .Destructure.ToMaximumStringLength(maximumStringLength: 3)
    .Destructure.ToMaximumCollectionCount(maximumCollectionCount: 3)
    .Destructure.AsScalar(typeof(System.Version))
    .Destructure.With(new CustomPolicy());
```


- in **JSON** (ex : `235-Destructure.json`)

```json
{
  "Serilog": {
    "Using": [ "TestDummies" ],
    "Destructure": [
      {
        "Name": "ToMaximumDepth",
        "Args": { "maximumDestructuringDepth": 3 }
      },
      {
        "Name": "ToMaximumStringLength",
        "Args": { "maximumStringLength": 3 }
      },
      {
        "Name": "ToMaximumCollectionCount",
        "Args": { "maximumCollectionCount": 3 }
      },
      {
        "Name": "AsScalar",
        "Args": { "scalarType": "System.Version" }
      },
      {
        "Name": "With",
        "Args": { "policy": "TestDummies.Policies.CustomPolicy, TestDummies" }
      }
    ]
  }
}
```


- in **XML** (ex : `235-Destructure.config`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:TestDummies" value="TestDummies" />
    <add key="serilog:destructure:ToMaximumDepth.maximumDestructuringDepth" value="3" />
    <add key="serilog:destructure:ToMaximumStringLength.maximumStringLength" value="3" />
    <add key="serilog:destructure:ToMaximumCollectionCount.maximumCollectionCount" value="3" />
    <add key="serilog:destructure:AsScalar.scalarType" value="System.Version" />
    <add key="serilog:destructure:With.policy" value="TestDummies.Policies.CustomPolicy, TestDummies" />
  </appSettings>
</configuration>
```


### Filtering - Expressions
Filtering can be specified using *filter expressions* thanks to the package *Serilog.Filters.Expressions*.


- in **C#** (ex : `240-FilterExpressions.csx`)

```csharp
#r ".\Serilog.Filters.Expressions.dll"

LoggerConfiguration
    .Filter.ByExcluding("filter = 'exclude'");
```


- in **JSON** (ex : `240-FilterExpressions.json`)

```json
{
  "Serilog": {
    "Using": [ "Serilog.Filters.Expressions" ],
    "Filter": [
      {
        "Name": "ByExcluding",
        "Args": {
          "expression": "filter = 'exclude'"
        }
      }
    ]
  }
}
```


- in **XML** (ex : `240-FilterExpressions.config`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:Serilog.Filters.Expressions" value="Serilog.Filters.Expressions" />
    <add key="serilog:filter:ByExcluding.expression" value="filter = 'exclude'" />
  </appSettings>
</configuration>
```


### Sub-loggers / child loggers
When conditional configuration is needed depending on the sinks, sub-loggers can be used. [More about sub-loggers](https://nblumhardt.com/2016/07/serilog-2-write-to-logger/)


- in **C#** (ex : `250-SubLoggers.csx`)

```csharp
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
```


- in **JSON** (ex : `250-SubLoggers.json`)

```json
{
  "Serilog": {
    "Using": [ "TestDummies" ],
    "WriteTo:SubLogger1": {
      "Name": "Logger",
      "Args": {
        "configureLogger": {
          "Properties": {
            "Prop1": "PropValue1"
          },
          "WriteTo": [ "DummyConsole" ]
        }
      }
    },
    "WriteTo:SubLogger2": {
      "Name": "Logger",
      "Args": {
        "configureLogger": {
          "Properties": {
            "Prop2": "PropValue2"
          },
          "WriteTo": [ "Dummy" ]
        }
      }
    }
  }
}
```


:warning: Not supported yet in the appSettings XML format. [![GitHub issue state](https://img.shields.io/github/issues/detail/s/serilog/serilog/1072.svg)](https://github.com/serilog/serilog/issues/1072)

# Advanced settings formats
Below are the general rules for setting values.


## Method Discovery
Settings providers will discover extension methods for configuration. Remember to add  `using` directives if those extension methods leave in an assembly other than *Serilog*.

Extension methods to the following types are supported :

| Type | C# API | xml prefix | json section
| ---- | ------ | ---------- | ------------
| `LoggerSinkConfiguration` | `config.WriteTo.*` | `serilog:write-to:` | `WriteTo`
| `LoggerAuditSinkConfiguration` | `config.AuditTo.*` | `serilog:audit-to:` | `AuditTo`
| `LoggerEnrichmentConfiguration` | `config.Enrich.*` | `serilog:enrich:` | `Enrich`
| `LoggerFilterConfiguration` | `config.Filter.*` | `serilog:filter:` | `Filter`




- in **C#** (ex : `310-MethodDiscovery.csx`)

```csharp
#r ".\TestDummies.dll"
using Serilog.Events;
using TestDummies;

LoggerConfiguration
    .Filter.ByExcludingLevel(LogEventLevel.Warning)
    .Enrich.WithDummyUserName("UserExtraParam")
    .AuditTo.Dummy(stringParam: "A string param", intParam: 666)
    .WriteTo.Dummy()
    ;
```


- in **JSON** (ex : `310-MethodDiscovery.json`)

```json
{
  "Serilog": {
    "Using": [ "TestDummies" ],
    "Filter": [
      {
        "Name": "ByExcludingLevel",
        "Args": {
          "excludedLevel": "Warning"
        }
      }
    ],
    "Enrich": [
      {
        "Name": "WithDummyUserName",
        "Args": {
          "extraParam": "UserExtraParam"
        }
      }
    ],
    "AuditTo": [
      {
        "Name": "Dummy",
        "Args": {
          "stringParam": "A string param",
          "intParam": 666
        }
      }
    ],
    "WriteTo": [ "Dummy" ]
  }
}
```


- in **XML** (ex : `310-MethodDiscovery.config`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:TestDummies" value="TestDummies" />
    <add key="serilog:filter:ByExcludingLevel.excludedLevel" value="Warning" />
    <add key="serilog:enrich:WithDummyUserName.extraParam" value="UserExtraParam" />
    <add key="serilog:audit-to:Dummy.stringParam" value="A string param" />
    <add key="serilog:audit-to:Dummy.intParam" value="666" />
    <add key="serilog:write-to:Dummy" value="" />
  </appSettings>
</configuration>
```


## Setting values conversions
Values for settings can be simple value types (`string`, `int`, `bool` etc), nullable versions of the previous. `Enum`s can also be parsed by name. Some specific types like `Uri` and `TimeSpan` are also supported.


- in **C#** (ex : `320-SettingsValueConversions.csx`)

```csharp
#r ".\TestDummies.dll"
using System;
using TestDummies;

LoggerConfiguration
    .WriteTo.DummyWithManyParams(
        enumParam: MyEnum.Qux,
        timespanParam: new TimeSpan(2, 3, 4, 5),
        uriParam: new Uri("https://www.serilog.net"));
```


- in **JSON** (ex : `320-SettingsValueConversions.json`)

```json
{
  "Serilog": {
    "Using": [ "TestDummies" ],
    "WriteTo": [
      {
        "Name": "DummyWithManyParams",
        "Args": {
          "enumParam": "Qux",
          "timespanParam": "2.03:04:05",
          "uriParam": "https://www.serilog.net"
        }
      }
    ]
  }
}
```


- in **XML** (ex : `320-SettingsValueConversions.config`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:TestDummies" value="TestDummies" />
    <add key="serilog:write-to:DummyWithManyParams.enumParam" value="Qux" />
    <add key="serilog:write-to:DummyWithManyParams.timespanParam" value="2.03:04:05" />
    <add key="serilog:write-to:DummyWithManyParams.uriParam" value="https://www.serilog.net" />
  </appSettings>
</configuration>
```


## Interfaces and abstract classes


### Full type name of implementation with default constructor
For parameters whose type is an `interface` or an `abstract class`, the full type name of an implementation can be provided. If the type is not in the `Serilog` assembly, remember to include `using` directives.


- in **C#** (ex : `331-ImplementationDefaultConstructor.csx`)

```csharp
#r ".\TestDummies.dll"
using System;
using Serilog.Formatting.Json;
using TestDummies;
using TestDummies.Console;

LoggerConfiguration
    .WriteTo.DummyWithFormatter(formatter: new JsonFormatter())
    .WriteTo.DummyConsole(theme: new CustomConsoleTheme());
```


:warning: Inconstency : JSON provider seems to need fully-qualified type name for Serilog types, but appSettings does not ! [![GitHub issue state](https://img.shields.io/github/issues/detail/s/serilog/serilog-settings-configuration/81.svg)](https://github.com/serilog/serilog-settings-configuration/issues/81)

- in **XML** (ex : `331-ImplementationDefaultConstructor.config`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:TestDummies" value="TestDummies" />
    <add key="serilog:write-to:DummyWithFormatter.formatter" value="Serilog.Formatting.Json.JsonFormatter" />
    <add key="serilog:write-to:DummyConsole.theme" value="TestDummies.Console.CustomConsoleTheme, TestDummies" />
  </appSettings>
</configuration>
```


### Public static properties
For parameters whose type is an `interface` or an `abstract class`, you can reference a static property that exposes an instance of that interface. Use the full containing type name followed by `::` and the `public static` property name.


- in **C#** (ex : `332-ImplementationViaStaticProperty.csx`)

```csharp
#r ".\TestDummies.dll"
#r ".\Serilog.Settings.Comparison.Tests.dll"
using System;
using Serilog.Formatting.Json;
using TestDummies;
using TestDummies.Console;
using TestDummies.Console.Themes;
using Serilog.SettingsComparisonTests.Support.Formatting;

LoggerConfiguration
    .WriteTo.DummyWithFormatter(formatter: CustomFormatters.Formatter)
    .WriteTo.DummyConsole(theme: ConsoleThemes.Theme1);
```


- in **JSON** (ex : `332-ImplementationViaStaticProperty.json`)

```json
{
  "Serilog": {
    "Using": [ "TestDummies" ],
    "WriteTo": [
      {
        "Name": "DummyWithFormatter",
        "Args": {
          "formatter": "Serilog.SettingsComparisonTests.Support.Formatting.CustomFormatters::Formatter, Serilog.Settings.Comparison.Tests"
        }
      },
      {
        "Name": "DummyConsole",
        "Args": {
          "theme": "TestDummies.Console.Themes.ConsoleThemes::Theme1, TestDummies"
        }
      }
    ]
  }
}
```


- in **XML** (ex : `332-ImplementationViaStaticProperty.config`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:using:TestDummies" value="TestDummies" />
    <add key="serilog:write-to:DummyWithFormatter.formatter" value="Serilog.SettingsComparisonTests.Support.Formatting.CustomFormatters::Formatter, Serilog.Settings.Comparison.Tests" />
    <add key="serilog:write-to:DummyConsole.theme" value="TestDummies.Console.Themes.ConsoleThemes::Theme1, TestDummies" />
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


