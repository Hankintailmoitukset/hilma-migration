
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of SerilogConfigurationContract for Ted integration
    /// </summary>
    public class SerilogConfigurationContractConfiguration
    {
        
        
        public bool WorkspaceId {get; set;} = false;
        public bool AuthenticationId {get; set;} = false;
        public bool LogName {get; set;} = false;
    }
}
