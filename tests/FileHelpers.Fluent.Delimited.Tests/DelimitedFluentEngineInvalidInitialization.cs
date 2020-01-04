using FileHelpers.Fluent.Core;
using FileHelpers.Fluent.Core.Exceptions;
using FileHelpers.Fluent.Delimited.Descriptors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FileHelpers.Fluent.Delimited.Tests
{
    [TestClass]
    public class DelimitedFluentEngineInvalidInitialization
    {
        [TestMethod]
        public void Read()
        {
            var descriptor = new DelimitedRecordDescriptor("|");

            descriptor.AddField("Name")
                .SetTrimMode(TrimMode.Both)
                .SetAlignMode(AlignMode.Left)
                ;

            descriptor.AddField("BirthDate")
                .SetType(typeof(DateTime))
                .SetConverterFormat("yyyyMMdd")
                ;

            var engine = new DelimitedFluentEngine(descriptor);
            var items = engine.ReadString("HARLEN      |19840330");

            Assert.IsNotNull(items);
            Assert.AreEqual(1, items.Length);
            dynamic item = items[0];

            Assert.AreEqual("HARLEN", item.Name);
            Assert.IsNotNull(item.BirthDate);
            Assert.AreEqual(new DateTime(1984, 3, 30).Date, ((DateTime)item.BirthDate).Date);
        }

        [TestMethod]
        public void Write()
        {
            var descriptor = new DelimitedRecordDescriptor("|");

            descriptor.AddField("Name")
                .SetTrimMode(TrimMode.None)
                .SetAlignMode(AlignMode.Left)
                ;

            descriptor.AddField("BirthDate")
                .SetType(typeof(DateTime))
                .SetConverterFormat("yyyyMMdd")
                ;
            var originalContent = "HARLEN      |19840330";
            var engine = new DelimitedFluentEngine(descriptor);
            var items = engine.ReadString("HARLEN      |19840330");

            Assert.IsNotNull(items);
            Assert.AreEqual(1, items.Length);

            var content = engine.WriteString(items);

            Assert.IsNotNull(content);
            Assert.AreNotEqual(string.Empty, content);
            Assert.AreEqual(originalContent, content.Replace("\r\n", string.Empty));
        }
    }
}
