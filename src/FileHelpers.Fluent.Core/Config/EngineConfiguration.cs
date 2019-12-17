using System;
using System.Collections.Generic;
using System.Text;

namespace FileHelpers.Fluent.Core
{
    public class EngineConfiguration
    {
        public EngineConfiguration(bool throwOnError = true, Encoding encoding = null, char nullChar = '\u0000')
        {
            ThrowOnError = throwOnError;
            Encoding = encoding ?? Encoding.UTF8;
            NullChar = nullChar;
        }

        public bool ThrowOnError { get; set; }

        public Encoding Encoding { get; set; }

        public char NullChar { get; set; }
    }
}
