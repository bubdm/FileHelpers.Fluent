using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileHelpers.Fluent.Xml.Tests
{
    [TestClass]
    public class XmlFluentEngineWriteTests
    {
        [TestMethod]
        public void Write_Xml_With_Array()
        {
            var descriptor = new XmlRecordDescriptor
            {
                RootElementName = "Clients",
                ElementName = "Client"
            };

            descriptor.AddField("Name");
            descriptor.AddField("Document")
                .SetIsAttribute(true);
            var addressesDescriptor = descriptor.AddArray("Addresses")
                .SetElementName("Address");
            addressesDescriptor.AddField("Street")
                .SetIsAttribute(true);
            addressesDescriptor.AddField("Number")
                .SetIsAttribute(true)
                .SetType(typeof(int));

            var engine = new XmlFluentEngine(descriptor);

            var xml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?><Clients><Client Document=\"987654321\"><Name>No name 1</Name><Addresses><Address Street=\"First Street\" Number=\"1\" /><Address Street=\"Second Street\" Number=\"1\" /><Address Street=\"Third street\" Number=\"1\" /></Addresses></Client><Client Document=\"123456789\"><Name>No name 2</Name><Addresses><Address Street=\"First Street\" Number=\"1\" /><Address Street=\"Second Street\" Number=\"1\" /><Address Street=\"Third street\" Number=\"1\" /></Addresses></Client></Clients>";

            var items = engine.ReadString(xml);

            var xmlOutput = engine.WriteString(items);

            Assert.AreEqual(xml, xmlOutput);
        }
    }
}
