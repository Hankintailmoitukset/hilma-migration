using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Describes one awarding criterion.
    /// </summary>
    [Contract]
    public class AwardCriterionDefinition
    {
        /// <summary>
        ///     Free text description of criterion.
        /// </summary>
        /// <example>Monthly license fees</example>
        [CorrigendumLabel("award_criterion_name", "II.2.5")]
        //[StringMaxLength(1000)]
        public string Criterion { get; set; }
        /// <summary>
        ///     Free text description of weight.
        /// </summary>
        /// <example>33%</example>
        [CorrigendumLabel("award_criterion_weight", "II.2.5")]
        //[StringMaxLength(30)]
        public string Weighting { get; set; }
    }
}
