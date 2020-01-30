// Responsible developer:
// Responsible team:

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
    }
}