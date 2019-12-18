using FileHelpers.Fluent.Core.Descriptors;

namespace FileHelpers.Fluent.Xml
{
    public class XmlFieldInfoDescriptor : FieldInfoDescriptor, IXmlFieldInfoDescriptor
    {
        public bool IsAttribute { get; set; }

        public string PropertyName { get; set; }
    }
}
