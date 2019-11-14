using System;

namespace Hilma.Domain.DataContracts.EtsContracts {
    /// <summary>
    ///     Information about TED publication process, if successful available.
    /// </summary>
    public class EtsTedPublicationInfo
    {
        /// <summary>
        ///     Map TED model into human readable.
        /// </summary>
        /// <param name="ted">TED model to extract information from.</param>
        public EtsTedPublicationInfo(TedPublicationInfo ted)
        {
            OrderNumberInSeries = ted.Ojs_number;
            DocumentNumber = ted.No_doc_ojs;
            PublicationDate = ted.Publication_date;
            Links = ted.Ted_links;
        }

        /// <summary>
        ///     Document identifier.
        /// </summary>
        public string DocumentNumber { get; set; }
        /// <summary>
        ///     Links to different language versions of TED notice.
        /// </summary>
        public TedLinks Links { get; set; }
        /// <summary>
        ///     In TED, publications are grouped in certain way, this is the order number
        ///     in that group, most often it is just "001"
        /// </summary>
        public string OrderNumberInSeries { get; set; }
        /// <summary>
        ///     Moment of publication in TED
        /// </summary>
        public DateTime PublicationDate { get; set; }
        /// <summary>
        ///     Moment the request for publish arrived to TED
        /// </summary>
        public DateTime? PublicationRequestedDate { get; set; }
    }
}
