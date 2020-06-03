
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of TranslationsConfigurationContract for Ted integration
    /// </summary>
    public class TranslationsConfigurationContractConfiguration
    {
        
        
        public bool TranslationsEndpoint {get; set;} = false;
        public bool TranslationsCacheLifetimeMinutes {get; set;} = false;
        public bool OrganisationUrl {get; set;} = false;
    }
}
