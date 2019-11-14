using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Entities.Annexes {
    /// <summary>
    /// Annex D4 for direct purchases
    /// </summary>
    [Contract]
    public class AnnexD4 : IJustifiable
    {
        /// <summary>
        ///     AD4.1.1) Got no valid tenders.
        /// </summary>
        public bool NoTenders { get; set; }

        /// <summary>
        ///     AD3.1.6) The solution is only provided by particular economic operator.
        ///     Not sent to TED, only to toggle visibility of AD1.1.7
        /// </summary>
        public bool ProvidedByOnlyParticularOperator { get; set; }

        /// <summary>
        ///     AD3.1.7) The reason why There was no competition in section AD3.1.6
        /// </summary>
        [CorrigendumLabel("d_can_provided_only", "AD3.1")]
        public ReasonForNoCompetition ReasonForNoCompetition { get; set; }

        /// <summary>
        ///     AD4.3.1) Please explain in a clear and comprehensive manner why the award of the
        ///     contract without prior publication in the Official Journal of the European
        ///     Union is lawful
        /// </summary>
        [CorrigendumLabel("d_explain", "AD4.3")]
        public string[] Justification { get; set; }
    }
}
