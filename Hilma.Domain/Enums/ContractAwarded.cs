using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums {
    /// <summary>
    ///     Is a contract awarded.
    /// </summary>
    /// <remarks>
    ///     Boolean would be better for storing the value, but we want a radio
    ///     group for UX reasons, since both options open some new fields.
    /// </remarks>
    [EnumContract]
    public enum ContractAwarded : int
    {
        /// <summary>
        ///     Default value, error state.
        /// </summary>
        Undefined = 0,

        /// <summary>
        ///     Contract has been awarded / true
        /// </summary>
        AwardedContract = 1,

        /// <summary>
        ///     Contract has not been awarded / false
        /// </summary>
        NoAwardedContract = 2,
    }
}
