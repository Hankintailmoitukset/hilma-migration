using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts {
    /// <summary>
    ///     Statistics about number of tenders received.
    /// </summary>
    [Contract]
    public class NumberOfTenders
    {
        /// <summary>
        ///     If the information in this section is confidential and should not be published on TED, it must be indicated by selecting false.
        /// </summary>
        [CorrigendumLabel("H_disagree_to_publish", "V.2.2")]
        public bool DisagreeTenderInformationToBePublished { get; set; }
        /// <summary>
        ///     Total number of tenders received.
        /// </summary>
        [CorrigendumLabel("offers_received", "V.2.2")]
        public int Total { get; set; }

        /// <summary>
        ///     Number of tenders received from SMEs
        ///     (SME – as defined in Commission Recommendation 2003/361/EC)
        /// </summary>
        [CorrigendumLabel("nb_tenders_received_sme", "V.2.2")]
        public int? Sme { get; set; }

        /// <summary>
        ///     Tenders received from other eu states.
        /// </summary>
        [CorrigendumLabel("nb_tenders_received_other_eu", "V.2.2")]
        public int? OtherEu { get; set; }

        /// <summary>
        ///     Tenders received from non-eu states.
        /// </summary>
        [CorrigendumLabel("nb_tenders_received_non_eu", "V.2.2")]
        public int? NonEu { get; set; }

        /// <summary>
        ///     Tenders received by electronic means.
        /// </summary>
        [CorrigendumLabel("nb_tenders_received_emeans", "V.2.2")]
        public int? Electronic { get; set; }
    }
}
