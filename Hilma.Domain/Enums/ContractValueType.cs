using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    /// <summary>
    ///     Determines if value is set to exact value or range.
    /// </summary>
    [EnumContract]
    public enum ContractValueType
    {
        /// <summary>
        /// Not selected
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Exact value
        /// </summary>
        Exact = 1,
        /// <summary>
        /// Value given from with range from lowest to highest value
        /// </summary>
        Range = 2
    }
}
