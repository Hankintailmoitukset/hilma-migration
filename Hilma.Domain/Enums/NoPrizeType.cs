using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    /// <summary>
    /// V.1)Information on non-award
    /// </summary>
    [EnumContract]
    public enum NoPrizeType : int
    {
        Undefined = 0,

        /// <summary>
        /// No plans or projects were received or all were rejected
        /// </summary>
        AwardNoProjects = 1,

        /// <summary>
        /// Other reasons (discontinuation of procedure)
        /// </summary>
        AwardDiscontinued = 2
    }
}
