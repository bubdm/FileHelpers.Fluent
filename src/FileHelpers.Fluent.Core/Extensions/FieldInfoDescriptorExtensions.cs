using FileHelpers.Fluent.Core.Converters;
using FileHelpers.Fluent.Core.Descriptors;

namespace FileHelpers.Fluent.Core.Extensions
{
    public static class FieldInfoDescriptorExtensions
    {
        public static string StringTrim(this IFieldInfoDescriptor recordInfo, string value)
        {
            switch (recordInfo.TrimMode)
            {
                case TrimMode.None:
                    return value;
                case TrimMode.Both:
                    return value.Trim();
                case TrimMode.Left:
                    return value.TrimStart();
                case TrimMode.Right:
                    return value.TrimEnd();
            }

            return value;
        }

        public static object StringToField(this IFieldInfoDescriptor fieldDescriptor, string value)
        {
            if (value == null)
                return null;

            value = fieldDescriptor.StringTrim(value);

            if (string.Empty.Equals(value) && fieldDescriptor.Converter == null)
                return value;

            if (fieldDescriptor.Converter == null && fieldDescriptor.Type == null)
                return value;

            ConverterBase converterInstance =
                fieldDescriptor.Converter == null
                ? ConverterFactory.GetDefaultConverter(fieldDescriptor.Type, fieldDescriptor.ConverterFormat)
                : ConverterFactory.GetConverter(fieldDescriptor.Converter, fieldDescriptor.ConverterFormat);

            return converterInstance == null
                ? value
                : converterInstance.StringToField(value);
        }


        public static string CreateFieldString(this IFieldInfoDescriptor fieldBuilder, object fieldValue)
        {
            ConverterBase converterInstance = null;

            if (fieldBuilder.Type != null || fieldBuilder.Converter != null)
                converterInstance = fieldBuilder.Converter == null
                ? ConverterFactory.GetDefaultConverter(fieldBuilder.Type, fieldBuilder.ConverterFormat)
                : ConverterFactory.GetConverter(fieldBuilder.Converter, fieldBuilder.ConverterFormat);

            if (converterInstance == null)
            {
                if (fieldValue == null)
                    return string.Empty;

                return fieldValue.ToString();
            }

            return converterInstance.FieldToString(fieldValue) ?? string.Empty;
        }
    }
}
