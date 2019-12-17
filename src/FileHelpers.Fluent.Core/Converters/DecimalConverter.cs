using System;
using System.Globalization;

using FileHelpers.Fluent.Core.Exceptions;

namespace FileHelpers.Fluent.Core.Converters
{
    public class DecimalConverter : DecimalNumberBaseConverter
    {
        public DecimalConverter() : base($"N{CultureInfo.InvariantCulture.NumberFormat.NumberDecimalDigits}")
        {

        }

        public DecimalConverter(string format) :
            base(format)
        {

        }

        public override object StringToField(string @from)
        {
            if (string.IsNullOrWhiteSpace(from))
                return new decimal(0, 0, 0, false, (byte)1);

            decimal result = new decimal(0, 0, 0, false, (byte)1);
            decimal.TryParse(from.Trim(), NumberStyles.Number | NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out result);
            if (!string.IsNullOrWhiteSpace(DecimalSeparator))
                return (object)result;
            if (!decimal.TryParse(from.Trim(), NumberStyles.Number | NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out decimal _))
                throw new ConvertException(from, typeof(decimal));
            return (object)(result / (decimal)Math.Pow(10.0, (double)DecimalDigits));
        }
    }
}
