// Responsible developer:
// Responsible team:

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hilma.Tests
{
    [TestClass]
    public class Form3Test
    {
        [TestMethod]
        public void TestForm3()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form3.xml");
            var tedXml = TestHelpers.ValidateFormReturnTedXml("3", null, formOriginalXml);
            Assert.IsNotNull(tedXml);
        }

        [TestMethod]
        public void TestForm3AgreeTopublish()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form3.xml");
            var hilmaDto = TestHelpers.ConvertContract("3", null, formOriginalXml);

            Assert.AreEqual(false, hilmaDto.ObjectDescriptions[0].AwardContract.AwardedContract.FinalTotalValue.DisagreeToBePublished);
        }
    }
}