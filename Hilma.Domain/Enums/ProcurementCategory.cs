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
        /// <summary>
        /// Default value. EtsApi might or might not set the correct category, if it is evident from the
        /// context. Please avoid using the default value.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Use for notices not belonging to any other category, including directive 2014/24/EU
        /// </summary>
        Public = 1 << 0,

        /// <summary>
        /// 
        /// </summary>
        Defence = 1 << 1,

        /// <summary>
        /// Use for notices regarding directive 2014/25/EU
        /// </summary>
        Utility = 1 << 2,

        /// <summary>
        /// Use for notices regarding directive 2014/23/EU.
        /// </summary>
        Lisence = 1 << 3,

        /// <summary>
        /// Use for agriculture notices
        /// </summary>
        Agriculture = 1 << 4
    }
}
