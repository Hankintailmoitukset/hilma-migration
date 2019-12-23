using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{

    /// <summary>
    ///     Set of criteria for awarding the tender.
    /// </summary>
    [Contract]
    public class AwardCriteriaDefence
    {
        /// <summary>
        ///     Type of criteria selected for this tender.
        /// </summary>
        [CorrigendumLabel("award_criteria", "IV.2.1")]
        public AwardCriterionTypeDefence CriterionTypes { get; set; }

        /// <summary>
        ///     When CriterionTypes = EconomicallyAdvantageous
        /// </summary>
        [CorrigendumLabel("award_criteria", "IV.2.1")]
        public AwardCriterionTypeDefence EconomicCriteriaTypes { get; set; }

        /// <summary>
        ///     Award criteria 
        /// </summary>
        public AwardCriterionDefinition[] Criteria { get; set; } = new AwardCriterionDefinition[0];
    }
}
