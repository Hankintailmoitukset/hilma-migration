
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ContractorContactInformation for Ted integration
    /// </summary>
    public class ContractorContactInformationConfiguration
    {
        
        
        public bool ContractId {get; set;} = false;
        public bool OfficialName {get; set;} = false;
        public bool NationalRegistrationNumber {get; set;} = false;
        public bool NutsCodes {get; set;} = false;
        public PostalAddressConfiguration PostalAddress {get; set;} = new PostalAddressConfiguration();
        public bool TelephoneNumber {get; set;} = false;
        public bool Email {get; set;} = false;
        public bool MainUrl {get; set;} = false;
        public bool IsSmallMediumEnterprise {get; set;} = false;
        public bool ValidationState {get; set;} = false;
    }
}
