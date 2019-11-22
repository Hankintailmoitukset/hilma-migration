// Responsible developer:
// Responsible team:

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hilma.Tests
{
    [TestClass]
    public class Form6Test
    {
        [TestMethod]
        public void TestForm6()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString($"Form6.xml");
            var tedXml = TestHelpers.ValidateFormReturnTedXml("6", null, formOriginalXml);
            Assert.IsNotNull(tedXml);
        }
    }
}