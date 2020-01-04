using FileHelpers.Fluent.Core.Descriptors;
using FileHelpers.Fluent.Core.Events;
using FileHelpers.Fluent.Core.Exceptions;
using FileHelpers.Fluent.Core.Extensions;
using FileHelpers.Fluent.Delimited.Descriptors;
using FileHelpers.Fluent.Delimited.Extensions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileHelpers.Fluent.Delimited
{
    public class DelimitedFluentEngine : FluentEventEngineBase
    {
        public DelimitedFluentEngine(
            IRecordDescriptor descriptor,
            Encoding encoding = null) : base(descriptor, encoding)
        {
        }

        #region Protected Override Methods

        protected override void CheckFieldDescriptor(string fieldName, IFieldInfoTypeDescriptor fieldDescriptor)
        {
            if (fieldDescriptor.IsArray)
                throw new BadFluentConfigurationException("Array fields is not suported on Delimited files!");
        }

        protected override async Task<ExpandoObject> ReadLineAsync(string currentLine, IRecordDescriptor descriptor) =>
            await Task.Run(() =>
            {
                var item = new ExpandoObject();
                string[] fieldsValue = currentLine.Split(new string[] { ((DelimitedRecordDescriptor)Descriptor).Delimiter }, StringSplitOptions.RemoveEmptyEntries);
                int index = 0;
                foreach (KeyValuePair<string, IFieldInfoTypeDescriptor> fieldInfoTypeDescriptor in descriptor.Fields)
                {
                    if (fieldInfoTypeDescriptor.Value.IsArray)
                        continue;

                    string fieldValue = index >= fieldsValue.Length ? null : fieldsValue[index];
                    item.InternalTryAdd(fieldInfoTypeDescriptor.Key,
                        ((IFieldInfoDescriptor)fieldInfoTypeDescriptor.Value).StringToRecord(fieldValue, Descriptor.NullChar));

                    index++;
                }

                return item;
            });

        #endregion

        public override async Task WriteStreamAsync(TextWriter writer, IEnumerable<ExpandoObject> records, bool flush = true)
        {
            writer.NewLine = Environment.NewLine;
            var lineNumber = 1;
            string delimiter = ((DelimitedRecordDescriptor)Descriptor).Delimiter;
            foreach (ExpandoObject expandoObject in records)
            {
                var beforeWriteArgs = OnBeforeWriteRecord(expandoObject, lineNumber);

                var record = (beforeWriteArgs.LineChanged ? beforeWriteArgs.Record : expandoObject) as IDictionary<string, object>;

                if (record == null || beforeWriteArgs.SkipRecord)
                {
                    lineNumber++;
                    continue;
                }

                var sb = new StringBuilder();

                foreach (KeyValuePair<string, object> keyValuePair in record)
                {
                    if (!Descriptor.Fields.TryGetValue(keyValuePair.Key, out IFieldInfoTypeDescriptor fieldDescriptor))
                        throw new Exception($"The field {keyValuePair.Key} is not configured");
                    if (fieldDescriptor.IsArray)
                        continue;

                    sb.Append(((IFieldInfoDescriptor)fieldDescriptor).RecordToString(keyValuePair.Value));
                    sb.Append(delimiter);
                }
                sb.Length = sb.Length - 1;

                var afterWriteArgs = OnAfterWriteRecord(sb.ToString(), lineNumber, expandoObject);

                await writer.WriteLineAsync(afterWriteArgs.Line);
                if (flush)
                    await writer.FlushAsync();
                lineNumber++;
            }
        }
    }
}
