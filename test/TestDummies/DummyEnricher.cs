using System;
using Serilog.Core;
using Serilog.Events;

namespace TestDummies
{
    public class DummyEnricher : ILogEventEnricher
    {
        private readonly string _userName;

        public DummyEnricher(string userName)
        {
            _userName = userName;
        }

        public DummyEnricher() : this(null)
        {

        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("ThreadId", Guid.Empty.ToString(), false));

            if (_userName != null)
            {
                logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("UserName", _userName, false));
            }
        }
    }
}
