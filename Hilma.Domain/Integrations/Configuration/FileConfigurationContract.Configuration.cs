
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of FileConfigurationContract for Ted integration
    /// </summary>
    public class FileConfigurationContractConfiguration
    {
        
        
        public bool AllowedExtensions {get; set;} = false;
        public bool DisableFileScan {get; set;} = false;
    }
}
