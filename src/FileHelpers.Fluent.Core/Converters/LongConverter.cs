using System.Globalization;

using FileHelpers.Fluent.Core.Exceptions;

namespace FileHelpers.Fluent.Core.Converters
{
    public class LongConverter : ConverterBase
    {
        public override object StringToField(string from)
        {
            long.TryParse(from, NumberStyles.Any, CultureInfo.InvariantCulture, out long to);
               
            return (to);
        }
    }
}
