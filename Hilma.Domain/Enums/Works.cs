using System;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    /// <summary>
    /// Directive 2009/81/EC (Defence contract)
    /// When ContractType = Works
    /// </summary>
    [EnumContract]
    [Flags]
    public enum Works : int
    {
        Undefined = 0,
        Execution = 1 << 0,
        Design = 1 << 1,
        Realisation = 1 << 2
    }
}
