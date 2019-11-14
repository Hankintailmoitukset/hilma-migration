using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Set of criteria for awarding the tender.
    /// </summary>
    [Contract]
    public class AwardCriteria
    {
        /// <summary>
        ///     Type of criteria selected for this tender.
        /// </summary>
        [CorrigendumLabel("award_criteria", "II.2.5")]
        public AwardCriterionType CriterionTypes { get; set; }
        /// <summary>
        ///     Selected criteria configuration related to quality. Applicable if CriterionTypes.HasFlag(QualityCriterion).
        /// </summary>
        [CorrigendumLabel("quality_criterion", "II.2.5")]
        public AwardCriterionDefinition[] QualityCriteria { get; set; } = new AwardCriterionDefinition[0];
        /// <summary>
        ///     Selected criteria configuration related to cost. Applicable if CriterionTypes.HasFlag(CostCriteria)
        /// </summary>
        [CorrigendumLabel("cost_criterion", "II.2.5")]
        public AwardCriterionDefinition[] CostCriteria { get; set; } = new AwardCriterionDefinition[0];

        /// <summary>
        ///     Price related criterion configured. Applicable if CriterionTypes.HasFlag(PriceCriterion)
        /// </summary>
        [CorrigendumLabel("price_criterion", "II.2.5")]
        public AwardCriterionDefinition PriceCriterion { get; set; } = new AwardCriterionDefinition();

        /// <summary>
        ///     Criterion given in descending order of importance.
        ///     For F15 ex-ante, when dealing with licenses. Might be useful for license notice too.
        /// </summary>
        [CorrigendumLabel("H_award_criteria", "II.2.5")]
        public string[] Criterion { get; set; } = new string[] {};

        /// <summary>
        /// If selected, the criteria is defined only in procurement documents. Only only for national notices
        /// </summary>
        public bool CriteriaStatedInProcurementDocuments { get; set; }

    }
}
