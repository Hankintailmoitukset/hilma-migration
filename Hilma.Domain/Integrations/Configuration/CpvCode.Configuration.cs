
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of CpvCode for Ted integration
    /// </summary>
    public class CpvCodeConfiguration
    {
        
        
        public bool Code {get; set;} = false;
        public bool Name {get; set;} = false;
        public VocCodeConfiguration VocCodes {get; set;} = new VocCodeConfiguration();
    }
}
