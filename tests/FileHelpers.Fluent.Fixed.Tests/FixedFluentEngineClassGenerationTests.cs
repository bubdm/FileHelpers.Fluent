using FileHelpers.Fluent.Core;
using FileHelpers.Fluent.Core.Converters;
using FileHelpers.Fluent.Core.Descriptors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using FileHelpers.Fluent.CodeDom;

namespace FileHelpers.Fluent.Fixed.Tests
{
    [TestClass]
    public class FixedFluentEngineClassGenerationTests 
    {
        [TestMethod]
        public void Generate_Single_CSharp_Class()
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

            var exptectedOutput = Encoding.UTF8.GetString(Convert.FromBase64String("Ly8tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0NCi8vIDxhdXRvLWdlbmVyYXRlZD4NCi8vICAgICBUaGlzIGNvZGUgd2FzIGdlbmVyYXRlZCBieSBhIHRvb2wuDQovLw0KLy8gICAgIENoYW5nZXMgdG8gdGhpcyBmaWxlIG1heSBjYXVzZSBpbmNvcnJlY3QgYmVoYXZpb3IgYW5kIHdpbGwgYmUgbG9zdCBpZg0KLy8gICAgIHRoZSBjb2RlIGlzIHJlZ2VuZXJhdGVkLg0KLy8gPC9hdXRvLWdlbmVyYXRlZD4NCi8vLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tDQoNCm5hbWVzcGFjZSBGaWxlSGVscGVycy5UZXN0cw0Kew0KICAgIHVzaW5nIFN5c3RlbTsNCiAgICANCiAgICANCiAgICBwdWJsaWMgc2VhbGVkIGNsYXNzIEN1c3RvbWVyDQogICAgew0KICAgICAgICANCiAgICAgICAgcHVibGljIHN0cmluZyBOYW1lIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIGxvbmcgRG9jIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIERhdGVUaW1lIEJpcnRoRGF0ZSB7IGdldDsgc2V0OyB9DQogICAgfQ0KfQ0K"));

            var output = ((IRecordDescriptor)descriptor).GenerateCSharpClass("Customer", "FileHelpers.Tests");
            
            Assert.AreEqual(exptectedOutput, output);
        }

        [TestMethod]
        public void Generate_Single_VisualBasic_Class()
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

            var exptectedOutput = Encoding.UTF8.GetString(Convert.FromBase64String("Jy0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLQ0KJyA8YXV0by1nZW5lcmF0ZWQ+DQonICAgICBUaGlzIGNvZGUgd2FzIGdlbmVyYXRlZCBieSBhIHRvb2wuDQonDQonICAgICBDaGFuZ2VzIHRvIHRoaXMgZmlsZSBtYXkgY2F1c2UgaW5jb3JyZWN0IGJlaGF2aW9yIGFuZCB3aWxsIGJlIGxvc3QgaWYNCicgICAgIHRoZSBjb2RlIGlzIHJlZ2VuZXJhdGVkLg0KJyA8L2F1dG8tZ2VuZXJhdGVkPg0KJy0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLQ0KDQpPcHRpb24gU3RyaWN0IE9mZg0KT3B0aW9uIEV4cGxpY2l0IE9uDQoNCkltcG9ydHMgU3lzdGVtDQoNCk5hbWVzcGFjZSBGaWxlSGVscGVycy5UZXN0cw0KICAgIA0KICAgIFB1YmxpYyBOb3RJbmhlcml0YWJsZSBDbGFzcyBDdXN0b21lcg0KICAgICAgICANCiAgICAgICAgcHVibGljIHN0cmluZyBOYW1lIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIGxvbmcgRG9jIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIERhdGVUaW1lIEJpcnRoRGF0ZSB7IGdldDsgc2V0OyB9DQogICAgRW5kIENsYXNzDQpFbmQgTmFtZXNwYWNlDQo="));

            var output = ((IRecordDescriptor)descriptor).GenerateVisualBasicClass("Customer", "FileHelpers.Tests");

            Assert.AreEqual(exptectedOutput, output);
        }

        [TestMethod]
        public void Generate_With_Array_CSharp_Class()
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


            var output = ((IRecordDescriptor)descriptor).GenerateCSharpClass("Customer", "FileHelpers.Tests");

            var base64Output = "Ly8tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0NCi8vIDxhdXRvLWdlbmVyYXRlZD4NCi8vICAgICBUaGlzIGNvZGUgd2FzIGdlbmVyYXRlZCBieSBhIHRvb2wuDQovLw0KLy8gICAgIENoYW5nZXMgdG8gdGhpcyBmaWxlIG1heSBjYXVzZSBpbmNvcnJlY3QgYmVoYXZpb3IgYW5kIHdpbGwgYmUgbG9zdCBpZg0KLy8gICAgIHRoZSBjb2RlIGlzIHJlZ2VuZXJhdGVkLg0KLy8gPC9hdXRvLWdlbmVyYXRlZD4NCi8vLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tDQoNCm5hbWVzcGFjZSBGaWxlSGVscGVycy5UZXN0cw0Kew0KICAgIHVzaW5nIFN5c3RlbTsNCiAgICB1c2luZyBTeXN0ZW0uQ29sbGVjdGlvbnMuR2VuZXJpYzsNCiAgICANCiAgICANCiAgICBwdWJsaWMgc2VhbGVkIGNsYXNzIEFycmF5RGF0YUl0ZW0NCiAgICB7DQogICAgICAgIA0KICAgICAgICBwdWJsaWMgc3RyaW5nIERlYWxJZCB7IGdldDsgc2V0OyB9DQogICAgfQ0KICAgIA0KICAgIHB1YmxpYyBzZWFsZWQgY2xhc3MgQ3VzdG9tZXINCiAgICB7DQogICAgICAgIA0KICAgICAgICBwdWJsaWMgc3RyaW5nIEZ1bmN0aW9uIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIGludCBBcnJheVNpemUgeyBnZXQ7IHNldDsgfQ0KICAgICAgICBwdWJsaWMgSUxpc3Q8QXJyYXlEYXRhSXRlbT4gQXJyYXlEYXRhIHsgZ2V0OyBzZXQ7IH0NCiAgICB9DQp9DQo=";
            
            var exptectedOutput = Encoding.UTF8.GetString(Convert.FromBase64String(base64Output));

            Assert.AreEqual(exptectedOutput, output);
        }

        [TestMethod]
        public void Generate_With_Array_VisualBasic_Class()
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


            var output = ((IRecordDescriptor)descriptor).GenerateVisualBasicClass("Customer", "FileHelpers.Tests");

            var base64Output = "Jy0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLQ0KJyA8YXV0by1nZW5lcmF0ZWQ+DQonICAgICBUaGlzIGNvZGUgd2FzIGdlbmVyYXRlZCBieSBhIHRvb2wuDQonDQonICAgICBDaGFuZ2VzIHRvIHRoaXMgZmlsZSBtYXkgY2F1c2UgaW5jb3JyZWN0IGJlaGF2aW9yIGFuZCB3aWxsIGJlIGxvc3QgaWYNCicgICAgIHRoZSBjb2RlIGlzIHJlZ2VuZXJhdGVkLg0KJyA8L2F1dG8tZ2VuZXJhdGVkPg0KJy0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLQ0KDQpPcHRpb24gU3RyaWN0IE9mZg0KT3B0aW9uIEV4cGxpY2l0IE9uDQoNCkltcG9ydHMgU3lzdGVtDQpJbXBvcnRzIFN5c3RlbS5Db2xsZWN0aW9ucy5HZW5lcmljDQoNCk5hbWVzcGFjZSBGaWxlSGVscGVycy5UZXN0cw0KICAgIA0KICAgIFB1YmxpYyBOb3RJbmhlcml0YWJsZSBDbGFzcyBBcnJheURhdGFJdGVtDQogICAgICAgIA0KICAgICAgICBwdWJsaWMgc3RyaW5nIERlYWxJZCB7IGdldDsgc2V0OyB9DQogICAgRW5kIENsYXNzDQogICAgDQogICAgUHVibGljIE5vdEluaGVyaXRhYmxlIENsYXNzIEN1c3RvbWVyDQogICAgICAgIA0KICAgICAgICBwdWJsaWMgc3RyaW5nIEZ1bmN0aW9uIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIGludCBBcnJheVNpemUgeyBnZXQ7IHNldDsgfQ0KICAgICAgICBwdWJsaWMgSUxpc3Q8QXJyYXlEYXRhSXRlbT4gQXJyYXlEYXRhIHsgZ2V0OyBzZXQ7IH0NCiAgICBFbmQgQ2xhc3MNCkVuZCBOYW1lc3BhY2UNCg==";

            var exptectedOutput = Encoding.UTF8.GetString(Convert.FromBase64String(base64Output));

            Assert.AreEqual(exptectedOutput, output);
        }

        [TestMethod]
        public void Generate_With_Multi_Array_CSharp_Class()
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

            var output = ((IRecordDescriptor)descriptor).GenerateCSharpClass("Accounting", "FileHelpers.Tests");
            var base64Output = "Ly8tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0NCi8vIDxhdXRvLWdlbmVyYXRlZD4NCi8vICAgICBUaGlzIGNvZGUgd2FzIGdlbmVyYXRlZCBieSBhIHRvb2wuDQovLw0KLy8gICAgIENoYW5nZXMgdG8gdGhpcyBmaWxlIG1heSBjYXVzZSBpbmNvcnJlY3QgYmVoYXZpb3IgYW5kIHdpbGwgYmUgbG9zdCBpZg0KLy8gICAgIHRoZSBjb2RlIGlzIHJlZ2VuZXJhdGVkLg0KLy8gPC9hdXRvLWdlbmVyYXRlZD4NCi8vLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tDQoNCm5hbWVzcGFjZSBGaWxlSGVscGVycy5UZXN0cw0Kew0KICAgIHVzaW5nIFN5c3RlbTsNCiAgICB1c2luZyBTeXN0ZW0uQ29sbGVjdGlvbnMuR2VuZXJpYzsNCiAgICANCiAgICANCiAgICBwdWJsaWMgc2VhbGVkIGNsYXNzIEFjY291bnRpbmdEYXRhSXRlbQ0KICAgIHsNCiAgICAgICAgDQogICAgICAgIHB1YmxpYyBpbnQgTlVNUEFSQU0geyBnZXQ7IHNldDsgfQ0KICAgICAgICBwdWJsaWMgaW50IE5VTUVWRU5UIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIHN0cmluZyBDT0RUSVBPIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIGludCBOVU1TRVEgeyBnZXQ7IHNldDsgfQ0KICAgICAgICBwdWJsaWMgc3RyaW5nIERFU0NDT05UQSB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBzdHJpbmcgREVTQ0NFTlRSTyB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBkZWNpbWFsIFZMUlBFUkNFTiB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBzdHJpbmcgQ09ETENUTyB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBzdHJpbmcgQ09EQ09QQSB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBzdHJpbmcgVFhUREVTQ1JJQ0FPIHsgZ2V0OyBzZXQ7IH0NCiAgICB9DQogICAgDQogICAgcHVibGljIHNlYWxlZCBjbGFzcyBBY2NvdW50aW5nRXZlbnRJdGVtDQogICAgew0KICAgICAgICANCiAgICAgICAgcHVibGljIGludCBOVU1QQVJBTSB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBpbnQgTlVNRVZFTlQgeyBnZXQ7IHNldDsgfQ0KICAgICAgICBwdWJsaWMgc3RyaW5nIFRYVERFU0NSSUNBTyB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBJTGlzdDxBY2NvdW50aW5nRGF0YUl0ZW0+IEFjY291bnRpbmdEYXRhIHsgZ2V0OyBzZXQ7IH0NCiAgICB9DQogICAgDQogICAgcHVibGljIHNlYWxlZCBjbGFzcyBBY2NvdW50aW5nDQogICAgew0KICAgICAgICANCiAgICAgICAgcHVibGljIHN0cmluZyBNZXNzYWdlQWN0aW9uIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIGludCBOVU1QQVJBTSB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBzdHJpbmcgVFBDT05UUiB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBzdHJpbmcgVFBQQVJNIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIHN0cmluZyBUUENIQVNTSSB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBzdHJpbmcgTlVNQ0RCIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIHN0cmluZyBUUEJPTlVTIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIGludCBOVU1DT05UUklOSSB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBpbnQgTlVNQ09OVFJGSU0geyBnZXQ7IHNldDsgfQ0KICAgICAgICBwdWJsaWMgc3RyaW5nIFRYVERFU0NSSUNBTyB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBJTGlzdDxBY2NvdW50aW5nRXZlbnRJdGVtPiBBY2NvdW50aW5nRXZlbnQgeyBnZXQ7IHNldDsgfQ0KICAgIH0NCn0NCg==";

            var exptectedOutput = Encoding.UTF8.GetString(Convert.FromBase64String(base64Output));

            Assert.AreEqual(exptectedOutput, output);
        }

        [TestMethod]
        public void Generate_With_Multi_Array_VisualBasic_Class()
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

            var output = ((IRecordDescriptor)descriptor).GenerateVisualBasicClass("Accounting", "FileHelpers.Tests");
            var base64Output = "Jy0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLQ0KJyA8YXV0by1nZW5lcmF0ZWQ+DQonICAgICBUaGlzIGNvZGUgd2FzIGdlbmVyYXRlZCBieSBhIHRvb2wuDQonDQonICAgICBDaGFuZ2VzIHRvIHRoaXMgZmlsZSBtYXkgY2F1c2UgaW5jb3JyZWN0IGJlaGF2aW9yIGFuZCB3aWxsIGJlIGxvc3QgaWYNCicgICAgIHRoZSBjb2RlIGlzIHJlZ2VuZXJhdGVkLg0KJyA8L2F1dG8tZ2VuZXJhdGVkPg0KJy0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLQ0KDQpPcHRpb24gU3RyaWN0IE9mZg0KT3B0aW9uIEV4cGxpY2l0IE9uDQoNCkltcG9ydHMgU3lzdGVtDQpJbXBvcnRzIFN5c3RlbS5Db2xsZWN0aW9ucy5HZW5lcmljDQoNCk5hbWVzcGFjZSBGaWxlSGVscGVycy5UZXN0cw0KICAgIA0KICAgIFB1YmxpYyBOb3RJbmhlcml0YWJsZSBDbGFzcyBBY2NvdW50aW5nRGF0YUl0ZW0NCiAgICAgICAgDQogICAgICAgIHB1YmxpYyBpbnQgTlVNUEFSQU0geyBnZXQ7IHNldDsgfQ0KICAgICAgICBwdWJsaWMgaW50IE5VTUVWRU5UIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIHN0cmluZyBDT0RUSVBPIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIGludCBOVU1TRVEgeyBnZXQ7IHNldDsgfQ0KICAgICAgICBwdWJsaWMgc3RyaW5nIERFU0NDT05UQSB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBzdHJpbmcgREVTQ0NFTlRSTyB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBkZWNpbWFsIFZMUlBFUkNFTiB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBzdHJpbmcgQ09ETENUTyB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBzdHJpbmcgQ09EQ09QQSB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBzdHJpbmcgVFhUREVTQ1JJQ0FPIHsgZ2V0OyBzZXQ7IH0NCiAgICBFbmQgQ2xhc3MNCiAgICANCiAgICBQdWJsaWMgTm90SW5oZXJpdGFibGUgQ2xhc3MgQWNjb3VudGluZ0V2ZW50SXRlbQ0KICAgICAgICANCiAgICAgICAgcHVibGljIGludCBOVU1QQVJBTSB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBpbnQgTlVNRVZFTlQgeyBnZXQ7IHNldDsgfQ0KICAgICAgICBwdWJsaWMgc3RyaW5nIFRYVERFU0NSSUNBTyB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBJTGlzdDxBY2NvdW50aW5nRGF0YUl0ZW0+IEFjY291bnRpbmdEYXRhIHsgZ2V0OyBzZXQ7IH0NCiAgICBFbmQgQ2xhc3MNCiAgICANCiAgICBQdWJsaWMgTm90SW5oZXJpdGFibGUgQ2xhc3MgQWNjb3VudGluZw0KICAgICAgICANCiAgICAgICAgcHVibGljIHN0cmluZyBNZXNzYWdlQWN0aW9uIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIGludCBOVU1QQVJBTSB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBzdHJpbmcgVFBDT05UUiB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBzdHJpbmcgVFBQQVJNIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIHN0cmluZyBUUENIQVNTSSB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBzdHJpbmcgTlVNQ0RCIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIHN0cmluZyBUUEJPTlVTIHsgZ2V0OyBzZXQ7IH0NCiAgICAgICAgcHVibGljIGludCBOVU1DT05UUklOSSB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBpbnQgTlVNQ09OVFJGSU0geyBnZXQ7IHNldDsgfQ0KICAgICAgICBwdWJsaWMgc3RyaW5nIFRYVERFU0NSSUNBTyB7IGdldDsgc2V0OyB9DQogICAgICAgIHB1YmxpYyBJTGlzdDxBY2NvdW50aW5nRXZlbnRJdGVtPiBBY2NvdW50aW5nRXZlbnQgeyBnZXQ7IHNldDsgfQ0KICAgIEVuZCBDbGFzcw0KRW5kIE5hbWVzcGFjZQ0K";

            var exptectedOutput = Encoding.UTF8.GetString(Convert.FromBase64String(base64Output));

            Assert.AreEqual(exptectedOutput, output);
        }
    }
}
