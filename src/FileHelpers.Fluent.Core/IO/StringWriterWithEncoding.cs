using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("FileHelpers.Fluent.CodeDom")]

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
