using System;

namespace FileHelpers.Fluent.Core.Exceptions
{
    public class BadFluentConfigurationException : Exception
    {
        public BadFluentConfigurationException() :
            base("Generic fluent configuration exception")
        {

        }

        public BadFluentConfigurationException(string message)
            : base(message)
        {

        }

        
    }
}
