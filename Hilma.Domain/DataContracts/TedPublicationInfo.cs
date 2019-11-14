using Hilma.Domain.Attributes;
using System;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Maps to publication status information sent out by TED.
    /// </summary>
    [Contract]
    public class TedPublicationInfo
    {
        /// <summary>
        ///     Incrementing issue number. Each procurement groups all
        ///     related notices together and this acts as an order number
        ///     for those.
        ///
        ///     Internally this is TED's Open Journal System enforced thing.
        /// </summary>
        public string Ojs_number { get; set; }
        /// <summary>
        ///     Uniquely identifies the notice with the current user information.
        /// </summary>
        public string No_doc_ojs { get; set; }
        /// <summary>
        ///     Date of publication to TED.
        /// </summary>
        public DateTime Publication_date { get; set; }
        /// <summary>
        ///     Links to published notice in various languages.
        /// </summary>
        public TedLinks Ted_links { get; set; }
    }
}
