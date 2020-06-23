// Responsible developer:
// Responsible team:

using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hilma.Tests
{
    [TestClass]
    public class Form2Test
    {
        private string formXml = "Form2.xml";
        private string formNumber = "2";

        [TestMethod]
        public void TestForm2_DurationType()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form2.xml");
            var tedXml = TestHelpers.ValidateFormReturnTedXml("2", null, formOriginalXml);

            var tedDoc = new XmlDocument();
            tedDoc.LoadXml(tedXml);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(tedDoc.NameTable);
            nsmgr.AddNamespace("bk", tedDoc.DocumentElement.NamespaceURI);

            Assert.AreEqual("DAY", tedDoc.SelectSingleNode("//bk:DURATION", nsmgr).Attributes["TYPE"].Value);
        }

        [Ignore]
        [TestMethod]
        public void TestForm2_AddressReviewInfo()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form2.xml");
            var tedXml = TestHelpers.ValidateFormReturnTedXml("2", null, formOriginalXml);

            var tedDoc = new XmlDocument();
            tedDoc.LoadXml(tedXml);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(tedDoc.NameTable);
            nsmgr.AddNamespace("bk", tedDoc.DocumentElement.NamespaceURI);

            Assert.IsNotNull(tedDoc.SelectSingleNode("//bk:ADDRESS_REVIEW_INFO", nsmgr));
        }
    }
}