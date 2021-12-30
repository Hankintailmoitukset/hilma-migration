using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hilma.Tests
{
    [TestClass]
    public class Form15Test
    {
        private string formXml = "Form15.xml";
        private string formNumber = "15";

        [TestMethod]
        public void TestForm15()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString(formXml);
            var tedXml = TestHelpers.ValidateFormReturnTedXml(formNumber, null, formOriginalXml);
            Assert.IsNotNull(tedXml);

            var hilmaDto = TestHelpers.ConvertContract(formNumber, null, formOriginalXml);
            Assert.IsNull(hilmaDto.HilmaStatistics);
        }
    }
}