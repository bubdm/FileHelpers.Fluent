using System.IO;
using System.Text;

namespace FileHelpers.Fluent.Core.IO
{
    internal class StringWriterWithEncoding : StringWriter
    {
        private readonly Encoding encoding;

        internal StringWriterWithEncoding(StringBuilder sb, Encoding encoding)
            : base(sb)
        {
            this.encoding = encoding;
        }

        public override Encoding Encoding => encoding;
    }
}
