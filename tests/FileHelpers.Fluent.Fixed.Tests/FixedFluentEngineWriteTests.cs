using FileHelpers.Fluent.Core;
using FileHelpers.Fluent.Core.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace FileHelpers.Fluent.Fixed.Tests
{
    [TestClass]
    public class FixedFluentEngineWriteTests
    {
        [TestMethod]
        public void Write()
        {
            var recordDescriptor = new FixedRecordDescriptor();
            recordDescriptor.AddField("Name")
                            .SetLength(50);
            recordDescriptor.AddField("Doc")
                            .SetLength(14)
                            .SetAlignMode(AlignMode.Left)
                            .SetAlignChar('0');

            var engine = new FluentFixedEngine(recordDescriptor);

            ExpandoObject item = new ExpandoObject();
            item.TryAdd("Name", "Harlen Naves");
            item.TryAdd("Doc", 05870659663);

            string line = engine.WriteString(new[] { item });

            Assert.AreEqual("Harlen Naves                                      00005870659663\r\n", line);
        }

        [TestMethod]
        public void Write_With_DateTime_yyyyMMdd_Converter()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(50)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Doc")
                      .SetLength(14)
                      .SetAlignMode(AlignMode.Left)
                      .SetAlignChar('0');

            descriptor.AddField("BirthDate")
                      .SetLength(8)
                      .SetConverter(typeof(DateTimeConverter))
                      .SetConverterFormat("yyyyMMdd");

            var engine = new FluentFixedEngine(descriptor);

            var item = new ExpandoObject();
            item.TryAdd("Name", "Harlen Naves");
            item.TryAdd("Doc", 05870659663);
            item.TryAdd("BirthDate", new DateTime(1984, 03, 30));

            string line = engine.WriteString(new[] { item });

            Assert.AreEqual("Harlen Naves                                      0000587065966319840330" + Environment.NewLine, line);
        }

        [TestMethod]
        public void Write_With_Array()
        {
            var descriptor = new FixedRecordDescriptor();

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

            FluentFixedEngine engine = descriptor.Build();

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

            string content = engine.WriteString(new[] { item });

            Assert.AreEqual("0010025STOCKASC00STOCKASC01STOCKASC02STOCKASC03STOCKASC04STOCKASC05STOCKASC06STOCKASC07STOCKASC08STOCKASC09STOCKASC10STOCKASC11STOCKASC12STOCKASC13STOCKASC14STOCKASC15STOCKASC16STOCKASC17STOCKASC18STOCKASC19STOCKASC20STOCKASC21STOCKASC22STOCKASC23STOCKASC240000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" + Environment.NewLine, content);
        }

        [TestMethod]
        public void Write_With_Array_With_No_Align()
        {
            var descriptor = new FixedRecordDescriptor();

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
                                            .SetAlign(false);

            arrayDescriptor.AddField("DealId")
                           .SetLength(10)
                           .SetNullValue(string.Empty)
                           .SetAlignMode(AlignMode.Right)
                           .SetAlignChar('0');

            FluentFixedEngine engine = descriptor.Build();

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

            string content = engine.WriteString(new[] { item });

            Assert.AreEqual("0010025STOCKASC00STOCKASC01STOCKASC02STOCKASC03STOCKASC04STOCKASC05STOCKASC06STOCKASC07STOCKASC08STOCKASC09STOCKASC10STOCKASC11STOCKASC12STOCKASC13STOCKASC14STOCKASC15STOCKASC16STOCKASC17STOCKASC18STOCKASC19STOCKASC20STOCKASC21STOCKASC22STOCKASC23STOCKASC24" + Environment.NewLine, content);
        }

        [TestMethod]
        public void Write_With_Decimal_Positive_N_Integer_Signal_Format()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(10)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Price")
                .SetLength(18)
                .SetConverter(typeof(DecimalConverter))
                .SetConverterFormat("+N15.2");

            var engine = new FluentFixedEngine(descriptor);

            var items = engine.ReadString("Product 1 +00000000000000129");

            Assert.AreEqual(1, items.Length);
            dynamic item = items[0];
            Assert.AreEqual(1.29M, item.Price);
            var output = engine.WriteString(items).Replace("\r\n", string.Empty);
            Assert.AreEqual("Product 1 +00000000000000129", output);
        }

        [TestMethod]
        public void Write_With_Decimal_Positive_N_Integer_Signal_Separator_Format()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(10)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Price")
                .SetLength(18)
                .SetConverter(typeof(DecimalConverter))
                .SetConverterFormat("+NS15.2");

            var engine = new FluentFixedEngine(descriptor);

            var items = engine.ReadString("Product 1 +00000000000001.29");

            Assert.AreEqual(1, items.Length);
            dynamic item = items[0];
            Assert.AreEqual(1.29M, item.Price);
            var output = engine.WriteString(items).Replace("\r\n", string.Empty);
            Assert.AreEqual("Product 1 +00000000000001.29", output);
        }

        [TestMethod]
        public void Write_Boolean_With_Format_False()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(10)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Enabled")
                .SetLength(1)
                .SetConverter(typeof(BooleanConverter))
                .SetConverterFormat("1:T,0:F");

            var engine = new FluentFixedEngine(descriptor);

            var items = engine.ReadString("Product 1 T");

            Assert.AreEqual(1, items.Length);
            dynamic item = items[0];
            Assert.AreEqual(true, item.Enabled);
            item.Enabled = false;
            var output = engine.WriteString(items).Replace("\r\n", string.Empty);
            Assert.AreEqual("Product 1 F", output);

        }

        [TestMethod]
        public void Write_Boolean_With_Format_True()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(10)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Enabled")
                .SetLength(1)
                .SetConverter(typeof(BooleanConverter))
                .SetConverterFormat("1:T,0:F");

            var engine = new FluentFixedEngine(descriptor);

            var items = engine.ReadString("Product 1 T");

            Assert.AreEqual(1, items.Length);
            dynamic item = items[0];
            Assert.AreEqual(true, item.Enabled);
            
            var output = engine.WriteString(items).Replace("\r\n", string.Empty);
            Assert.AreEqual("Product 1 T", output);

        }

        [TestMethod]
        public void Write_Boolean_With_Format_True_Diff_Value()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(10)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Enabled")
                .SetLength(1)
                .SetConverter(typeof(BooleanConverter))
                .SetConverterFormat("1:V,0:F");

            var engine = new FluentFixedEngine(descriptor);

            var items = engine.ReadString("Product 1 V");

            Assert.AreEqual(1, items.Length);
            dynamic item = items[0];
            Assert.AreEqual(true, item.Enabled);

            var output = engine.WriteString(items).Replace("\r\n", string.Empty);
            Assert.AreEqual("Product 1 V", output);

        }
    }
}
