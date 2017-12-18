# Serilog Settings documentation
Serilog is typically configured via code though its *configuration APIs*, but it is quite common to specify the settings also from some sort of configuration file. 

For that purpose, several *Settings providers* exist, that mimic the *code-based* API and allow supplying values from external sources : 
- *Serilog.Settings.AppSettings* allows to read configuration from the `<appSettings>` section of an `App.config` or `Web.config` file. It is used
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

