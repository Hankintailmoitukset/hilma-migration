using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     VII.2) Information about modifications
    /// </summary>
    [Contract]
    public class Modifications
    {
        /// <summary>
        /// Description of the modifications
        /// Nature and extent of the modifications (with indication of possible earlier changes to the contract)
        /// </summary>
        [CorrigendumLabel("mod_descr_mod", "VII.2.1")]
        public string[] Description { get; set; }

        /// <summary>
        /// Affected lot number. The lot number will fill in II.2) Description and Section V: Award of contract/concession
        /// </summary>
        public int AffectedLot { get; set; }

        /// <summary>
        /// Reasons for modification
        /// </summary>
        [CorrigendumLabel("mod_reason", "VII.2.2" )]
        public ModificationReason Reason { get; set; }

        /// <summary>
        /// Description of the economic or technical reasons and the inconvenience or duplication of cost preventing a change of contractor
        /// </summary>
        [CorrigendumLabel("mod_descr_prevent_change", "VII.2.2")]
        public string[] ReasonDescriptionEconomic { get; set; }

        /// <summary>
        /// Description of the circumstances which rendered the modification necessary and explanation of the unforeseen nature of these circumstances
        /// </summary>
        [CorrigendumLabel("mod_descr_circums", "VII.2.2")]
        public string[] ReasonDescriptionCircumstances { get; set; }

        /// <summary>
        /// VII.2.3) Increase in price
        /// Updated total contract value before the modifications
        /// </summary>
        [CorrigendumLabel("mod_reason", "VII.2.3")]
        public ValueContract IncreaseBeforeModifications { get; set; } = new ValueContract();

        /// <summary>
        /// VII.2.3) Increase in price
        /// Total contract value after the modifications
        /// </summary>
        [CorrigendumLabel("mod_reason", "VII.2.3")]
        public ValueContract IncreaseAfterModifications { get; set; } = new ValueContract();

        public ValidationState ValidationState { get; set; }
    }
}
