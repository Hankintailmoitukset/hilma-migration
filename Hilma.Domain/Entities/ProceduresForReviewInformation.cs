using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;
using Hilma.Domain.Validators;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     VI.4) Procedures for review
    /// </summary>
    [Contract]
    public class ProceduresForReviewInformation
    {
        /// <summary>
        /// Review body
        /// </summary>
        [CorrigendumLabel("appeals_body","VI.4.1")]
        public ContractBodyContactInformation ReviewBody { get; set; }

        /// <summary>
        /// Precise information on deadline(s) for review procedures
        /// </summary>
        [CorrigendumLabel("appeals_lodging","VI.4.3" )]
        //[StringMaxLength(4000)]
        public string[] ReviewProcedure { get; set; }

        public ValidationState ValidationState { get; set; }
    }
}
