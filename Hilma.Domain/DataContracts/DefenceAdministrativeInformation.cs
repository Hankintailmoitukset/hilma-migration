using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;
using System;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// Directive 2009/81/EC
    /// IV.3) Administrative information
    /// </summary>
    [Contract]
    public class DefenceAdministrativeInformation
    {
        /// <summary>
        /// IV.3.2) Previous publication(s) concerning the same contract
        /// </summary>
        [CorrigendumLabel("previous_publication_exists", "IV.3.2")]
        public bool PreviousPublicationExists { get; set; }

        /// <summary>
        /// Previous prior information ojs number and its date
        /// </summary>
        [CorrigendumLabel("pub_previous", "IV.3.2")]
        public OjsNumber PreviousPriorInformationNoticeOjsNumber { get; set; }

        /// <summary>
        /// If contract award has a previous contract notice ojs number
        /// </summary>
        [CorrigendumLabel("previous_contract_notice_exists", "IV.3.2")]
        public bool HasPreviousContractNoticeOjsNumber { get; set; }

        /// <summary>
        /// Previous contract notice ojs number and its date
        /// </summary>
        [CorrigendumLabel("pub_previous", "IV.3.2")]
        public OjsNumber PreviousContractNoticeOjsNumber { get; set; }

        /// <summary>
        /// If contract award has a previous ex ante ojs number
        /// </summary>
        [CorrigendumLabel("previous_ex_ante_exists", "IV.3.2")]
        public bool HasPreviousExAnteOjsNumber { get; set; }

        /// <summary>
        /// Previous ex ante ojs number and its date
        /// </summary>
        [CorrigendumLabel("pub_previous", "IV.3.2")]
        public OjsNumber PreviousExAnteOjsNumber { get; set; }

        /// <summary>
        /// IV.3.3) Conditions for obtaining specifications and additional documents or descriptive document
        /// Time limit for receipt of requests for documents or for accessing documents
        /// Date: [ ] [ ] / [ ] [ ] / [ ] [ ] [ ] [ ] (dd/mm/yyyy) Time: [ ] [ ] : [ ] [ ]
        /// </summary>
        [CorrigendumLabel("time_limit_for_receipt", "IV.3.3")]
        public DateTime? TimeLimitForReceipt { get; set; }

        /// <summary>
        /// IV.3.3) Conditions for obtaining specifications and additional documents or descriptive document 
        /// Payable documents
        /// </summary>
        [CorrigendumLabel("payable_documents", "IV.3.3")]
        public bool PayableDocuments { get; set; }

        /// <summary>
        /// Price and currency for payable documents.
        /// </summary>
        [CorrigendumLabel("price", "IV.3.3")]
        public ValueContract DocumentPrice { get; set; }

        /// <summary>
        /// Terms and method of payment
        /// </summary>
        [CorrigendumLabel("payment_method_terms", "IV.3.3")]
        public string[] PaymentTermsAndMethods { get; set; }

        /// <summary>
        /// Any or selected EU language type
        /// </summary>
        [CorrigendumLabel("languages_allowed_old", "IV.3.6")]
        public LanguageType LanguageType { get; set; }

        /// <summary>
        /// IV.3.6) Language(s) in which tenders or requests to participate may be drawn up
        /// Official EU language(s):
        /// </summary>
        [CorrigendumLabel("languages_allowed_old", "IV.3.6")]
        public string[] Languages { get; set; } = new string[0];

        /// <summary>
        /// IV.3.6) Language(s) in which tenders or requests to participate may be drawn up
        /// Other -boolean
        /// </summary>
        [CorrigendumLabel("other_languages", "IV.3.6")]
        public bool OtherLanguage { get; set; }

        /// <summary>
        /// IV.3.6) Language(s) in which tenders or requests to participate may be drawn up
        /// Other:
        /// </summary>
        [CorrigendumLabel("other_languages", "IV.3.6")]
        public string OtherLanguages { get; set; }

    }
}
