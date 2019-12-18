using FileHelpers.Fluent.Core.Descriptors;

namespace FileHelpers.Fluent.Xml
{
    public interface IXmlFieldInfoDescriptor : IFieldInfoDescriptor, IXmlFieldPropertyNameInfoDescriptor
    {
        bool IsAttribute { get; set; }
    }
}
