using System;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Describes the selected awarding criterion type.
    /// </summary>
    [EnumContract]
    [Flags]
    public enum AwardCriterionType
    {
        /// <summary>
        ///     Default value, error state.
        /// </summary>
        Undefined = 0,
        /// <summary>
        ///     Tender is awarded based on quality criterion. Not valid individually.
        /// </summary>
        QualityCriterion = 1 << 0,
        /// <summary>
        ///     Cost criteria means supplier cost plus margin.
        /// </summary>
        CostCriterion = 1 << 1,
        /// <summary>
        ///     Cost and quality criteria mixed.
        /// </summary>
        CostAndQualityCriteria = CostCriterion | QualityCriterion,
        /// <summary>
        ///     Fixed list-price criteria.
        /// </summary>
        PriceCriterion = 1 << 2,
        /// <summary>
        ///     Price and quality criteria mixed.
        /// </summary>
        PriceAndQualityCriteria = PriceCriterion | QualityCriterion,
        /// <summary>
        ///     Other criteria described in clear text.
        /// </summary>
        DescriptiveCriteria = 1 << 3,
        /// <summary>
        ///     Lowest price, for defense ex-ante notices
        /// </summary>
        LowestPrice = 1 << 4,
        /// <summary>
        ///     Economically most advantageous, for defense ex-ante notices
        /// </summary>
        EconomicallyAdvantageous = 1 << 5,
        /// <summary>
        /// the criteria stated in the procurement documents
        /// Used in concessions
        /// </summary>
        AwardCriteriaInDocs = 1 << 6,
        /// <summary>
        /// the criteria described below
        /// Used in concessions
        /// </summary>
        AwardCriteriaDescrBelow = 1 << 7
    }
}
