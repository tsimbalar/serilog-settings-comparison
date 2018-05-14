using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace TestDummies.Policies
{
    public class LoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class CustomPolicy : IDestructuringPolicy
    {
        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            result = null;

            if (value is LoginData)
            {
                result = new StructureValue(
                    new List<LogEventProperty>
                    {
                        new LogEventProperty("Username", new ScalarValue(((LoginData)value).Username))
                    });
            }

            return (result != null);
        }
    }
}
