using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums {
    /// <summary>
    ///     Reasons why there was no competition for the target of the procurement.
    /// </summary>
    [EnumContract]
    public enum ReasonForNoCompetition
    {
        /// <summary>
        ///     Uninitialized
        /// </summary>
        Undefined = 0,

        /// <summary>
        ///     Absence of competition for technical reasons.
        /// </summary>
        DTechnical = 1,

        /// <summary>
        ///     Procurement aiming at the creation or acquisition of a unique work of art or artistic performance.
        /// </summary>
        DArtistic = 2,

        /// <summary>
        ///     Existence of exclusive right.
        /// </summary>
        DExistenceExclusive = 3,

        /// <summary>
        ///     Protection of exclusive rights, including intellectual property rights.
        /// </summary>
        DProtectRights = 4
    }
}
