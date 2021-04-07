using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;
using System.Collections.Generic;

namespace Hilma.Domain.DataContracts {
    /// <summary>
    ///     Section V: Award of contract
    /// </summary>
    /// <note>
    ///     Lot number is omitted, is parented to the lot in question.
    /// </note>
    [Contract]
    public class Award
    {
        /// <summary>
        ///     Contract has been awarded for this notice.
        /// </summary>
        [CorrigendumLabel("awarded_contract", "V")]
        public ContractAwarded ContractAwarded { get; set; }
        /// <summary>
        ///     If contract has not been awarded, information about the failure.
        /// </summary>
        public NonAward NoAwardedContract { get; set; } = new NonAward();
        /// <summary>
        ///     If contract has been awarded, information about the contracts made.
        /// </summary>
        public ContractAward AwardedContract { get; set; } = new ContractAward();

        public List<ContractAward> AwardedContracts { get; set; } = new List<ContractAward>();
    }
}
