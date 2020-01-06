using System;
using System.Globalization;

using FileHelpers.Fluent.Core.Exceptions;

namespace FileHelpers.Fluent.Core.Converters
{
    public class DoubleConverter : DecimalNumberBaseConverter
    {

        public DoubleConverter() : base($"N{CultureInfo.InvariantCulture.NumberFormat.NumberDecimalDigits}")
        {
            
        }

        public DoubleConverter(string format) :
            base(format)
        {
            
        }

        public override string FieldType => "double";

        public override object StringToField(string @from)
        {
            if (string.IsNullOrWhiteSpace(from))
                return 0.0;

            double to = 0.0;

            double.TryParse(from.Trim(),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    CultureInfo.InvariantCulture,
                    out to);

            if (!string.IsNullOrWhiteSpace(DecimalSeparator))
                return to;

            if (
                !double.TryParse(from.Trim(),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    CultureInfo.InvariantCulture,
                    out double res))
                throw new ConvertException(from, typeof(double));
            return to / Math.Pow(10, DecimalDigits);
        }
    }
}
