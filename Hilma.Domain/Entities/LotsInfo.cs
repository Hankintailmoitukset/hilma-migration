using System.ComponentModel.DataAnnotations;
using Hilma.Domain.Attributes;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Enums;
using Hilma.Domain.Validators;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     Information about partitioning of the notice. On corrigendum notices, none of these fields should be changed.
    /// </summary>
    [Contract]
    public class LotsInfo
    {
        /// <summary>
        ///     If the notice is partitioned.
        /// </summary>
        [Required]
        [CorrigendumLabel("division_lots", "II.1.6")]
        public bool DivisionLots { get; set; }

        /// <summary>
        ///     Into how many partitions the notice is split into. Needs to be at least 2
        ///     and front-end limits it to 999, to avoid performance hits. Seem to work
        ///     with 10000 just fine, but slow. Crashes on 2000000.
        /// </summary>
        public int? QuantityOfLots { get; set; }

        /// <summary>
        ///     Limitation type for tenders concerning how many lots one can cover.
        /// </summary>
        [CorrigendumLabel("lots_submitted_for", "II.1.6")]
        public LotsSubmittedFor LotsSubmittedFor { get; set; }

        /// <summary>
        ///     If the LotsSubmittedFor=LotsMax, the number of lots tender can concern.
        /// </summary>
        [CorrigendumLabel("lots_max", "II.1.6")]
        public int LotsSubmittedForQuantity { get; set; }

        /// <summary>
        ///     Is there a max number of lots awarded for one candidate?
        /// </summary>
        [CorrigendumLabel("lots_max_awarded", "II.1.6")]
        public bool LotsMaxAwarded { get; set; }

        /// <summary>
        ///     If there is a max number of lots awarded for a candidate, the number.
        /// </summary>
        [CorrigendumLabel("lots_max_awarded", "II.1.6")]
        public int LotsMaxAwardedQuantity { get; set; }

        /// <summary>
        ///     If the contracting authority reserves a right to combine lots.
        /// </summary>
        [CorrigendumLabel("lots_combination_possible", "II.1.6")]
        public bool LotCombinationPossible { get; set; }

        /// <summary>
        ///     Free text description describing the possible lot combinations, criteria etc.
        /// </summary>
        [CorrigendumLabel("lots_combination_possible_ca_ce", "II.1.6")]
        [StringMaxLength(1000)]
        public string[] LotCombinationPossibleDescription { get; set; }

        /// <summary>
        ///     Vuejs application form validation state for corresponding section.
        /// </summary>
        public ValidationState ValidationState { get; set; }
    }
}
