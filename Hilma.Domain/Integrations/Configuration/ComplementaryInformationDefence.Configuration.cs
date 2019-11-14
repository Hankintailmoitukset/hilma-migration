
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ComplementaryInformationDefence for Ted integration
    /// </summary>
    public class ComplementaryInformationDefenceConfiguration
    {
        
        
        public EuFundsConfiguration EuFunds {get; set;} = new EuFundsConfiguration();
        public bool TaxLegislationUrl {get; set;} = false;
        public bool TaxLegislationInfoProvided {get; set;} = false;
        public ContractBodyContactInformationConfiguration TaxLegislation {get; set;} = new ContractBodyContactInformationConfiguration();
        public bool EnvironmentalProtectionUrl {get; set;} = false;
        public bool EnvironmentalProtectionInfoProvided {get; set;} = false;
        public ContractBodyContactInformationConfiguration EnvironmentalProtection {get; set;} = new ContractBodyContactInformationConfiguration();
        public bool EmploymentProtectionUrl {get; set;} = false;
        public bool EmploymentProtectionInfoProvided {get; set;} = false;
        public ContractBodyContactInformationConfiguration EmploymentProtection {get; set;} = new ContractBodyContactInformationConfiguration();
    }
}
