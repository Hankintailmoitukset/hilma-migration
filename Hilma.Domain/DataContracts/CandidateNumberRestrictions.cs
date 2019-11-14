using Hilma.Domain.Attributes;
using Hilma.Domain.Validators;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Describes number of candidates to be invited.
    /// </summary>
    [Contract]
    public class CandidateNumberRestrictions    
    {
        /// <summary>
        ///     Exact number of candidates planned to be selected. Applicable if Selected = EnvisagedNumber
        /// </summary>
        [CorrigendumLabel("envisaged_number", "II.2.9")]
        public int EnvisagedNumber { get; set; }

        /// <summary>
        ///     Lower bound of range of candidates planned to be selected. Applicable if Selected = Range
        /// </summary>
        [CorrigendumLabel("envisaged_min", "II.2.9")]
        public int EnvisagedMinimumNumber { get; set; }

        /// <summary>
        ///     Upper bound of range of candidates planned to be selected. Applicable if Selected = Range
        /// </summary>
        [CorrigendumLabel("max_number", "II.2.9")]
        public int EnvisagedMaximumNumber { get; set; }

        /// <summary>
        ///     Free text description of candidate number of selection criteria.
        /// </summary>
        [CorrigendumLabel("criteria_choosing_limited", "II.2.9")]
        [StringMaxLength(6000)]

        public string[] ObjectiveCriteriaForChoosing{ get; set; }

        /// <summary>
        ///     Type of restriction for this tender.
        /// </summary>
        public EnvisagedParticipantsOptions Selected { get; set; }
    }
}
