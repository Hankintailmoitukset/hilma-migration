
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of TedConfigurationContract for Ted integration
    /// </summary>
    public class TedConfigurationContractConfiguration
    {
        
        
        public bool Username {get; set;} = false;
        public bool Password {get; set;} = false;
        public bool ESenderApiUrl {get; set;} = false;
        public bool IsSimulation {get; set;} = false;
    }
}
