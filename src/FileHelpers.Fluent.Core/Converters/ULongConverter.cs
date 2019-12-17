using System.Globalization;

namespace FileHelpers.Fluent.Core.Converters
{
    public class ULongConverter : ConverterBase
    {
        public override object StringToField(string @from)
        {
            ulong.TryParse(from, NumberStyles.Any, CultureInfo.InvariantCulture, out ulong to);
            return (to);
        }
    }
}
