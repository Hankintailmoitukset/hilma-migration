using System;
using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.DataContracts {
    /// <summary>
    ///     Information on why there was no contract awarded.
    /// </summary>
    [Contract]
    public class NonAward
    {
        /// <summary>
        ///     Why no contract has been awarded
        /// </summary>
        [CorrigendumLabel("no_awarded_contract", "V")]
        public ProcurementFailureReason FailureReason { get; set; }

        /// <summary>
        ///     How was the original notice submitted
        /// </summary>
        public NoticeDeliveryMethod OriginalNoticeSentVia { get; set; }

        /// <summary>
        ///     The information about original e-sender, if OriginalNoticeSentVia=Esender other than Hilma.
        ///     In case of hilma, we kinda know that stuff.
        /// </summary>
        public Esender OriginalEsender { get; set; }

        /// <summary>
        ///     Which other method was used to submit the original notice.
        /// </summary>
        //[MaxLength(200)]
        public string OriginalNoticeSentViaOther { get; set; }

        /// <summary>
        ///     Date of original notice submission.
        /// </summary>
        public DateTime? OriginalNoticeSentDate { get; set; }
    }
}
