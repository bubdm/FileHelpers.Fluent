using System.Globalization;

using FileHelpers.Fluent.Core.Exceptions;

namespace FileHelpers.Fluent.Core.Converters
{
    public class IntegerConverter : ConverterBase
    {
        public IntegerConverter() { }

        public override string FieldType => "int";

        public override object StringToField(string from)
        {
            int.TryParse(from, NumberStyles.Any, CultureInfo.InvariantCulture, out int to);
            
            return (to);
        }
    }
}
