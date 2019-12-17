using System.Globalization;

namespace FileHelpers.Fluent.Core.Converters
{
    public class UShortConverter : ConverterBase
    {
        public override object StringToField(string @from)
        {
            ushort.TryParse(from, NumberStyles.Any, CultureInfo.InvariantCulture, out ushort to);
            return (to);
        }
    }
}
