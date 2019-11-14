using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums {
    /// <summary>
    /// Procedure type for Annex D1.
    /// </summary>
    [EnumContract]
    public enum AnnexProcedureType
    {
        /// <summary>
        /// Uninitialized.
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Procedure in question was open procedure.
        /// </summary>
        DProcOpen = 1,
        /// <summary>
        /// Procedure in question was restricted procedure.
        /// </summary>
        DProcRestricted = 2,
        /// <summary>
        /// negotiated procedure with prior publication of a contract notice
        /// </summary>
        DProcNegotiatedPriorCallCompetition = 3,
        /// <summary>
        /// competitive dialogue
        /// </summary>
        DProcCompetitiveDialogue = 4
    }
}
