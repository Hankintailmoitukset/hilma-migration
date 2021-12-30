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
            Assert.IsTrue(hilmaDto.HilmaStatistics.EnergyEfficiencyConsidered);
            Assert.IsFalse(hilmaDto.HilmaStatistics.InnovationConsidered);
            Assert.IsTrue(hilmaDto.HilmaStatistics.SMEParticipationConsidered);
            Assert.IsFalse(hilmaDto.HilmaStatistics.LowCarbon);
            Assert.IsTrue(hilmaDto.HilmaStatistics.CircularEconomy);
            Assert.IsFalse(hilmaDto.HilmaStatistics.Biodiversity);
            Assert.IsTrue(hilmaDto.HilmaStatistics.SustainableFoodProduction);
            Assert.IsFalse(hilmaDto.HilmaStatistics.ListedGreenCriteriaUsed);
            Assert.IsTrue(hilmaDto.HilmaStatistics.JustWorkingConditions);
            Assert.IsFalse(hilmaDto.HilmaStatistics.EmploymentCondition);
            Assert.AreEqual(2, hilmaDto.HilmaStatistics.HowManyOpportunitiesIsEstimated);
            Assert.IsTrue(hilmaDto.HilmaStatistics.CodeOfConduct);
            Assert.IsFalse(hilmaDto.HilmaStatistics.SolutionNewToBuyer);
            Assert.IsTrue(hilmaDto.HilmaStatistics.SolutionNewToMarketOrIndustry);
            Assert.IsFalse(hilmaDto.HilmaStatistics.EndUserInvolved);
        }
    }
}