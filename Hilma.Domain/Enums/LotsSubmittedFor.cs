using System;
using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;

namespace Hilma.Domain.Enums
{
    /// <summary>
    ///     Describes limitations on how many lots a tender can be for.
    /// </summary>
    [EnumContract]
    [Flags]
    public enum LotsSubmittedFor : int
    {
        /// <summary>
        ///     Default value, error state.
        /// </summary>
        Undefined = 0,
        /// <summary>
        ///     No limitation.
        /// </summary>
        LotsAll = 1 << 0,
        /// <summary>
        ///     Only one lot.
        /// </summary>
        LotOneOnly = 1 << 1,
        /// <summary>
        ///     Max number of lots defined by <see cref="LotsInfo"/>.LotsSubmittedForQuantity
        /// </summary>
        LotsMax = 1 << 2,
    }
}
