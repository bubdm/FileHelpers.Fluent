using FileHelpers.Fluent.Core.Descriptors;
using FileHelpers.Fluent.Core.Exceptions;
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

        public abstract ExpandoObject[] ReadBuffer(byte[] buffer);

        public abstract Task<ExpandoObject[]> ReadBufferAsync(byte[] buffer);

        public abstract ExpandoObject[] ReadStream(StreamReader reader);

        public abstract Task<ExpandoObject[]> ReadStreamAsync(StreamReader reader);

        public abstract ExpandoObject[] ReadString(string source);

        protected abstract Task<ExpandoObject> ReadLineAsync(string currentLine, IRecordDescriptor descriptor);

        public abstract void WriteStream(TextWriter writer, IEnumerable<ExpandoObject> records, bool flush = true);

        public abstract Task WriteStreamAsync(TextWriter writer, IEnumerable<ExpandoObject> records, bool flush = true);

        public abstract string WriteString(IEnumerable<ExpandoObject> records);
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
