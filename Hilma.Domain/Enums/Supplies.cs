using System;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    /// <summary>
    /// Directive 2009/81/EC (Defence contract)
    /// When ContractType = Supplies
    /// </summary>
    [EnumContract]
    [Flags]
    public enum Supplies : int
    {
        Undefined = 0,
        Purchase = 1 << 0,
        Lease = 1 << 1,
        Rental = 1 << 2,
        HirePurchase = 1 << 3,
        Combination = 1 << 4
    }
}
