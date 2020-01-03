using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// II.2.3) Information about renewals 
    /// </summary>
    [Contract]
    public class DefenceRenewals
    {
        /// <summary>
        ///     If the notice can be renewed after the duration.
        /// </summary>
        [CorrigendumLabel("renewals_subject", "II.2.3")]
        public bool CanBeRenewed { get; set; }

        /// <summary>
        /// If number of renewals is set exactly or by range
        /// </summary>
        [CorrigendumLabel("possible_renewals", "II.2.3")]
        public ValueRangeContract Amount { get; set; }

        /// <summary>
        /// If months or days
        /// </summary>
        public TimeFrame SubsequentContract { get; set;}
    }
}
