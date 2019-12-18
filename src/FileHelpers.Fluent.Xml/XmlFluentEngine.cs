using FileHelpers.Fluent.Core;
using FileHelpers.Fluent.Core.Descriptors;
using FileHelpers.Fluent.Core.Exceptions;
using FileHelpers.Fluent.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FileHelpers.Fluent.Xml
{
    public class XmlFluentEngine : FluentEngine
    {
        public XmlFluentEngine(IRecordDescriptor descriptor, Encoding encoding = null) : base(descriptor, encoding)
        {
        }

        #region Private Methods

        private ExpandoObject ReadElement(XElement element, IRecordDescriptor descriptor)
        {
            var item = new ExpandoObject();

            foreach (KeyValuePair<string, IFieldInfoTypeDescriptor> fieldInfoTypeDescriptor in descriptor.Fields)
            {
                string propertyName = string.IsNullOrWhiteSpace(((IXmlFieldPropertyNameInfoDescriptor)fieldInfoTypeDescriptor.Value).PropertyName)
                            ? fieldInfoTypeDescriptor.Key
                            : ((IXmlFieldPropertyNameInfoDescriptor)fieldInfoTypeDescriptor.Value).PropertyName;

                if (fieldInfoTypeDescriptor.Value.IsArray)
                {
                    item.InternalTryAdd(propertyName,
                    ((IXmlArrayFieldInfoDescriptor)fieldInfoTypeDescriptor.Value).ToRecordArray(
                            fieldInfoTypeDescriptor.Key,
                            element
                        ));
                    continue;
                }

                item.InternalTryAdd(propertyName,
                        ((IXmlFieldInfoDescriptor)fieldInfoTypeDescriptor.Value).ToRecord(fieldInfoTypeDescriptor.Key, element)
                    );
            }

            return item;
        }

        #endregion

        public override async Task<ExpandoObject[]> ReadStreamAsync(StreamReader reader)
        {
            IList<ExpandoObject> items = new List<ExpandoObject>();

#if NETSTANDARD2_0
            var xmlDocument = await Task.Run(() => XDocument.Load(reader, LoadOptions.None));
#else
            var xmlDocument = await XDocument.LoadAsync(reader, LoadOptions.None, new CancellationToken());
#endif

            IEnumerable<XElement> elements = null;

            string rootElementName = ((XmlRecordDescriptor)Descriptor).RootElementName;
            if (string.IsNullOrWhiteSpace(rootElementName))
                elements = xmlDocument.Elements();
            else
            {
                var firstElement = xmlDocument.Elements().FirstOrDefault();
                elements = (firstElement == null || firstElement.Name.LocalName != rootElementName)
                    ? xmlDocument.Elements()
                    : firstElement.Elements();
            }
            
            foreach (XElement element in elements)
            {
                items.Add(ReadElement(element, Descriptor));
            }

            return items.ToArray();
        }

        public override async Task WriteStreamAsync(TextWriter writer, IEnumerable<ExpandoObject> records, bool flush = true)
        {
            var xmlDescriptor = ((XmlRecordDescriptor)Descriptor);
            string rootElementName = xmlDescriptor.RootElementName;
            string elementName = xmlDescriptor.ElementName;

            if (records.Count() > 1 && rootElementName == elementName)
                throw new BadFluentConfigurationException("There are more than one record without root element. It is impossible to create a valid XML.");

            XElement rootElement = new XElement(rootElementName);
            XDocument xmlDocument = new XDocument(new XDeclaration(xmlDescriptor.Version, Encoding.BodyName, xmlDescriptor.Standalone), rootElement);

            foreach (ExpandoObject record in records)
            {
                XElement element;
                if (rootElementName == elementName)
                    element = rootElement;
                else
                {
                    element = new XElement(elementName);
                    rootElement.Add(element);
                }
                foreach (KeyValuePair<string, object> keyValuePair in record)
                {
                    string propertyName = keyValuePair.Key;
                    if (!Descriptor.Fields.TryGetValue(keyValuePair.Key, out IFieldInfoTypeDescriptor fieldDescriptor))
                    {
                        fieldDescriptor = Descriptor.Fields.Values.FirstOrDefault(x => ((IXmlFieldPropertyNameInfoDescriptor)x).PropertyName == keyValuePair.Key);
                        if (fieldDescriptor == null)
                            throw new Exception($"The field {keyValuePair.Key} is not configured");
                    }

                    if (fieldDescriptor.IsArray)
                    {
                        ((IXmlArrayFieldInfoDescriptor)fieldDescriptor).ArrayToXml(propertyName, element, (IEnumerable<dynamic>)keyValuePair.Value);
                        continue;
                    }
                    ((IXmlFieldInfoDescriptor)fieldDescriptor).RecordToXml(propertyName, element, keyValuePair.Value);
                }

            }
#if NETSTANDARD2_0
            await Task.Run(() => xmlDocument.Save(writer, SaveOptions.DisableFormatting));
#else
            await xmlDocument.SaveAsync(writer, SaveOptions.DisableFormatting, new CancellationToken());
#endif
        }
    }
}
