using FileHelpers.Fluent.Core.Descriptors;
using FileHelpers.Fluent.Core.Events;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileHelpers.Fluent.Fixed
{
    public class FluentFixedMultiRecordEngine : FluentEventEngineBase
    {
        public FluentFixedMultiRecordEngine(IRecordDescriptor descriptor, Encoding encoding = null) : base(descriptor, encoding)
        {
        }

        public override Task WriteStreamAsync(TextWriter writer, IEnumerable<ExpandoObject> records, bool flush = true)
        {
            throw new NotImplementedException();
        }
    }
}
