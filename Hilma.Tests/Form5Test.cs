// Responsible developer:
// Responsible team:

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hilma.Tests
{
    [TestClass]
    public class Form5Test
    {
        [Ignore]
        [TestMethod]
        public void TestForm5()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form5.xml");
            var tedXml = TestHelpers.ValidateFormReturnTedXml("5", null, formOriginalXml);
            Assert.IsNotNull(tedXml);
        }
    }
}