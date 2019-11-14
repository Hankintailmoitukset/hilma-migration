using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums {
    [EnumContract]
    public enum NationalDirectPurchaseType
    {
        /// <summary>
        /// Uninitialized
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Direct purchase that needs to be justified
        /// </summary>
        JustifiableDirectPurchase = 1,
        /// <summary>
        /// Purchase
        /// </summary>
        OutsideOfScope = 2
    }
}
