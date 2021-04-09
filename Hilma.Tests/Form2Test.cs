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

        [TestMethod]
        public void TestForm2_Nuts2021()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form2_nuts2021.xml");
            var notice = TestHelpers.ConvertContract("2", null, formOriginalXml);

            Assert.AreEqual("DK", notice.Organisation.Information.NutsCodes[0]);
        }

        [TestMethod]
        public void TestForm2_description_procurement_and_short()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form2_nuts2021.xml");
            Domain.DataContracts.EtsContracts.EtsNoticeContract notice = TestHelpers.ConvertContract("2", null, formOriginalXml);

            Assert.AreEqual("ghgh", notice.ObjectDescriptions[0].DescrProcurement[0]);
            Assert.AreEqual("ghgh", notice.ShortDescription[0]);
        }
    }
}