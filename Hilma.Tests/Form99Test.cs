// Responsible developer:
// Responsible team:

using Hilma.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hilma.Tests
{
    [TestClass]
    public class Form99Test
    {
        [TestMethod]
        public void TestForm99()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form99.xml");
            var tedXml = TestHelpers.ValidateFormReturnTedXml("99", null, formOriginalXml, false);
        }

        [TestMethod]
        public void TestForm99Cancellation()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form99_cancellation.xml");
            var hilmaDto = TestHelpers.ConvertContract("99", "procurement_discontinued", formOriginalXml);

            Assert.IsTrue(hilmaDto.IsCancelled, "HilmaDto is not set to IsCancelled");
            Assert.AreEqual("112014", hilmaDto.PreviousNoticeOjsNumber, "Hilma dto dosn't have ojs number");
            Assert.AreEqual("dfdfdf", hilmaDto.CancelledReason?[0]);
            Assert.AreEqual(NoticeType.NationalContract, hilmaDto.Type);
        }

        [TestMethod]
        public void TestForm99Amendment()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form99_amendment.xml");
            var tedXml = TestHelpers.ValidateFormReturnTedXml("99", null, formOriginalXml, false);
            Domain.DataContracts.EtsContracts.EtsNoticeContract hilmaDto = TestHelpers.ConvertContract("99", "corrigendum_notice", formOriginalXml);

            Assert.IsTrue(hilmaDto.IsCorrigendum, "HilmaDto is not set to IsCancelled");
            Assert.AreEqual("43182", hilmaDto.PreviousNoticeOjsNumber, "Hilma dto dosn't have ojs number");
            Assert.AreEqual(NoticeType.NationalContract, hilmaDto.Type);

        }
    }
}