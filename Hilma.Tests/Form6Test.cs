// Responsible developer:
// Responsible team:

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hilma.Tests
{
    [TestClass]
    public class Form6Test
    {
        private string formXml = "Form6.xml";
        private string formNumber = "6";

        [TestMethod]
        public void TestForm6()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form6.xml");
            var tedXml = TestHelpers.ValidateFormReturnTedXml("6", null, formOriginalXml);
            Assert.IsNotNull(tedXml);
        }

        [TestMethod]
        public void TestForm6AgreeTopublish()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form6.xml");
            var hilmaDto = TestHelpers.ConvertContract("6", null, formOriginalXml);

            Assert.AreEqual(false, hilmaDto.ObjectDescriptions[0].AwardContract.AwardedContract.FinalTotalValue.DisagreeToBePublished);
        }
    }
}