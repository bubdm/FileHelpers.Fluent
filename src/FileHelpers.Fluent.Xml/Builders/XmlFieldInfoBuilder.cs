using FileHelpers.Fluent.Core.Builders;

namespace FileHelpers.Fluent.Xml
{
    public class XmlFieldInfoBuilder : FieldInfoBuilder, IXmlFieldInfoDescriptor
    {
        public bool IsAttribute { get; set; }
        public string PropertyName { get; set; }

        public XmlFieldInfoBuilder SetIsAttribute(bool isAttribute)
        {
            IsAttribute = isAttribute;
            return this;
        }

        public XmlFieldInfoBuilder SetPropertyName(string propertyName)
        {
            PropertyName = propertyName;
            return this;
        }
    }
}
