using FileHelpers.Fluent.Core;
using FileHelpers.Fluent.Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileHelpers.Fluent.Fixed.Tests
{
    [TestClass]
    public class FixedFluentEngineInvalidInitializationTests
    {
        [TestMethod]
        [ExpectedException(typeof(BadFluentConfigurationException))]
        public void Has_No_Fields()
        {
            var descriptor = new FixedRecordDescriptor();

            var engine = new FluentFixedEngine(descriptor);
        }

        [TestMethod]
        [ExpectedException(typeof(BadFluentConfigurationException))]
        public void Invalid_Property_Length()
        {
            var descriptor = new FixedRecordDescriptor();
            descriptor.AddField("")
                      .SetLength(10);
            var engine = new FluentFixedEngine(descriptor);
        }

        [TestMethod]
        [ExpectedException(typeof(BadFluentConfigurationException))]
        public void Invalid_Array_Length()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Function")
                      .SetLength(3);

            descriptor.AddField("ArraySize")
                      .SetLength(4)
                      .SetAlignMode(AlignMode.Right)
                      .SetAlignChar('0');

            var arrayBuilder = descriptor.AddArray("ArrayData");
            arrayBuilder.SetArrayItemLength(10)
                        .SetAlign(true);

            arrayBuilder.AddField("DealId")
                        .SetLength(10)
                        .SetAlignMode(AlignMode.Right)
                        .SetNullValue(string.Empty)
                        .SetAlignChar('0');

            var engine = new FluentFixedEngine(descriptor);

        }

        [TestMethod]
        [ExpectedException(typeof(BadFluentConfigurationException))]
        public void Invalid_ArrayItem_Length()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Function")
                      .SetLength(3);

            descriptor.AddField("ArraySize")
                      .SetLength(4)
                      .SetAlignMode(AlignMode.Right)
                      .SetAlignChar('0');

            var arrayBuilder = descriptor.AddArray("ArrayData");
            arrayBuilder.SetArrayLength(500)
                        .SetAlign(true);

            arrayBuilder.AddField("DealId")
                        .SetLength(10)
                        .SetAlignMode(AlignMode.Right)
                        .SetNullValue(string.Empty)
                        .SetAlignChar('0');

            var engine = new FluentFixedEngine(descriptor);
        }

        [TestMethod]
        [ExpectedException(typeof(BadFluentConfigurationException))]
        public void Invalid_Array_Remainder_Length()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Function")
                      .SetLength(3);

            descriptor.AddField("ArraySize")
                      .SetLength(4)
                      .SetAlignMode(AlignMode.Right)
                      .SetAlignChar('0');

            var arrayBuilder = descriptor.AddArray("ArrayData");
            arrayBuilder.SetArrayLength(500)
                        .SetArrayItemLength(11)
                        .SetAlign(true);

            arrayBuilder.AddField("DealId")
                        .SetLength(10)
                        .SetAlignMode(AlignMode.Right)
                        .SetNullValue(string.Empty)
                        .SetAlignChar('0');

            var engine = new FluentFixedEngine(descriptor);
        }

        [TestMethod]
        [ExpectedException(typeof(BadFluentConfigurationException))]
        public void Invalid_ArrayItem_Greater_Than_Array()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Function")
                      .SetLength(3);

            descriptor.AddField("ArraySize")
                      .SetLength(4)
                      .SetAlignMode(AlignMode.Right)
                      .SetAlignChar('0');

            var arrayBuilder = descriptor.AddArray("ArrayData");
            arrayBuilder.SetArrayLength(500)
                        .SetArrayItemLength(501)
                        .SetAlign(true);

            arrayBuilder.AddField("DealId")
                        .SetLength(10)
                        .SetAlignMode(AlignMode.Right)
                        .SetNullValue(string.Empty)
                        .SetAlignChar('0');

            var engine = new FluentFixedEngine(descriptor);
        }

        [TestMethod]
        [ExpectedException(typeof(BadFluentConfigurationException))]
        public void Invalid_Array_Without_Fields()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Function")
                      .SetLength(3);

            descriptor.AddField("ArraySize")
                      .SetLength(4)
                      .SetAlignMode(AlignMode.Right)
                      .SetAlignChar('0');

            var arrayBuilder = descriptor.AddArray("ArrayData");
            arrayBuilder.SetArrayLength(500)
                        .SetArrayItemLength(10)
                        .SetAlign(true);

            var engine = new FluentFixedEngine(descriptor);
        }
    }
}
