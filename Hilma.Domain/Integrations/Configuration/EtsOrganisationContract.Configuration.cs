
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of EtsOrganisationContract for Ted integration
    /// </summary>
    public class EtsOrganisationContractConfiguration
    {
        
        
        public ContractBodyContactInformationConfiguration Information {get; set;} = new ContractBodyContactInformationConfiguration();
        public bool ContractingAuthorityType {get; set;} = false;
        public bool ContractingType {get; set;} = false;
        public bool MainActivity {get; set;} = false;
        public bool MainActivityUtilities {get; set;} = false;
        public bool OtherContractingAuthorityType {get; set;} = false;
        public bool OtherMainActivity {get; set;} = false;
    }
}
