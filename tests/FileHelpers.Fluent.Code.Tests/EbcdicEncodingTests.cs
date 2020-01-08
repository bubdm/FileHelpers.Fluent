using FileHelpers.Fluent.Core.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace FileHelpers.Fluent.Code.Tests
{
    [TestClass]
    public class EbcdicEncodingTests
    {
        [TestMethod]
        public void List_All_Names()
        {
            var names = EbcdicEncoding.AllNames;

            Assert.IsNotNull(names);
            Assert.AreEqual(46, names.Length);
        }

        [TestMethod]
        public void Retrieve_EBCDIC_CP_US()
        {
            var encoding = EbcdicEncoding.EBCDIC_CP_US;

            Assert.IsNotNull(encoding);
        }
    }
}
