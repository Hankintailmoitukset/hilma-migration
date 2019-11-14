using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// Contract Award required fields that are not to be published
    /// </summary>
    [Contract]
    public class ContractAwardNotPublicFields
    {
        #region 2.8 Country of origin of the product or service - contract award utilities
        /// <summary>
        /// 2.8 Country of origin (EU)
        /// </summary>
        [CorrigendumLabel("origin_community", "V.2.8.1")]
        public bool CommunityOrigin { get; set; }
        /// <summary>
        /// 2.8 Country of origin (Non-EU)
        /// </summary>
        [CorrigendumLabel("nonCommunityOrigin", "V.2.8.2")]
        public bool NonCommunityOrigin { get; set; }
        /// <summary>
        /// 2.8 List of countries of origin (Non-EU)
        /// </summary>
        [CorrigendumLabel("nonCommunityOrigin", "V.2.8.3")]
        public string[] Countries { get; set; }
        #endregion

        /// <summary>
        ///      The contract was awarded to a tenderer who submitted a variant 
        /// </summary>
        [CorrigendumLabel("award_submitted_variant", "V.2.9")]
        public bool AwardedToTendererWithVariant { get; set; }

        /// <summary>
        ///      Tenders were excluded on the ground that they were abnormally low
        /// </summary>
        [CorrigendumLabel("abnormallyLowTendersExcluded", "V.2.10")]
        public bool AbnormallyLowTendersExcluded { get; set; }
    }
}
