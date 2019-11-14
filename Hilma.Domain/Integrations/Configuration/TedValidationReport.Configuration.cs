
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of TedValidationReport for Ted integration
    /// </summary>
    public class TedValidationReportConfiguration
    {
        
        
        public bool Type {get; set;} = false;
        public TedValidationItemConfiguration Items {get; set;} = new TedValidationItemConfiguration();
    }
}
