using System.Globalization;

namespace FileHelpers.Fluent.Core.Converters
{
    public class UIntegerConverter : ConverterBase
    {
        public override object StringToField(string @from)
        {
            uint.TryParse(from, NumberStyles.Any, CultureInfo.InvariantCulture, out uint to);
            return (to);
        }
    }
}
