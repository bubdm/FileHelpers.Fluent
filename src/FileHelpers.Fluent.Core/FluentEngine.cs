using FileHelpers.Fluent.Core.Descriptors;
using FileHelpers.Fluent.Core.Exceptions;
using FileHelpers.Fluent.Core.IO;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHelpers.Fluent.Core
{
    public abstract class FluentEngine : IFluentEngine
    {
        protected FluentEngine(IRecordDescriptor descriptor, Encoding encoding = null)
        {
            CheckDescriptor(descriptor);
            if (encoding == null)
                encoding = Encoding.UTF8;

            Encoding = encoding;
            Descriptor = descriptor;
        }

        public IRecordDescriptor Descriptor { get; }

        public Encoding Encoding { get; }


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
