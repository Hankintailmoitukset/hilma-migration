// Responsible developer:
// Responsible team:

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hilma.Tests
{
    [TestClass]
    public class Form4Test
    {
        [Ignore]
        [TestMethod]
        public void TestForm4()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form4.xml");
            var tedXml = TestHelpers.ValidateFormReturnTedXml("4", null, formOriginalXml);
            Assert.IsNotNull(tedXml);
        }
    }
}