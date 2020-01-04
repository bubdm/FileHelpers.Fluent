using FileHelpers.Fluent.Core.Converters;
using FileHelpers.Fluent.Core.Descriptors;
using FileHelpers.Fluent.Core.Extensions;

namespace FileHelpers.Fluent.Delimited.Extensions
{
    internal static class DelimitedFieldBuilderDescriptorExtensions
    {
        internal static string RecordToString(this IFieldInfoDescriptor fieldInfoDescriptor, object record) =>
            fieldInfoDescriptor.CreateFieldString(record);

        internal static object StringToRecord(this IFieldInfoDescriptor fieldInfoDescriptor, string fieldString, char nullChar)
        {
            if (fieldString == null)
                return fieldInfoDescriptor.NullValue ?? null;

            string stringNullRepresentation = new string(nullChar, fieldString.Length);

            if (fieldString == stringNullRepresentation)
                return fieldInfoDescriptor.NullValue ?? null;

            fieldString = fieldInfoDescriptor.StringTrim(fieldString);
            ConverterBase converterInstance;
            if (string.Empty.Equals(fieldString) && fieldInfoDescriptor.Converter == null)
            {
                if (fieldInfoDescriptor.NullValue != null)
                    fieldString = fieldInfoDescriptor.NullValue.ToString();
                if (string.Empty.Equals(fieldString) && fieldInfoDescriptor.Converter == null)
                {
                    if (fieldInfoDescriptor.Type != null)
                    {
                        converterInstance = ConverterFactory.GetDefaultConverter(fieldInfoDescriptor.Type, fieldInfoDescriptor.ConverterFormat);
                        return converterInstance == null
                            ? fieldString
                            : converterInstance.StringToField(fieldString);
                    }
                    return fieldString;
                }
            }

            if (fieldInfoDescriptor.Converter == null && fieldInfoDescriptor.Type == null)
                return fieldString;

            if (string.IsNullOrWhiteSpace(fieldString) && fieldInfoDescriptor.NullValue != null)
                fieldString = fieldInfoDescriptor.NullValue.ToString();

            converterInstance =
                fieldInfoDescriptor.Converter == null
                ? ConverterFactory.GetDefaultConverter(fieldInfoDescriptor.Type, fieldInfoDescriptor.ConverterFormat)
                : ConverterFactory.GetConverter(fieldInfoDescriptor.Converter, fieldInfoDescriptor.ConverterFormat);

            return converterInstance == null
                ? fieldString
                : converterInstance.StringToField(fieldString);
        }

    }
}
