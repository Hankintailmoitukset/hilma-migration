// Responsible developer:
// Responsible team:

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hilma.Tests
{
    [TestClass]
    public class Form21Test
    {
        private string formXml = "Form21.xml";
        private string formNumber = "21";

        [TestMethod]
        public void TestForm21()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form21.xml");
            var tedXml = TestHelpers.ValidateFormReturnTedXml("21", "contract", formOriginalXml);
            Assert.IsNotNull(tedXml);
        }

        [TestMethod]
        public void TestForm21_2()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form21_2.xml");
            var tedXml = TestHelpers.ValidateFormReturnTedXml("21", "contract", formOriginalXml);
            Assert.IsNotNull(tedXml);
        }
    }
}