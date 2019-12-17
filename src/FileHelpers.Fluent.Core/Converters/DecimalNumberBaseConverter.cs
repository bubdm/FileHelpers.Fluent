using FileHelpers.Fluent.Core.Exceptions;
using System;

namespace FileHelpers.Fluent.Core.Converters
{
    public abstract class DecimalNumberBaseConverter : ConverterBase
    {
        public string Format { get; }

        public int IntegerDigits { get; private set; }

        public int DecimalDigits {get; private set; }
        public string DecimalSeparator { get; private set; }

        public bool WithSign { get; private set; }

        protected DecimalNumberBaseConverter(string format)
        {
            Format = format;

            DiscoverSeparator(ref format);
            if (!DealWithShortFormat(format))
                DealWithLongFormat(format);
        }

        public override string FieldToString(object from)
        {
            if (from == null)
                return string.Empty;
            Decimal num = (Decimal)(from is Decimal ? from : Convert.ChangeType(from, typeof(Decimal)));
            string str = (this.WithSign ? (num > Decimal.Zero ? "+" : "-") : string.Empty) + num.ToString(new string('0', this.IntegerDigits - (string.IsNullOrWhiteSpace(this.DecimalSeparator) ? 0 : 1)) + "." + new string('0', this.DecimalDigits));
            if (string.IsNullOrWhiteSpace(this.DecimalSeparator))
                str = str.Replace(".", string.Empty).Replace(",", string.Empty);
            return base.FieldToString((object)str);
        }

        private void DiscoverSeparator(ref string pattern)
        {
            pattern = pattern.ToUpperInvariant();
            if (pattern.StartsWith("+NS") || pattern.StartsWith("-NS") || pattern.StartsWith("NS"))
            {
                this.DecimalSeparator = !pattern.Contains(",") ? "." : ",";
                pattern = pattern.Replace("S", string.Empty);
            }
            else
                this.DecimalSeparator = string.Empty;
        }

        private bool DealWithShortFormat(string pattern)
        {
            pattern = pattern.ToUpperInvariant();
            if (!pattern.StartsWith("+N") && !pattern.StartsWith("-N") && (!pattern.StartsWith("N") && !pattern.StartsWith("+NS")) && (!pattern.StartsWith("-NS") && !pattern.StartsWith("NS")))
                return false;
            this.WithSign = pattern.StartsWith("+") || pattern.StartsWith("-");
            if (this.WithSign)
                pattern = pattern.Substring(1);
            int result1 = 0;
            string separator = string.IsNullOrWhiteSpace(this.DecimalSeparator) ? "." : this.DecimalSeparator;
            int result2;
            if (pattern.Contains(separator))
            {
                string[] strArray = pattern.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length != 2)
                    throw new BadFluentConfigurationException("Invalid pattern!");
                if (!int.TryParse(strArray[1], out result2))
                    throw new BadFluentConfigurationException("The numeric format is invalid!");
                if (!int.TryParse(strArray[0].Substring(1), out result1))
                    throw new BadFluentConfigurationException("The numeric format is invalid!");
            }
            else if (!int.TryParse(pattern.Substring(1), out result2))
                throw new BadFluentConfigurationException("The numeric format is invalid!");
            this.DecimalDigits = result2;
            this.IntegerDigits = result1;
            return true;
        }

        private void DealWithLongFormat(string pattern)
        {
            string separator = string.IsNullOrWhiteSpace(this.DecimalSeparator) ? "." : this.DecimalSeparator;
            pattern = pattern.ToUpperInvariant();
            this.WithSign = pattern.StartsWith("+") || pattern.StartsWith("-");
            string[] strArray = pattern.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length != 2)
                throw new BadFluentConfigurationException("The numeric format is invalid!");
            this.DecimalDigits = strArray[1].Length;
            this.IntegerDigits = strArray[0].Length - (this.WithSign ? 1 : 0);
        }
    }
}
