
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of OrganisationContract for Ted integration
    /// </summary>
    public class OrganisationContractConfiguration
    {
        
        
        public bool Id {get; set;} = false;
        public ContractBodyContactInformationConfiguration Information {get; set;} = new ContractBodyContactInformationConfiguration();
        public bool IsUtilitiesEntity {get; set;} = false;
        public bool ContractingAuthorityType {get; set;} = false;
        public bool OtherContractingAuthorityType {get; set;} = false;
        public bool MainActivity {get; set;} = false;
        public bool OtherMainActivity {get; set;} = false;
        public bool MainActivityUtilities {get; set;} = false;
        public bool ValidationState {get; set;} = false;
    }
}
