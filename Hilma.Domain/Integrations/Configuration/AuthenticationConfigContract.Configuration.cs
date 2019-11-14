
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of AuthenticationConfigContract for Ted integration
    /// </summary>
    public class AuthenticationConfigContractConfiguration
    {
        
        
        public bool HilmaAppId {get; set;} = false;
        public bool B2CAuthority {get; set;} = false;
        public bool B2CReset {get; set;} = false;
        public bool B2CWebUserScope {get; set;} = false;
    }
}
