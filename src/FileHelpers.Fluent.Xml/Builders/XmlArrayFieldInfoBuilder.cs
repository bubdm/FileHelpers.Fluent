using FileHelpers.Fluent.Core.Descriptors;
using System.Collections.Generic;

namespace FileHelpers.Fluent.Xml
{
    public class XmlArrayFieldInfoBuilder : IXmlArrayFieldInfoDescriptor
    {
        public XmlArrayFieldInfoBuilder()
        {
            IsArray = true;
            Fields = new Dictionary<string, IFieldInfoTypeDescriptor>();
        }

        public bool IsArray { get; }

        public IDictionary<string, IFieldInfoTypeDescriptor> Fields { get; }
        public string PropertyName { get; set; }

        public string ElementName { get; set; }
        public char NullChar { get; set; }

        public void Add(string fieldName, IFieldInfoTypeDescriptor fieldDescriptor)
        {
            if (Fields.ContainsKey(fieldName))
            {
                Fields[fieldName] = fieldDescriptor;
                return;
            }
            Fields.Add(fieldName, fieldDescriptor);
        }
    }
}
