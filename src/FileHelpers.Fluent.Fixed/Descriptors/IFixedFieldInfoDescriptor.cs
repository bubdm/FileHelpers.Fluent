using FileHelpers.Fluent.Core.Descriptors;

namespace FileHelpers.Fluent.Fixed
{
    public interface IFixedFieldInfoDescriptor : IFieldInfoDescriptor
    {
        int Length { get; set; }
    }
}
