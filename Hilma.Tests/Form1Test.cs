// Responsible developer:
// Responsible team:

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hilma.Tests
{
    [TestClass]
    public class Form1Test
    {
        private string formXml = "Form1.xml";
        private string formNumber = "1";

        [TestMethod]
        public void TestForm1()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString("Form1.xml");
            var tedXml = TestHelpers.ValidateFormReturnTedXml("1", null, formOriginalXml);
            Assert.IsNotNull(tedXml);
        }
    }
}