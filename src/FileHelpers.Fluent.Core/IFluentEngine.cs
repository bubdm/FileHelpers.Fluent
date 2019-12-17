using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;

namespace FileHelpers.Fluent.Core
{
    public interface IFluentEngine
    {
        string WriteString(IEnumerable<ExpandoObject> records);

        void WriteStream(TextWriter writer, IEnumerable<ExpandoObject> records, bool flush = true);

        Task WriteStreamAsync(TextWriter writer, IEnumerable<ExpandoObject> records, bool flush = true);

        ExpandoObject[] ReadString(string source);

        ExpandoObject[] ReadStream(StreamReader reader);

        Task<ExpandoObject[]> ReadStreamAsync(StreamReader reader);

        ExpandoObject[] ReadBuffer(byte[] buffer);

        Task<ExpandoObject[]> ReadBufferAsync(byte[] buffer);
    }
}
