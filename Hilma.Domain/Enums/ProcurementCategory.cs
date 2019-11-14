using System;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums {
    /// <summary>
    ///     Defined the domain of the procurement, used to select
    ///     correct form type and directive, when creating notices
    /// </summary>
    [EnumContract]
    [Flags]
    public enum ProcurementCategory
    {
        Undefined = 0,
        Public = 1 << 0,
        Defence = 1 << 1,
        Utility = 1 << 2,
        Lisence = 1 << 3,
        Agriculture = 1 << 4
    }
}
