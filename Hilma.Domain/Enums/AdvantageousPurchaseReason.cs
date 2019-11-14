using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums {
    /// <summary>
    ///     Specifies type of advantageous purchase.
    /// </summary>
    [EnumContract]
    public enum AdvantageousPurchaseReason
    {
        /// <summary>
        ///     Uninitialized
        /// </summary>
        Undefined = 0,
        /// <summary>
        ///     From a supplier which is definitely winding up its business activities
        /// </summary>
        DFromWindingSupplier = 1,
        /// <summary>
        ///     from the liquidator in an insolvency procedure, an arrangement with creditors or a
        ///     similar procedure under national laws and regulations
        /// </summary>
        DFromReceivers = 2,
    }
}
