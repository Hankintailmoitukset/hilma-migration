// Responsible developer:
// Responsible team:

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hilma.Tests
{
    [TestClass]
    public class Form21Test
    {
        [TestMethod]
        public void TestForm21()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form21.xml");
            var tedXml = TestHelpers.ValidateFormReturnTedXml("21", "contract", formOriginalXml);
            Assert.IsNotNull(tedXml);
        }
    }
}