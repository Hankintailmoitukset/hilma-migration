using Hilma.Domain.Attributes;
using Hilma.Domain.Validators;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Describes EU-funding for this tender.
    /// </summary>
    [Contract]
    public class EuFunds
    {
        /// <summary>
        ///     If EU funds are going to be used.
        /// </summary>
        [CorrigendumLabel("eu_progr_related", "II.2.13")]
        public bool ProcurementRelatedToEuProgram { get; set; }
        /// <summary>
        ///     EU funding project number. Applicable if EU funds are used.
        /// </summary>
        [CorrigendumLabel("eu_progr_ref", "II.2.13")]
        [StringMaxLength(400)]
        public string[] ProjectIdentification { get; set; }
    }
}
