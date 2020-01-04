using FileHelpers.Fluent.Core.Builders;
using FileHelpers.Fluent.Core.Exceptions;
using FileHelpers.Fluent.Delimited.Descriptors;

namespace FileHelpers.Fluent.Delimited
{
    public static class DelimitedRecordDescriptorExtensions
    {
        public static FieldInfoBuilder AddField(this DelimitedRecordDescriptor descriptor, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BadFluentConfigurationException($"The {nameof(name)} cannot be null or empty");

            var fieldInfo = new FieldInfoBuilder();

            descriptor.Add(name, fieldInfo);

            return fieldInfo;
        }
    }
}
