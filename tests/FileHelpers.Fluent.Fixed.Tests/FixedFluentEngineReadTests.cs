using FileHelpers.Fluent.Core;
using FileHelpers.Fluent.Core.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileHelpers.Fluent.Fixed.Tests
{
    [TestClass]
    public class FixedFluentEngineReadTests
    {
        [TestMethod]
        public void Read_With_DateTime_yyyyMMdd_Converter()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(50)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Doc")
                      .SetLength(14)
                      .SetAlignMode(AlignMode.Left)
                      .SetConverter(typeof(LongConverter))
                      .SetAlignChar('0');

            descriptor.AddField("BirthDate")
                      .SetLength(8)
                      .SetConverter(typeof(DateTimeConverter))
                      .SetConverterFormat("yyyyMMdd");

            var engine = new FluentFixedEngine(descriptor);

            var items = engine.ReadString("Harlen Naves                                      0000587065966319840330");

            Assert.AreEqual(1, items.Length);

            dynamic item = items[0];

            Assert.AreEqual("Harlen Naves", item.Name);
            Assert.AreEqual(05870659663, item.Doc);
        }

        [TestMethod]
        public void Read_With_Array()
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

            var engine = new FluentFixedEngine(descriptor);

            var items = engine.ReadString(
                "0010025STOCKAPA17STOCKASS18STOCKASS17STOCKDIC18STOCKDIC17STOCKDIP18STOCKDIP17STOCKGOT18STOCKGOT17STOCKLUV18STOCKLUV17STOCKNOR18STOCKNOR17STOCKRIV18STOCKRIV17STOCKSUE18STOCKSUE17STOCKTRC18STOCKTRC17STOCKTRV18STOCKTRV17STOCKLAP18STOCKLAP17STOCKASC18STOCKASC17000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");

            Assert.AreEqual(1, items.Length);

            dynamic item = items[0];

            Assert.AreEqual("001", item.Function);
            Assert.AreEqual(25, item.ArraySize);
            Assert.AreEqual(item.ArraySize, item.ArrayData.Length);
        }

        [TestMethod]
        public void Read_With_NullValue()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(50)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Doc")
                      .SetLength(14)
                      .SetAlignMode(AlignMode.Left)
                      .SetConverter(typeof(LongConverter))
                      .SetNullValue("05870659663")
                      .SetAlignChar('0');

            var engine = new FluentFixedEngine(descriptor);

            var items = engine.ReadString("Harlen Naves                                                    ");


        }

        [TestMethod]
        public void Read_With_Decimal_Positive()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(10)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Price")
                .SetLength(18)
                .SetConverter(typeof(DecimalConverter))
                .SetConverterFormat("0.00");

            var engine = new FluentFixedEngine(descriptor);

            var itens = engine.ReadString("Product 1 000000000000000129");
        }

        [TestMethod]
        public void Read_With_Decimal_Positive_N_Format()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(10)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Price")
                .SetLength(18)
                .SetConverter(typeof(DecimalConverter))
                .SetConverterFormat("N2");

            var engine = new FluentFixedEngine(descriptor);

            var items = engine.ReadString("Product 1 000000000000000129");

            Assert.AreEqual(1, items.Length);
            dynamic item = items[0];
            Assert.AreEqual(1.29M, item.Price);
        }

        [TestMethod]
        public void Read_With_Decimal_Positive_N_Integer_Format()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(10)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Price")
                .SetLength(18)
                .SetConverter(typeof(DecimalConverter))
                .SetConverterFormat("N16.2");

            var engine = new FluentFixedEngine(descriptor);

            var items = engine.ReadString("Product 1 000000000000000129");

            Assert.AreEqual(1, items.Length);
            dynamic item = items[0];
            Assert.AreEqual(1.29M, item.Price);
        }

        [TestMethod]
        public void Read_With_Decimal_Positive_N_Integer_Signal_Format()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(10)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Price")
                .SetLength(18)
                .SetConverter(typeof(DecimalConverter))
                .SetConverterFormat("+N16.2");

            var engine = new FluentFixedEngine(descriptor);

            var items = engine.ReadString("Product 1 000000000000000129");

            Assert.AreEqual(1, items.Length);
            dynamic item = items[0];
            Assert.AreEqual(1.29M, item.Price);
        }

        [TestMethod]
        public void Read_With_Decimal_Positive_N_3_Format()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(10)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Price")
                .SetLength(18)
                .SetConverter(typeof(DecimalConverter))
                .SetConverterFormat("N3");

            var engine = new FluentFixedEngine(descriptor);

            var items = engine.ReadString("Product 1 000000000000000129");

            Assert.AreEqual(1, items.Length);
            dynamic item = items[0];
            Assert.AreEqual(0.129M, item.Price);
        }

        [TestMethod]
        public void Read_With_Decimal_Negative()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(10)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Price")
                .SetLength(18)
                .SetConverter(Type.GetType("FileHelpers.Fluent.Core.Converters.DecimalConverter, FileHelpers.Fluent.Core", false))
                .SetConverterFormat("0.00");

            var engine = new FluentFixedEngine(descriptor);

            var items = engine.ReadString("Product 1 -00000000000000129");

            Assert.AreEqual(1, items.Length);
            dynamic item = items[0];
            Assert.AreEqual(-1.29M, item.Price);
        }

        [TestMethod]
        public void Read_With_Float_Negative()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(10)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Price")
                .SetLength(12)
                .SetConverter(typeof(FloatConverter))
                .SetConverterFormat("0.00");

            var engine = new FluentFixedEngine(descriptor);

            var items = engine.ReadString("Product 1 -00000000129");

            Assert.AreEqual(1, items.Length);
            dynamic item = items[0];
            Assert.AreEqual(-1.29F, (float)item.Price);
        }

        [TestMethod]
        public void Read_With_Double_Negative_LessChars()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(10)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Price")
                .SetLength(12)
                .SetConverter(typeof(DoubleConverter))
                .SetConverterFormat("0.00");

            var engine = new FluentFixedEngine(descriptor);

            var items = engine.ReadString("Product 1 -129");

            Assert.AreEqual(1, items.Length);
            dynamic item = items[0];
            Assert.AreEqual(-1.29, item.Price);
        }

        [TestMethod]
        public void Read_Multi_Array()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("MessageAction")
                .SetLength(32)
                .SetTrimMode(TrimMode.Right);

            descriptor.AddField("NUMPARAM")
                .SetLength(9)
                .SetAlignMode(AlignMode.Right)
                .SetAlignChar('0')
                .SetType(typeof(int))
                .SetTrimMode(TrimMode.Both);

            descriptor.AddField("TPCONTR")
                .SetLength(1)
                .SetTrimMode(TrimMode.None)
                ;

            descriptor.AddField("TPPARM")
                .SetLength(1)
                .SetTrimMode(TrimMode.None)
                ;
            descriptor.AddField("TPCHASSI")
                .SetLength(1)
                .SetTrimMode(TrimMode.None)
                ;
            descriptor.AddField("NUMCDB")
                .SetLength(10)
                .SetTrimMode(TrimMode.Both);

            descriptor.AddField("TPBONUS")
                .SetLength(1)
                .SetTrimMode(TrimMode.None)
                ;

            descriptor.AddField("NUMCONTRINI")
                .SetLength(9)
                .SetAlignMode(AlignMode.Right)
                .SetAlignChar('0')
                .SetType(typeof(int))
                .SetTrimMode(TrimMode.Both);

            descriptor.AddField("NUMCONTRFIM")
                .SetLength(9)
                .SetAlignMode(AlignMode.Right)
                .SetAlignChar('0')
                .SetType(typeof(int))
                .SetTrimMode(TrimMode.Both);

            descriptor.AddField("TXTDESCRICAO")
                .SetLength(80)
                .SetTrimMode(TrimMode.Right);

            var accountingEventDescriptor = descriptor.AddArray("AccountingEvent")
                .SetAlign(true)
                .SetAlignChar(' ')
                .SetArrayLength(5590)
                .SetArrayItemLength(1118);

            accountingEventDescriptor.AddField("NUMPARAM")
                .SetLength(9)
                .SetAlignMode(AlignMode.Right)
                .SetAlignChar('0')
                .SetType(typeof(int))
                .SetTrimMode(TrimMode.Both);

            accountingEventDescriptor.AddField("NUMEVENT")
                .SetLength(9)
                .SetAlignMode(AlignMode.Right)
                .SetAlignChar('0')
                .SetType(typeof(int))
                .SetTrimMode(TrimMode.Both);

            accountingEventDescriptor.AddField("TXTDESCRICAO")
                .SetLength(80)
                .SetTrimMode(TrimMode.Right);

            var accountingDataDescriptor = accountingEventDescriptor.AddSubArray("AccountingData")
                .SetAlign(true)
                .SetAlignChar(' ')
                .SetArrayLength(1020)
                .SetArrayItemLength(102);

            accountingDataDescriptor.AddField("NUMPARAM")
                .SetLength(9)
                .SetAlignMode(AlignMode.Right)
                .SetAlignChar('0')
                .SetType(typeof(int))
                .SetTrimMode(TrimMode.Both);

            accountingDataDescriptor.AddField("NUMEVENT")
                .SetLength(9)
                .SetAlignMode(AlignMode.Right)
                .SetAlignChar('0')
                .SetType(typeof(int))
                .SetTrimMode(TrimMode.Both);

            accountingDataDescriptor.AddField("CODTIPO")
                .SetLength(2)
                .SetTrimMode(TrimMode.None);

            accountingDataDescriptor.AddField("NUMSEQ")
                .SetLength(4)
                .SetAlignMode(AlignMode.Right)
                .SetAlignChar('0')
                .SetType(typeof(int))
                .SetTrimMode(TrimMode.Both);

            accountingDataDescriptor.AddField("DESCCONTA")
                .SetLength(10)
                .SetTrimMode(TrimMode.Right);

            accountingDataDescriptor.AddField("DESCCENTRO")
                .SetLength(10)
                .SetTrimMode(TrimMode.Right);

            accountingDataDescriptor.AddField("VLRPERCEN")
                .SetLength(5)
                .SetAlignMode(AlignMode.Right)
                .SetAlignChar(' ')
                .SetConverterFormat("N2")
                .SetConverter(typeof(DecimalConverter))
                .SetTrimMode(TrimMode.Both);

            accountingDataDescriptor.AddField("CODLCTO")
                .SetLength(2)
                .SetTrimMode(TrimMode.None);

            accountingDataDescriptor.AddField("CODCOPA")
                .SetLength(1)
                .SetTrimMode(TrimMode.None);

            accountingDataDescriptor.AddField("TXTDESCRICAO")
                .SetLength(50)
                .SetTrimMode(TrimMode.Right);

            var engine = descriptor.Build();

            var items = engine.ReadString("LOAD                            000000001PRC0000000000 000000000000000000SETUP PADRÃO - REGRA DEFAULT                                                    000000001000000001Evento default                                                                  000000001000000001CR0001244044    2PPM      09450  NPARCELA PM - SET UP PADRÃO                        000000001000000001CR0002244048    2PPM      00200  NPM  SET UP PADRÃO - FUNDO DE CONTINGENCIA         000000001000000001CR0003244049    2PPM      00250  NPM SET UP PADRÃO - TX ADMINISTRATIVA              000000001000000001CR0004244050    2PPM      00100  NPM SET UP PADRÃO - FIGHTING FUND                  000000001000000001DB0001PARMA     2PPM      10000  NPARCELA PM SET UP PADRÃO                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ");
        }

        [TestMethod]
        public void Read_Boolean_With_Format()
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

        }

        [TestMethod]
        public void Read_Boolean_With_True_Format()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(10)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Enabled")
                .SetLength(1)
                .SetConverter(typeof(BooleanConverter))
                .SetConverterFormat("1:T");

            var engine = new FluentFixedEngine(descriptor);

            var items = engine.ReadString("Product 1 T");

            Assert.AreEqual(1, items.Length);
            dynamic item = items[0];
            Assert.AreEqual(true, item.Enabled);

        }

        [TestMethod]
        public void Read_Boolean_With_True_Format_False_Value()
        {
            var descriptor = new FixedRecordDescriptor();

            descriptor.AddField("Name")
                      .SetLength(10)
                      .SetTrimMode(TrimMode.Both);

            descriptor.AddField("Enabled")
                .SetLength(1)
                .SetConverter(typeof(BooleanConverter))
                .SetConverterFormat("1:T");

            var engine = new FluentFixedEngine(descriptor);

            var items = engine.ReadString("Product 1 A");

            Assert.AreEqual(1, items.Length);
            dynamic item = items[0];
            Assert.AreEqual(false, item.Enabled);

        }
    }
}
