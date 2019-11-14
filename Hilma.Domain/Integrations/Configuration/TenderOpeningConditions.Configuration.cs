
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of TenderOpeningConditions for Ted integration
    /// </summary>
    public class TenderOpeningConditionsConfiguration
    {
        
        
        public bool OpeningDateAndTime {get; set;} = false;
        public bool Place {get; set;} = false;
        public bool InformationAboutAuthorisedPersons {get; set;} = false;
    }
}
