
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of CpvCodeMetadata for Ted integration
    /// </summary>
    public class CpvCodeMetadataConfiguration
    {
        
        
        public bool Code {get; set;} = false;
        public bool Name {get; set;} = false;
        public bool IsSocialService {get; set;} = false;
        public bool HasSocialServiceChild {get; set;} = false;
        public bool IsNatSocial {get; set;} = false;
        public bool IsNatOther {get; set;} = false;
    }
}
