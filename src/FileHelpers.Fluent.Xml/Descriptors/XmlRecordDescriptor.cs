using FileHelpers.Fluent.Core.Descriptors;

namespace FileHelpers.Fluent.Xml
{
    public class XmlRecordDescriptor : RecordDescriptor
    {
        public XmlRecordDescriptor()
        {
            Version = "1.0";
            Standalone = "no";
        }

        public string RootElementName { get; set; }

        public string ElementName { get; set; }

        public string Version { get; set; }

        public string Standalone { get; set; }
    }
}
