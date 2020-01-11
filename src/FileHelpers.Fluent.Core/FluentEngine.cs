using FileHelpers.Fluent.Core.Descriptors;
using FileHelpers.Fluent.Core.Exceptions;
using FileHelpers.Fluent.Core.IO;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileHelpers.Fluent.Core
{
    public abstract class FluentEngine : IFluentEngine
    {
        private const string DefaultRecordItemName = "Default";

        private FluentEngine(Encoding encoding)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;

            Encoding = encoding;
        }

        protected FluentEngine(IRecordDescriptor descriptor, Encoding encoding = null) : this(encoding)
        {
            CheckDescriptor(descriptor);
            RecordItems = new List<RecordItem>
                {
                    new RecordItem
                    {
                        Name = DefaultRecordItemName,
                        Descriptor = descriptor,
                        RegexPattern = string.Empty
                    }
                };
        }

        protected FluentEngine(IList<RecordItem> recordItems, Encoding encoding = null) : this(encoding)
        {
            if (recordItems == null || recordItems.Count == 0)
                throw new BadFluentConfigurationException("The list of type RecordItem must contains at least 1 item.");

            RecordItems = recordItems;

            foreach (var recordItem in recordItems)
                CheckDescriptor(recordItem.Descriptor);
        }

        protected IList<RecordItem> RecordItems { get; }

        public Encoding Encoding { get; }

        public IRecordDescriptor Descriptor => RecordItems[0].Descriptor;


        #region IFluentEngine Implementation

        public virtual ExpandoObject[] ReadBuffer(byte[] buffer) =>
            ReadBufferAsync(buffer).GetAwaiter().GetResult();

        public virtual async Task<ExpandoObject[]> ReadBufferAsync(byte[] buffer)
        {
            using (var stream = new MemoryStream(buffer))
                using (var streamReader = new StreamReader(stream, Encoding))
                    return await ReadStreamAsync(streamReader);
        }

        public virtual ExpandoObject[] ReadStream(StreamReader reader) =>
            ReadStreamAsync(reader).GetAwaiter().GetResult();

        public abstract Task<ExpandoObject[]> ReadStreamAsync(StreamReader reader);

        public virtual ExpandoObject[] ReadString(string source)
        {
            if (source == null)
                source = string.Empty;

            using (var stream = new MemoryStream(Encoding.GetBytes(source)))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return ReadStream(streamReader);
                }
            }
        }

        protected virtual Task<ExpandoObject> ReadLineAsync(string currentLine, IRecordDescriptor descriptor)
        {
            return null;
        }

        public virtual void WriteStream(TextWriter writer, IEnumerable<ExpandoObject> records, bool flush = true) =>
            WriteStreamAsync(writer, records, flush).GetAwaiter().GetResult();

        public abstract Task WriteStreamAsync(TextWriter writer, IEnumerable<ExpandoObject> records, bool flush = true);

        public virtual string WriteString(IEnumerable<ExpandoObject> records)
        {
            var sb = new StringBuilder();
            using (var writer = new StringWriterWithEncoding(sb, Encoding))
            {
                WriteStream(writer, records);
                return sb.ToString();
            }
        }
        #endregion

        #region Protected Virtual Methods

        protected virtual void CheckFieldDescriptor(string fieldName, IFieldInfoTypeDescriptor fieldDescriptor)
        {

        }

        #endregion

        #region Protected Methods

        protected RecordItem GetRecordDescriptor(string line, int lineNumber)
        {
            if (RecordItems.Count == 1)
                return RecordItems[0];

            foreach (var recordItem in RecordItems)
            {
                if (Regex.IsMatch(line, recordItem.RegexPattern))
                    return recordItem;
            }

            throw new BadFluentConfigurationException($"There is no descriptor configured for line number {lineNumber}.");
        }

        protected RecordItem GetRecordDescriptor(IDictionary<string, object> record, int lineNumber)
        {
            if (RecordItems.Count == 1)
                return RecordItems[0];

            foreach (var recordItem in RecordItems)
            {
                if (record.ContainsKey(recordItem.RecordTypeProperty) 
                    && record[recordItem.RecordTypeProperty] != null
                    && Regex.IsMatch(record[recordItem.RecordTypeProperty].ToString(), recordItem.RegexPattern))
                    return recordItem;
            }

            throw new BadFluentConfigurationException($"There is no descriptor configured for the record on index {lineNumber--}");
        }


        #endregion

        #region Private Methods

        protected void CheckDescriptor(IRecordDescriptor descriptor, bool isArray = false)
        {
            if (!descriptor.Fields.Any())
                throw new BadFluentConfigurationException(isArray ? "The array property has no fields" : "The builder has no fields");

            foreach (KeyValuePair<string, IFieldInfoTypeDescriptor> fieldInfoDescriptor in descriptor.Fields)
            {
                if (string.IsNullOrWhiteSpace(fieldInfoDescriptor.Key))
                    throw new BadFluentConfigurationException();

                CheckFieldDescriptor(fieldInfoDescriptor.Key, fieldInfoDescriptor.Value);
            }
        }

        #endregion
    }
}
