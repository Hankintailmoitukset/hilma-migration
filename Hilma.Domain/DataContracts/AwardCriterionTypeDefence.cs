using System;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Directive 2009/81/EC
    ///     Describes the selected awarding criterion type.
    /// </summary>
    [EnumContract]
    [Flags]
    public enum AwardCriterionTypeDefence
    {
        /// <summary>
        ///     Default value, error state.
        /// </summary>
        Undefined = 0,
        /// <summary>
        ///     Lowest price
        /// </summary>
        LowestPrice = 1 << 0,
        /// <summary>
        ///     The most economically advantageous tender in terms of 
        /// </summary>
        EconomicallyAdvantageous = 1 << 1,
        /// <summary>
        ///     the criteria stated below (the award criteria should be given with their weighting or in descending order of importance
        ///     where weighting is not possible for demonstrable reasons)
        /// </summary>
        CriteriaBelow = 1 << 2,
        /// <summary>
        ///     the criteria stated in the specifications, in the invitation to tender or to negotiate or in the descriptive document
        /// </summary>
        CriteriaElsewhere = 1 << 3
    }
}
