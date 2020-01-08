using FileHelpers.Fluent.Core;
using FileHelpers.Fluent.Core.Converters;
using FileHelpers.Fluent.Delimited.Descriptors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FileHelpers.Fluent.Delimited.Tests
{
    [TestClass]
    public class DelimitedFluentEngineMultiRecordWriteTests
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
            var originalContent = $"CLI|Harlen Naves|19840330{Environment.NewLine}ADR|No name street|10";

            var items = engine.ReadString(originalContent);

            var content = engine.WriteString(items);

            Assert.AreEqual(originalContent + Environment.NewLine, content);
        }
    }
}
