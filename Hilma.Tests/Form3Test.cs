// Responsible developer:
// Responsible team:

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hilma.Tests
{
    [TestClass]
    public class Form3Test
    {
        [Ignore]
        [TestMethod]
        public void TestForm3()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form3.xml");
            var tedXml = TestHelpers.ValidateFormReturnTedXml("3", null, formOriginalXml);
            Assert.IsNotNull(tedXml);
        }
    }
}