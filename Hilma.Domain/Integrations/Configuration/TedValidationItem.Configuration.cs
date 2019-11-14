
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of TedValidationItem for Ted integration
    /// </summary>
    public class TedValidationItemConfiguration
    {
        
        
        public bool Name {get; set;} = false;
        public bool Valid {get; set;} = false;
        public bool Severity {get; set;} = false;
        public bool Message {get; set;} = false;
        public bool Details {get; set;} = false;
    }
}
