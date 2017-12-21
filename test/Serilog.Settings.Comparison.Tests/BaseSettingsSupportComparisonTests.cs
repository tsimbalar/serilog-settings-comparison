using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Serilog.Settings.Code;
using Xunit.Abstractions;

namespace Serilog.SettingsComparisonTests
{
    public abstract class BaseSettingsSupportComparisonTests
    {
        readonly ITestOutputHelper _outputHelper;

        protected BaseSettingsSupportComparisonTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        protected void WriteDocumentation(string fileName, bool includeInOutput = true)
        {
            if (!includeInOutput)
            {
                _outputHelper.WriteLine("<!--");
            }

            var fullFilePath = GetTestFileFullPath(fileName);

            _outputHelper.WriteLine($"- in **{GetLanguageDisplayName(fileName)}** (ex : `{fileName}`)");
            _outputHelper.WriteLine("");
            _outputHelper.WriteLine("```" + GetMarkdownSnippetLanguage(fileName));
            _outputHelper.WriteLine(File.ReadAllText(fullFilePath));
            _outputHelper.WriteLine("```");
            _outputHelper.WriteLine("");

            if (!includeInOutput)
            {
                _outputHelper.WriteLine("-->");
            }

        }

        static string GetMarkdownSnippetLanguage(string fileName)
        {
            var mdOuputFormat = fileName.Split('.').Last();
            if (mdOuputFormat == "csx") mdOuputFormat = "csharp";
            if (mdOuputFormat == "config") mdOuputFormat = "xml";
            return mdOuputFormat;
        }

        static string GetLanguageDisplayName(string fileName)
        {
            var displayName = fileName.Split('.').Last();
            if (displayName == "csx") displayName = "C#";
            if (displayName == "config") displayName = "XML";
            if (displayName == "json") displayName = "JSON";
            return displayName;
        }

        protected static LoggerConfiguration LoadConfig(string fileName)
        {
            var fullFilePath = GetTestFileFullPath(fileName);
            if (fileName.EndsWith(".json")) return LoadJsonConfig(fullFilePath);
            if (fileName.EndsWith(".config")) return LoadXmlConfig(fullFilePath);
            if (fileName.EndsWith(".csx")) return LoadCSharpConfig(fullFilePath);
            throw new ArgumentException($"Only .json and .config are supported. Provided value : {fileName}", nameof(fileName));
        }

        static string GetTestFileFullPath(string fileName)
        {
            return Directory.GetFiles(".", fileName, SearchOption.AllDirectories).SingleOrDefault()
                ?? fileName;
        }

        static LoggerConfiguration LoadXmlConfig(string fileName)
        {
            var xmlConfig = new LoggerConfiguration().ReadFrom.AppSettings(filePath: fileName);
            return xmlConfig;
        }

        static LoggerConfiguration LoadJsonConfig(string fileName)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(fileName, optional: false)
                .Build();

            var jsonConfig = new LoggerConfiguration().ReadFrom.Configuration(config);
            return jsonConfig;
        }

        static LoggerConfiguration LoadCSharpConfig(string fileName)
        {
            var code = File.ReadAllText(fileName);
            // this is a hack, but trying to avoid hard-coding a path in the files ...
            // replace .\ with an actual path
            code = code.Replace("#r \".\\", $"#r \"{Environment.CurrentDirectory}\\");
            var jsonConfig = new LoggerConfiguration().ReadFrom.CodeString(code);
            return jsonConfig;
        }

    }
}
