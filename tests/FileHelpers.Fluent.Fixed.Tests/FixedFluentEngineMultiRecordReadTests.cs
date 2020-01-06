using FileHelpers.Fluent.Core;
using FileHelpers.Fluent.Core.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace FileHelpers.Fluent.Fixed.Tests
{
    [TestClass]
    public class FixedFluentEngineMultiRecordReadTests
    {
        [TestMethod]
        public void Two_Record_Types()
        {
            var clientDescriptor = new FixedRecordDescriptor();

            clientDescriptor.AddField("RecordType")
                            .SetLength(3)
                            .SetTrimMode(TrimMode.Both)
                            .SetAlignMode(AlignMode.Right)
                            .SetAlignChar(' ');

            clientDescriptor.AddField("Name")
                            .SetLength(50)
                            .SetAlignMode(AlignMode.Right)
                            .SetTrimMode(TrimMode.Both)
                            .SetAlignChar(' ');

            clientDescriptor.AddField("Doc")
                      .SetLength(14)
                      .SetAlignMode(AlignMode.Left)
                      .SetConverter(typeof(LongConverter))
                      .SetAlignChar('0');

            clientDescriptor.AddField("BirthDate")
                      .SetLength(8)
                      .SetConverter(typeof(DateTimeConverter))
                      .SetConverterFormat("yyyyMMdd");

            var addressDescriptor = new FixedRecordDescriptor();

            addressDescriptor.AddField("RecordType")
                            .SetLength(3)
                            .SetTrimMode(TrimMode.Both)
                            .SetAlignMode(AlignMode.Right)
                            .SetAlignChar(' ');

            addressDescriptor.AddField("Street")
                             .SetLength(20)
                             .SetTrimMode(TrimMode.Both)
                             .SetAlignMode(AlignMode.Right)
                             .SetAlignChar(' ');
            addressDescriptor.AddField("City")
                             .SetLength(20)
                             .SetTrimMode(TrimMode.Both)
                             .SetAlignMode(AlignMode.Right)
                             .SetAlignChar(' ');

            var engine = new FluentFixedEngine(new List<RecordItem>
            {
                new RecordItem
                {
                    Descriptor = clientDescriptor,
                    Name = "Client",
                    RegexPattern = "^(CLI)",
                    RecordTypeProperty = "RecordType"
                },
                new RecordItem
                {
                    Descriptor = addressDescriptor,
                    Name = "Address",
                    RegexPattern = "^(ADR)",
                    RecordTypeProperty = "RecordType"
                }
            });

            string content =
                "CLIHarlen Naves                                      0000587065966319840330\r\nADRNo name street      No name city        ";

            var items = engine.ReadString(content);

            Assert.IsNotNull(items);

            Assert.AreEqual(2, items.Length);

            dynamic customer = items[0];

            Assert.AreEqual("CLI", customer.RecordType);
            Assert.AreEqual("Harlen Naves", customer.Name);
            Assert.AreEqual((long)05870659663, (long)customer.Doc);

            dynamic address = items[1];

            Assert.AreEqual("ADR", address.RecordType);
            Assert.AreEqual("No name street", address.Street);
            Assert.AreEqual("No name city", address.City);
        }

        [TestMethod]
        public void Two_Record_Types_With_Array()
        {
            var clientDescriptor = new FixedRecordDescriptor();

            clientDescriptor.AddField("RecordType")
                            .SetLength(3)
                            .SetTrimMode(TrimMode.Both)
                            .SetAlignMode(AlignMode.Right)
                            .SetAlignChar(' ');

            clientDescriptor.AddField("Name")
                            .SetLength(50)
                            .SetAlignMode(AlignMode.Right)
                            .SetTrimMode(TrimMode.Both)
                            .SetAlignChar(' ');

            clientDescriptor.AddField("Doc")
                            .SetLength(14)
                            .SetAlignMode(AlignMode.Left)
                            .SetConverter(typeof(LongConverter))
                            .SetAlignChar('0');

            clientDescriptor.AddField("BirthDate")
                            .SetLength(8)
                            .SetConverter(typeof(DateTimeConverter))
                            .SetConverterFormat("yyyyMMdd");

            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("RecordType")
                            .SetLength(3)
                            .SetTrimMode(TrimMode.Both)
                            .SetAlignMode(AlignMode.Right)
                            .SetAlignChar(' ');

            descriptor.AddField("Function")
                      .SetLength(3)
                      .SetAlignMode(AlignMode.Right)
                      .SetAlignChar('0');

            descriptor.AddField("ArraySize")
                      .SetLength(4)
                      .SetAlignMode(AlignMode.Left)
                      .SetAlignChar('0')
                      .SetConverter(typeof(IntegerConverter));

            var arrayDescriptor = descriptor.AddArray("ArrayData")
                                            .SetArrayLength(500)
                                            .SetArrayItemLength(10)
                                            .SetAlign(true)
                                            .SetAlignChar('0');

            arrayDescriptor.AddField("DealId")
                           .SetLength(10)
                           .SetNullValue(string.Empty)
                           .SetAlignMode(AlignMode.Right)
                           .SetAlignChar('0');



            ExpandoObject item = new ExpandoObject();
            item.TryAdd("Function", "001");
            item.TryAdd("ArraySize", 25);

            List<ExpandoObject> arrayData = new List<ExpandoObject>();
            for (int i = 0; i < 25; i++)
            {
                ExpandoObject arrayItem = new ExpandoObject();
                arrayItem.TryAdd("DealId", "STOCKASC" + i.ToString().PadLeft(2, '0'));
                arrayData.Add(arrayItem);
            }

            item.TryAdd("ArrayData", arrayData);

            var engine = new FluentFixedEngine(new List<RecordItem>
            {
                new RecordItem
                {
                    Descriptor = clientDescriptor,
                    Name = "Client",
                    RegexPattern = "^(CLI)",
                    RecordTypeProperty = "RecordType"
                },
                new RecordItem
                {
                    Descriptor = descriptor,
                    Name = "Test",
                    RegexPattern = "^(TST)",
                    RecordTypeProperty = "RecordType"
                }
            });

            string content =
                "TST0010025STOCKASC00STOCKASC01STOCKASC02STOCKASC03STOCKASC04STOCKASC05STOCKASC06STOCKASC07STOCKASC08STOCKASC09STOCKASC10STOCKASC11STOCKASC12STOCKASC13STOCKASC14STOCKASC15STOCKASC16STOCKASC17STOCKASC18STOCKASC19STOCKASC20STOCKASC21STOCKASC22STOCKASC23STOCKASC240000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
                Environment.NewLine + "CLIHarlen Naves                                      0000587065966319840330" + Environment.NewLine;

            var records = engine.ReadString(content);

            Assert.IsNotNull(records);
            Assert.AreEqual(2, records.Length);

            dynamic customer = records[1];

            Assert.AreEqual("CLI", customer.RecordType);
            Assert.AreEqual("Harlen Naves", customer.Name);
            Assert.AreEqual((long)05870659663, (long)customer.Doc);
            Assert.IsNotNull(customer.BirthDate);
            Assert.AreEqual(new DateTime(1984, 3, 30).Date, ((DateTime)customer.BirthDate).Date);

            dynamic items = records[0];

            Assert.AreEqual("TST", items.RecordType);
            Assert.AreEqual("001", items.Function);
            Assert.AreEqual(25, (int)items.ArraySize);
            Assert.AreEqual(25, ((object[])items.ArrayData).Length);

            dynamic firstItem = ((object[])items.ArrayData)[0];

            Assert.IsNotNull(firstItem);
            Assert.AreEqual("STOCKASC00", firstItem.DealId);

            dynamic lastItem = ((object[])items.ArrayData)[24];

            Assert.IsNotNull(lastItem);
            Assert.AreEqual("STOCKASC24", lastItem.DealId);
        }
    }
}
