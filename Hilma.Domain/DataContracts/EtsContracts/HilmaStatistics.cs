using System;
using System.Collections.Generic;
using System.Text;

namespace Hilma.Domain.DataContracts.EtsContracts
{
    public class HilmaStatistics
    {
        /// <summary>
        /// The procurement takes energy efficiency into consideration
        /// </summary>
        public bool EnergyEfficiencyConsidered { get; set; }

        /// <summary>
        /// The procurement takes innovations into consideration
        /// </summary>
        public bool InnovationConsidered { get; set; }

        /// <summary>
        /// The procurement is inclusive to SMEs.
        /// </summary>
        public bool SMEParticipationConsidered { get; set; }

        #region Hilma statistics update 02012022
        /// <summary>
        /// This procurement promotes low carbon
        /// </summary>
        public bool LowCarbon { get; set; }

        /// <summary>
        /// This acquisition will promote the circular economy
        /// </summary>
        public bool CircularEconomy { get; set; }

        /// <summary>
        /// This acquisition promotes biodiversity
        /// </summary>
        public bool Biodiversity { get; set; }

        /// <summary>
        /// This procurement promotes a sustainable food system
        /// </summary>
        public bool SustainableFoodProduction { get; set; }

        /// <summary>
        /// Whether Motiva, eco-labels or EU GPP criteria are used in the procurement
        /// </summary>
        public bool ListedGreenCriteriaUsed { get; set; }

        /// <summary>
        /// This acquisition promotes fair working conditions
        /// </summary>
        public bool JustWorkingConditions { get; set; }

        /// <summary>
        /// This acquisition takes into account the employment condition
        /// </summary>
        public bool EmploymentCondition { get; set; }

        /// <summary>
        /// How many jobs and apprenticeships are expected to be created by the acquisition?
        /// </summary>
        public int HowManyOpportunitiesIsEstimated { get; set; }

        /// <summary>
        /// This procurement uses the Code of Conduct
        /// </summary>
        public bool CodeOfConduct { get; set; }

        /// <summary>
        /// The solution we are looking for, or part of it, is new to us as a buyer.
        /// </summary>
        public bool SolutionNewToBuyer { get; set; }

        /// <summary>
        /// The solution or part of it sought is new to the market or industry.
        /// </summary>
        public bool SolutionNewToMarketOrIndustry { get; set; }

        /// <summary>
        /// The participation of service users or their representatives in the preparation of the procurement has been taken into account in this procurement.
        /// </summary>
        public bool EndUserInvolved { get; set; }
        #endregion

    }
}
