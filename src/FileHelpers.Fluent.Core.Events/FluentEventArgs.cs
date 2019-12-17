using System;
using System.Dynamic;

namespace FileHelpers.Fluent.Core.Events
{
    public class FluentEventArgs : EventArgs
    {
        public ExpandoObject Record { get; set; }

        public bool SkipRecord { get; set; }

        public bool LineChanged { get; set; }

        public int LineNumber { get; set; }

        public string Line { get; set; }
    }
}
