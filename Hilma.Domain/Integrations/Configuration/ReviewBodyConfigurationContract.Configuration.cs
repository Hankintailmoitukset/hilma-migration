
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ReviewBodyConfigurationContract for Ted integration
    /// </summary>
    public class ReviewBodyConfigurationContractConfiguration
    {
        
        
        public bool OfficialName {get; set;} = false;
        public bool StreetAddress {get; set;} = false;
        public bool PostalCode {get; set;} = false;
        public bool Town {get; set;} = false;
        public bool Country {get; set;} = false;
        public bool TelephoneNumber {get; set;} = false;
        public bool Email {get; set;} = false;
        public bool MainUrl {get; set;} = false;
    }
}
