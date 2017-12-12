<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="serilog:minimum-level" value="Verbose" />
    <add key="serilog:minimum-level:override:Microsoft" value="Error" />
    <add key="serilog:minimum-level:override:Microsoft.Extensions" value="Information" />
    <add key="serilog:minimum-level:override:System" value="Debug" />
  </appSettings>
</configuration>