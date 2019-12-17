using FileHelpers.Fluent.Core.Descriptors;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace FileHelpers.Fluent.Core.Events
{
    public abstract class FluentEventEngineBase : FluentEngine
    {
        protected FluentEventEngineBase(IRecordDescriptor descriptor, Encoding encoding = null) : base(descriptor, encoding)
        {
        }

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
    }
}
