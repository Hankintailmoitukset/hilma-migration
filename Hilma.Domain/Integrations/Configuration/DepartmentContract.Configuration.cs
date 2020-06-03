
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of DepartmentContract for Ted integration
    /// </summary>
    public class DepartmentContractConfiguration
    {
        
        
        public bool Id {get; set;} = false;
        public bool Department {get; set;} = false;
        public bool NutsCodes {get; set;} = false;
        public PostalAddressConfiguration PostalAddress {get; set;} = new PostalAddressConfiguration();
        public bool TelephoneNumber {get; set;} = false;
        public bool Email {get; set;} = false;
        public bool ContactPerson {get; set;} = false;
        public bool MainUrl {get; set;} = false;
        public bool ContractingAuthorityType {get; set;} = false;
        public bool OtherContractingAuthorityType {get; set;} = false;
        public bool ContractingType {get; set;} = false;
        public bool MainActivity {get; set;} = false;
        public bool OtherMainActivity {get; set;} = false;
        public bool MainActivityUtilities {get; set;} = false;
        public bool ValidationState {get; set;} = false;
    }
}
