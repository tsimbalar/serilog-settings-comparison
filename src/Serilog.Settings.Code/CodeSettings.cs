using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Serilog.Configuration;

namespace Serilog.Settings.Code
{
    public class CodeSettings : ILoggerSettings
    {
        readonly string _cSharpCode;
        readonly List<Assembly> _referencedAssemblies;

        public CodeSettings(string cSharpCode, Assembly[] referencedAssemblies = null)
        {
            _cSharpCode = cSharpCode ?? throw new ArgumentNullException(nameof(cSharpCode));
            _referencedAssemblies = referencedAssemblies?.ToList() ?? new List<Assembly>();
        }

        public void Configure(LoggerConfiguration loggerConfiguration)
        {
            var scriptOptions = ScriptOptions.Default
                .WithReferences(typeof(ILogger).Assembly)
                .WithImports(typeof(ILogger).Namespace)
                .AddReferences(_referencedAssemblies);

            var runTask = CSharpScript.RunAsync(_cSharpCode,
                globals: new Globals()
                {
                    LoggerConfiguration = loggerConfiguration
                },
                options: scriptOptions);
            runTask.Wait();
        }

        public class Globals
        {
            public LoggerConfiguration LoggerConfiguration;
        }
    }
}
