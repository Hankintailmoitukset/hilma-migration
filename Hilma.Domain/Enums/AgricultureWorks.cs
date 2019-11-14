using System;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    /// <summary>
    /// works used in national agriculture contract notices
    /// </summary>
    [EnumContract]
    [Flags]
    public enum AgricultureWorks : int
    {
        Undefined = 0,
        NewConstruction = 1 << 0,
        Expansion= 1 << 1,
        BasicRepair = 1 << 2,
    }
}
