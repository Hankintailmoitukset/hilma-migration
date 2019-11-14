using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums {
    /// <summary>
    /// 
    /// </summary>
    [EnumContract]
    public enum ProcurementFailureReason : int
    {
        /// <summary>
        ///     Default value, error state.
        /// </summary>
        Undefined = 0,

        /// <summary>
        ///     Received no tenders or all tenders were rejected.
        /// </summary>
        AwardNoTenders = 1,

        /// <summary>
        ///     The procurement was discontinued.
        /// </summary>
        AwardDiscontinued = 2
    }
}
