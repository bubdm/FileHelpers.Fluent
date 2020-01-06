using FileHelpers.Fluent.Core.Descriptors;
using FileHelpers.Fluent.Core.Events;
using FileHelpers.Fluent.Core.Exceptions;
using FileHelpers.Fluent.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileHelpers.Fluent.Fixed
{
    public sealed class FluentFixedEngine : FluentEventEngineBase
    {
        public FluentFixedEngine(IRecordDescriptor descriptor, Encoding encoding = null) : base(descriptor, encoding)
        {
        }

        public FluentFixedEngine(IList<RecordItem> recordItems, Encoding encoding = null) : base(recordItems, encoding)
        {
        }

        #region Private Methods
        private void CheckFieldArrayDescriptor(string fieldName, IArrayFieldInfoDescriptor recordInfo)
        {
            if (recordInfo.ArrayLength <= 0)
                throw new BadFluentConfigurationException($"The property {fieldName} must be the {nameof(recordInfo.ArrayLength)} length greater than 0");

            if (recordInfo.ArrayItemLength <= 0)
                throw new BadFluentConfigurationException($"The property {fieldName} must be the {nameof(recordInfo.ArrayItemLength)} length greater than 0");

            if (recordInfo.ArrayItemLength > recordInfo.ArrayLength)
                throw new BadFluentConfigurationException($"The {nameof(recordInfo.ArrayLength)} of property {fieldName} must be greater than {nameof(recordInfo.ArrayItemLength)}");

            if ((recordInfo.ArrayLength % recordInfo.ArrayItemLength) != 0)
                throw new BadFluentConfigurationException($"The remainder of {nameof(recordInfo.ArrayLength)} division by {nameof(recordInfo.ArrayItemLength)} can not be different than 0");

            var arrayRecordInfo = recordInfo as IRecordDescriptor;

            if (arrayRecordInfo == null)
                throw new BadFluentConfigurationException($"The property {fieldName} is not an array builder");

            CheckDescriptor(arrayRecordInfo, true);
        }

        private void CheckFieldDescriptor(string fieldName, IFixedFieldInfoDescriptor fieldDescriptor)
        {
            if (fieldDescriptor == null)
                throw new ArgumentNullException(nameof(fieldDescriptor));
            if (fieldDescriptor.Length <= 0)
                throw new BadFluentConfigurationException($"The property {fieldName} must be a length gearter than 0");
        }

        #endregion

        #region Protected Override Methods

        protected override void CheckFieldDescriptor(string fieldName, IFieldInfoTypeDescriptor fieldDescriptor)
        {
            if (fieldDescriptor.IsArray)
            {
                CheckFieldArrayDescriptor(fieldName, fieldDescriptor as IArrayFieldInfoDescriptor);
                return;
            }
            CheckFieldDescriptor(fieldName, fieldDescriptor as IFixedFieldInfoDescriptor);
        }

        protected override async Task<ExpandoObject> ReadLineAsync(string currentLine, IRecordDescriptor descriptor) =>
            await Task.Run(() =>
            {
                var item = new ExpandoObject();

                var offset = 0;
                foreach (KeyValuePair<string, IFieldInfoTypeDescriptor> fieldInfoTypeDescriptor in descriptor.Fields)
                {
                    if (fieldInfoTypeDescriptor.Value.IsArray)
                    {
                        item.InternalTryAdd(fieldInfoTypeDescriptor.Key,
                            ((IArrayFieldInfoDescriptor)fieldInfoTypeDescriptor.Value).StringToArray(currentLine,
                                ref offset));
                        continue;
                    }

                    item.InternalTryAdd(fieldInfoTypeDescriptor.Key,
                        ((IFixedFieldInfoDescriptor)fieldInfoTypeDescriptor.Value).StringToRecord(currentLine, ref offset));
                }

                return item;
            });

        #endregion

        public override async Task WriteStreamAsync(TextWriter writer, IEnumerable<ExpandoObject> records, bool flush = true)
        {
            writer.NewLine = Environment.NewLine;
            var lineNumber = 1;
            foreach (ExpandoObject expandoObject in records)
            {
                var beforeWriteArgs = OnBeforeWriteRecord(expandoObject, lineNumber);

                var record = (beforeWriteArgs.LineChanged ? beforeWriteArgs.Record : expandoObject) as IDictionary<string, object>;

                if (record == null || beforeWriteArgs.SkipRecord)
                {
                    lineNumber++;
                    continue;
                }

                var recordItem = GetRecordDescriptor(record, lineNumber);

                var sb = new StringBuilder();
                foreach (KeyValuePair<string, object> keyValuePair in record)
                {
                    if (!recordItem.Descriptor.Fields.TryGetValue(keyValuePair.Key, out IFieldInfoTypeDescriptor fieldDescriptor))
                        throw new Exception($"The field {keyValuePair.Key} is not configured");

                    if (fieldDescriptor.IsArray)
                    {
                        sb.Append(((IArrayFieldInfoDescriptor)fieldDescriptor).ArrayToString(
                            (IEnumerable<dynamic>)keyValuePair.Value));
                        continue;
                    }

                    sb.Append(((IFixedFieldInfoDescriptor)fieldDescriptor).RecordToString(keyValuePair.Value));
                }

                var afterWriteArgs = OnAfterWriteRecord(sb.ToString(), lineNumber, expandoObject);

                await writer.WriteLineAsync(afterWriteArgs.Line);

                if (flush)
                    await writer.FlushAsync();
                lineNumber++;
            }
        }
    }
}
