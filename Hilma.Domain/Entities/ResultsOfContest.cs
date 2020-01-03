using Hilma.Domain.Attributes;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Hilma.Domain.Entities
{
    /// <summary>
    /// Section V: Results of contest
    /// </summary>
    [Contract]
    public class ResultsOfContest
    {
        /// <summary>
        /// The contest was terminated without an award or attribution of prizes 
        /// </summary>
        [CorrigendumLabel("dc_terminated", "V")]
        public bool ContestWasTerminated { get; set; }

        #region V.1)Information on non-award
        /// <summary>
        /// V.1)Information on non-award
        /// </summary>
        [CorrigendumLabel("award_noaward_info", "V.1")]
        public NoPrizeType NoPrizeType { get; set; }

        /// <summary>
        ///     How was the original notice submitted
        /// </summary>
        public NoticeDeliveryMethod OriginalNoticeSentVia { get; set; }

        /// <summary>
        ///     The information about original e-sender
        /// </summary>
        public Esender OriginalEsender { get; set; }

        /// <summary>
        ///     Which other method was used to submit the original notice.
        /// </summary>
        public string OriginalNoticeSentViaOther { get; set; }

        /// <summary>
        ///     Date of original notice submission.
        /// </summary>
        [CorrigendumLabel("icar_date_original", "V.1")]
        public DateTime? OriginalNoticeSentDate { get; set; }
        #endregion

        #region V.3) Award and prizes
        /// <summary>
        /// V.3.1) Date of the jury decision
        /// </summary>
        [CorrigendumLabel("dc_date_decision", "V.3.1")]
        public DateTime? DateOfJuryDecision { get; set; }

        /// <summary>
        /// Number of participants to be contemplated
        /// </summary>
        [CorrigendumLabel("number_participants", "V.3.2")]
        public int ParticipantsContemplated { get; set; }

        /// <summary>
        /// Number of participating SMEs
        /// </summary>
        [CorrigendumLabel("number_participants_sme", "V.3.2")]
        public int ParticipantsSme { get; set; }

        /// <summary>
        /// Number of participants from other countries
        /// </summary>
        [CorrigendumLabel("number_participants_foreign", "V.3.2")]
        public int ParticipantsForeign { get; set; }

        /// <summary>
        /// If the infomation in this section is confidential and should not be published on TED, it must be indicated by clicking "NO".
        /// </summary>
        public bool DisagreeParticipantCountPublish { get; set; }

        /// <summary>
        /// V.3.3) Name(s) and address(es) of the winner(s) of the contest
        /// </summary>
        public List<ContractorContactInformation> Winners { get; set; }

        /// <summary>
        /// If the infomation in this section is confidential and should not be published on TED, it must be indicated by clicking "NO".
        /// </summary>
        public bool DisagreeWinnersPublish { get; set; }

        /// <summary>
        /// V.3.4) Value of the prize(s) 2
        /// Value of the prize(s) awarded excluding VAT
        /// </summary>
        [CorrigendumLabel("dc_value_prizes_excl_vat", "V.3.4")]
        public ValueContract ValueOfPrize { get; set; }

        /// <summary>
        /// If the infomation in this section is confidential and should not be published on TED, it must be indicated by clicking "NO".
        /// </summary>
        public bool DisagreeValuePublish { get; set; }
        #endregion

        #region VueJS
        /// <summary>
        ///     Validation state for Vuejs application.
        /// </summary>
        public ValidationState ValidationState { get; set; }
        #endregion
    }
}
