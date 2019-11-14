using System;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    [EnumContract]
    [Flags]
    public enum ModificationReason : int
    {
        Undefined = 0,
        ModNeedForAdditional = 1 << 0,
        ModNeedByCircums = 1 << 1
    }
}
