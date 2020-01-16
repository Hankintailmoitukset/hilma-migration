using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     Communications section of a notice. Contains information on where to
    ///     get additional information and where to send the offers.
    /// </summary>
    //[Owned]
    [Contract]
    public class CommunicationInformation
    {
        /// <summary>
        ///     How are the documents related to the procurement described by the notice available.
        /// </summary>
        [CorrigendumLabel("info_tendering", "I.3")]
        public ProcurementDocumentAvailability ProcurementDocumentsAvailable { get; set; }

        /// <summary>
        ///     Url for the documents. Including protocol.
        /// </summary>
        /// <example>
        ///     https://www.innofactor.com/spec_document.pdf
        /// </example>
        [CorrigendumLabel("address_obtain_docs", "I.3")]
        public string ProcurementDocumentsUrl { get; set; }

        /// <summary>
        ///     Where is additional information available from.
        /// </summary>
        [CorrigendumLabel("address_furtherinfo", "I.3")]
        public AdditionalInformationAvailability AdditionalInformation { get; set; }

        /// <summary>
        ///     If AdditionalInformation=AddressAnother, the contact details to get the information from.
        ///     Including protocol.
        ///     Additional information can be obtained from -> another address
        /// </summary>
        [CorrigendumLabel("address_another", "I.3")]
        public ContractBodyContactInformation AdditionalInformationAddress { get; set; } = new ContractBodyContactInformation();

        /// <summary>
        /// Documents available from an other address
        /// </summary>
        [CorrigendumLabel("address_another", "I.3")]
        public ContractBodyContactInformation OtherAddressForProcurementDocuments { get; set; } = new ContractBodyContactInformation();

        /// <summary>
        ///     How are tenders to be sent.
        /// </summary>
        [CorrigendumLabel("address_send_tenders_request", "I.3")]
        public TenderSendOptions SendTendersOption { get; set; }

        /// <summary>
        ///     If SendTendersOption=AddressSendTenders: the url for the tenders. Including protocol.
        /// </summary>
        /// <example>
        ///     https://www.innofactor.com
        /// </example>
        [CorrigendumLabel("address_send_tenders", "I.3")]
        public string ElectronicAddressToSendTenders { get; set; }

        /// <summary>
        ///     If SendTendersOption=AddressFollowing: physical for the tenders.
        ///     If SendTendersOption=AddressOrganisation: this value should be copied from organisation contact information.
        ///     Tenders or requests to participate must be submitted -> to the following address
        /// </summary>
        [CorrigendumLabel("address_following", "I.3")]
        public ContractBodyContactInformation AddressToSendTenders { get; set; } = new ContractBodyContactInformation();

        /// <summary>
        ///     Does making the tender require specialized tools.
        /// </summary>
        [CorrigendumLabel("url_communication_tools", "I.3")]
        public bool ElectronicCommunicationRequiresSpecialTools { get; set; }

        /// <summary>
        ///     If making the tender requires special tools, the address to obtain them from.
        ///     Including protocol.
        /// </summary>
        /// <example>
        ///     https://www.innofactor.dev
        /// </example>
        [CorrigendumLabel("info_tendering_url", "I.3")]
        public string ElectronicCommunicationInfoUrl { get; set; }

        /// <summary>
        ///     Procurement documents in Hilma in full.
        /// </summary>
        public bool DocumentsEntirelyInHilma { get; set; }

        /// <summary>
        ///     Directive 2009/81/EY (Defence contracts)
        ///     Specifications and additional documents can be obtained from
        /// </summary>
        [CorrigendumLabel("address_additionaldocs", "I.1")]
        public SpecificationsAndAdditionalDocuments SpecsAndAdditionalDocuments { get; set; }

        /// <summary>
        ///     Directive 2009/81/EY (Defence contracts)
        ///     If SpecsAndAdditionalDocuments=AddressAnother, the contact details to get the information from.
        ///     Other (please complete Annex A.II)
        /// </summary>
        [CorrigendumLabel("address_another", "I.1")]
        public ContractBodyContactInformation SpecsAndAdditionalDocumentsAddress { get; set; }

        /// <summary>
        /// Directive 2009/81/EY (Defence notices)
        /// Electronic access to information: (URL)
        /// </summary>
        [CorrigendumLabel("url_information", "I.1")]
        public string ElectronicAccess { get; set; }

        /// <summary>
        ///     Vuejs application validation state for corresponding form section.
        /// </summary>
        public ValidationState ValidationState { get; set; }
    }
}
