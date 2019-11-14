using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Describes how what type of candidate number restriction is planned for.
    /// </summary>
    [EnumContract]
    public enum EnvisagedParticipantsOptions
    {
        /// <summary>
        ///     No limitation.
        /// </summary>
        Undefined = 0,
        /// <summary>
        ///     Number of candidates is planned to be exactly some number.
        /// </summary>
        EnvisagedNumber = 1,
        /// <summary>
        ///     Number of candidates is planned to be a number range.
        /// </summary>
        Range = 2
    }
}
