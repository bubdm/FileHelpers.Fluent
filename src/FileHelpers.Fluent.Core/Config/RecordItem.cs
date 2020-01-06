using FileHelpers.Fluent.Core.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileHelpers.Fluent
{
    public class RecordItem
    {
        public string Name { get; set; }

        public string RegexPattern { get; set; }

        public string RecordTypeProperty { get; set; }

        public IRecordDescriptor Descriptor { get; set; }
    }
}
