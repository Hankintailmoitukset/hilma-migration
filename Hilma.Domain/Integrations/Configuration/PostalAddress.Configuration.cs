
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of PostalAddress for Ted integration
    /// </summary>
    public class PostalAddressConfiguration
    {
        
        
        public bool StreetAddress {get; set;} = false;
        public bool PostalCode {get; set;} = false;
        public bool Town {get; set;} = false;
        public bool Country {get; set;} = false;
    }
}
