
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of CommunicationInformation for Ted integration
    /// </summary>
    public class CommunicationInformationConfiguration
    {
        
        
        public bool ProcurementDocumentsAvailable {get; set;} = false;
        public bool ProcurementDocumentsUrl {get; set;} = false;
        public bool AdditionalInformation {get; set;} = false;
        public ContractBodyContactInformationConfiguration AdditionalInformationAddress {get; set;} = new ContractBodyContactInformationConfiguration();
        public ContractBodyContactInformationConfiguration OtherAddressForProcurementDocuments {get; set;} = new ContractBodyContactInformationConfiguration();
        public bool SendTendersOption {get; set;} = false;
        public bool ElectronicAddressToSendTenders {get; set;} = false;
        public ContractBodyContactInformationConfiguration AddressToSendTenders {get; set;} = new ContractBodyContactInformationConfiguration();
        public bool ElectronicCommunicationRequiresSpecialTools {get; set;} = false;
        public bool ElectronicCommunicationInfoUrl {get; set;} = false;
        public bool DocumentsEntirelyInHilma {get; set;} = false;
        public bool SpecsAndAdditionalDocuments {get; set;} = false;
        public ContractBodyContactInformationConfiguration SpecsAndAdditionalDocumentsAddress {get; set;} = new ContractBodyContactInformationConfiguration();
        public bool ElectronicAccess {get; set;} = false;
        public bool ValidationState {get; set;} = false;
    }
}
