using FileHelpers.Fluent.Core.Descriptors;
using FileHelpers.Fluent.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHelpers.Fluent.Core.Events
{
    public abstract class FluentEventEngineBase : FluentEngine
    {
        protected FluentEventEngineBase(IRecordDescriptor descriptor, Encoding encoding = null) : base(descriptor, encoding)
        {
        }

        protected FluentEventEngineBase(IList<RecordItem> recordItems, Encoding encoding = null) : base(recordItems, encoding) { }

        public event FluentEventHandler BeforeReadRecord;

        public event FluentEventHandler AfterReadRecord;

        public event FluentEventHandler BeforeWriteRecord;

        public event FluentEventHandler AfterWriteRecord;

        protected FluentEventArgs OnBeforeReadRecord(string line, int lineNumber)
        {
            var args = new FluentEventArgs { SkipRecord = false, LineChanged = false, LineNumber = lineNumber, Line = line };

            BeforeReadRecord?.Invoke(this, args);

            args.LineChanged = line != args.Line;

            return args;
        }

        protected FluentEventArgs OnAfterReadRecord(string line, int lineNumber, ExpandoObject record)
        {
            var args = new FluentEventArgs { Record = record, SkipRecord = false, LineChanged = false, LineNumber = lineNumber };

            AfterReadRecord?.Invoke(this, args);

            args.LineChanged = line != args.Line;

            return args;
        }

        protected FluentEventArgs OnBeforeWriteRecord(ExpandoObject record, int lineNumber)
        {
            var args = new FluentEventArgs {Record = record, SkipRecord = false, LineChanged = false, LineNumber = lineNumber };

            BeforeWriteRecord?.Invoke(this, args);

            return args;
        }

        protected FluentEventArgs OnAfterWriteRecord(string line, int lineNumber, ExpandoObject record)
        {
            var args = new FluentEventArgs { Record = record, SkipRecord = false, LineChanged = false, LineNumber = lineNumber, Line = line };

            AfterWriteRecord?.Invoke(this, args);

            return args;
        }

        public override async Task<ExpandoObject[]> ReadStreamAsync(StreamReader reader)
        {
            IList<ExpandoObject> items = new List<ExpandoObject>();

            string currentLine = await reader.ReadLineAsync();
            int currentLineNumber = 1;
            while (currentLine != null)
            {
                if (!string.IsNullOrWhiteSpace(currentLine))
                {
                    var beforeReadArgs = OnBeforeReadRecord(currentLine, currentLineNumber);
                    if (!beforeReadArgs.SkipRecord)
                    {
                        if (beforeReadArgs.LineChanged)
                            currentLine = beforeReadArgs.Line;
                        var recordItem = GetRecordDescriptor(currentLine, currentLineNumber);

                        ExpandoObject item = await ReadLineAsync(currentLine, recordItem.Descriptor);

                        var afterReadArgs = OnAfterReadRecord(currentLine, currentLineNumber, item);

                        items.Add(afterReadArgs.Record);
                    }
                }
                currentLineNumber++;
                currentLine = await reader.ReadLineAsync();
            }

            return items.ToArray();
        }
    }
}
