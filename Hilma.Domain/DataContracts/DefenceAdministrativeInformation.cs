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
        public bool PreviousPublicationExists { get; set; }

        /// <summary>
        /// IV.3.2) Previous publication(s) concerning the same contract
        /// Prior information notice
        /// Notice on a buyer profile
        /// </summary>
        public PreviousContractType PreviousContractType { get; set; }

        /// <summary>
        /// Previous prior information or buyer profile (buyer needed?) ojs number and its date
        /// </summary>
        public OjsNumber PreviousNoticeOjsNumber { get; set; }

        /// <summary>
        /// If contract award has a previous contract notice ojs number
        /// </summary>
        public bool HasPreviousContractNoticeOjsNumber { get; set; }

        /// <summary>
        /// Previous contract notice ojs number and its date
        /// </summary>
        public OjsNumber PreviousContractNoticeOjsNumber { get; set; }

        /// <summary>
        /// If contract award has a previous ex ante ojs number
        /// </summary>
        public bool HasPreviousExAnteOjsNumber { get; set; }

        /// <summary>
        /// Previous ex ante ojs number and its date
        /// </summary>
        public OjsNumber PreviousExAnteOjsNumber { get; set; }

        /// <summary>
        /// IV.3.3) Conditions for obtaining specifications and additional documents or descriptive document
        /// Time limit for receipt of requests for documents or for accessing documents
        /// Date: [ ] [ ] / [ ] [ ] / [ ] [ ] [ ] [ ] (dd/mm/yyyy) Time: [ ] [ ] : [ ] [ ]
        /// </summary>
        public DateTime? TimeLimitForReceipt { get; set; }

        /// <summary>
        /// IV.3.3) Conditions for obtaining specifications and additional documents or descriptive document 
        /// Payable documents
        /// </summary>
        public bool PayableDocuments { get; set; }

        /// <summary>
        /// Price and currency for payable documents.
        /// </summary>
        public ValueContract DocumentPrice { get; set; }

        /// <summary>
        /// Terms and method of payment
        /// </summary>
        public string[] PaymentTermsAndMethods { get; set; }

        /// <summary>
        /// Any or selected EU language type
        /// </summary>
        public LanguageType LanguageType { get; set; }

        public string[] Languages { get; set; } = new string[0];

        public bool OtherLanguage { get; set; }

        public string OtherLanguages { get; set; }

    }
}
