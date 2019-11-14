using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums {
    /// <summary>
    /// 
    /// </summary>
    [EnumContract]
    public enum NoticeDeliveryMethod : int
    {
        /// <summary>
        ///     Default value, error state.
        /// </summary>
        Undefined = 0,

        /// <summary>
        ///     The notice was delivered with enotice.
        /// </summary>
        IcarEnotices = 1,

        /// <summary>
        ///     The notice was delivered with esender.
        /// </summary>
        IcarEsender = 2,

        /// <summary>
        ///     The notice was delivered by other means specified in NonAward.OriginalNoticeSentViaOther.
        /// </summary>
        OtherMeans = 3
    }
}
