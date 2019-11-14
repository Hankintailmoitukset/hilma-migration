using System;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    [EnumContract]
    [Flags]
    public enum ValidationState : int
    {
        Pristine = 0,
        Invalid = 1 << 0,
        Valid = 1 << 1,
        // readonly
        Forbidden = 1 << 2
    }
}
