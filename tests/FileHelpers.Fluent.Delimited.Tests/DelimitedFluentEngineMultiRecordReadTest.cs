using FileHelpers.Fluent.Core;
using FileHelpers.Fluent.Core.Converters;
using FileHelpers.Fluent.Delimited.Descriptors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FileHelpers.Fluent.Delimited.Tests
{
    [TestClass]
    public class DelimitedFluentEngineMultiRecordReadTest
    {
        [TestMethod]
        public void Two_Record_Types()
        {
            var clientDescriptor = new DelimitedRecordDescriptor("|");

            clientDescriptor
                .AddField("RecordType")
                .SetTrimMode(TrimMode.Both);

            clientDescriptor
                .AddField("Name")
                .SetTrimMode(TrimMode.Both);

            clientDescriptor
                .AddField("BirthDate")
                .SetTrimMode(TrimMode.Both)
                .SetConverter(typeof(DateTimeConverter))
                .SetConverterFormat("yyyyMMdd");

            var addressDescriptor = new DelimitedRecordDescriptor("|");

            addressDescriptor
                .AddField("RecordType")
                .SetTrimMode(TrimMode.Both);

            addressDescriptor
                .AddField("Street")
                .SetTrimMode(TrimMode.Both);

            addressDescriptor
                .AddField("Number")
                .SetTrimMode(TrimMode.Both)
                .SetType(typeof(int));

            var engine = new DelimitedFluentEngine(new List<RecordItem>
            {
                new RecordItem
                {
                    Descriptor = clientDescriptor,
                    Name = "Client",
                    RecordTypeProperty = "RecordType",
                    RegexPattern = "^(CLI)"
                },
                new RecordItem
                {
                    Descriptor = addressDescriptor,
                    Name = "Address",
                    RecordTypeProperty = "RecordType",
                    RegexPattern = "^(ADR)"
                }
            });
            var content = $"CLI|Harlen Naves     |19840330{Environment.NewLine}ADR| No name street   | 10";

            var items = engine.ReadString(content);

            Assert.IsNotNull(items);
            Assert.AreEqual(2, items.Length);

            dynamic client = items[0];

            Assert.AreEqual("CLI", client.RecordType);
            Assert.AreEqual("Harlen Naves", client.Name);
            Assert.AreEqual(new DateTime(1984, 3, 30).Date, ((DateTime)client.BirthDate).Date);

            dynamic address = items[1];

            Assert.AreEqual("ADR", address.RecordType);
            Assert.AreEqual("No name street", address.Street);
            Assert.AreEqual(10, (int)address.Number);
        }
    }
}
