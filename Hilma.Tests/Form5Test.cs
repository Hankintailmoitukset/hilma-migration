// Responsible developer:
// Responsible team:

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hilma.Tests
{
    [TestClass]
    public class Form5Test
    {
        private string formXml = "Form5.xml";
        private string formNumber = "5";

        [TestMethod]
        public void TestForm5()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString(formXml);
            var tedXml = TestHelpers.ValidateFormReturnTedXml(formNumber, null, formOriginalXml);
            Assert.IsNotNull(tedXml);
        }

        [TestMethod]
        public void TestForm5Statistics()
        {
            var formOriginalXml = TestHelpers.GetEmbeddedResourceAsString(formXml);
            var hilmaDto = TestHelpers.ConvertContract(formNumber, null, formOriginalXml);

            Assert.IsNotNull(hilmaDto.HilmaStatistics);
            Assert.IsFalse(hilmaDto.HilmaStatistics.EnergyEfficiencyConsidered);
            Assert.IsFalse(hilmaDto.HilmaStatistics.InnovationConsidered);
            Assert.IsTrue(hilmaDto.HilmaStatistics.SMEParticipationConsidered);
        }
    }
}