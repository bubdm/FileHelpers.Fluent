using FileHelpers.Fluent.Core.Descriptors;

namespace FileHelpers.Fluent.Fixed
{
    public class FixedFieldInfoDescriptor : FieldInfoDescriptor, IFixedFieldInfoDescriptor
    {
        public int Length { get; set; }
    }
}
