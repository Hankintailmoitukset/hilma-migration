using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts {
    /// <summary>
    ///     Contains random defense specific subcontracting fields.
    /// </summary>
    [Contract]
    public class ExAnteSubcontracting
    {
        /// <summary>
        /// All or certain subcontracts will be awarded through a competitive procedure (see Title III of Directive 2009/81/EC)
        /// </summary>
        [CorrigendumLabel("subcontr_all_competitive", "V.2.5")]
        public bool AllOrCertainSubcontractsWillBeAwarded { get; set; }

        /// <summary>
        /// A share of the contract will be subcontracted through a competitive procedure (see Title III of Directive 2009/81/EC)
        /// </summary>
        [CorrigendumLabel("subcontr_share_competitive", "V.2.5")]
        public bool ShareOfContractWillBeSubcontracted { get; set; }

        /// <summary>
        /// Minimum percentage
        /// </summary>
        [CorrigendumLabel("min_percentage", "V.2.5")]
        public decimal? ShareOfContractWillBeSubcontractedMinPercentage { get; set; }

        /// <summary>
        /// Maximum percentage
        /// </summary>
        [CorrigendumLabel("max_percentage", "V.2.5")]
        public decimal? ShareOfContractWillBeSubcontractedMaxPercentage { get; set; }
    }
}
