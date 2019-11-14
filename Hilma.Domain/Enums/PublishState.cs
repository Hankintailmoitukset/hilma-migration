using System;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    [Flags]
    [EnumContract]
    public enum PublishState
    {
        Undefined = 0 << 0,
        Draft = 1 << 0,
        Published = 1 << 1,
        WaitingToBePublished = 1 << 2
    }
}
