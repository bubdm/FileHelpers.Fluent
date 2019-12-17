using System.Globalization;

namespace FileHelpers.Fluent.Core.Converters
{
    public class ShortConverter : ConverterBase
    {
        public override object StringToField(string @from)
        {
            short.TryParse(from, NumberStyles.Any, CultureInfo.InvariantCulture, out short to);
            return (to);
        }
    }
}
