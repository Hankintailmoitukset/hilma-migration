
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of CandidateNumberRestrictions for Ted integration
    /// </summary>
    public class CandidateNumberRestrictionsConfiguration
    {
        
        
        public bool EnvisagedNumber {get; set;} = false;
        public bool EnvisagedMinimumNumber {get; set;} = false;
        public bool EnvisagedMaximumNumber {get; set;} = false;
        public bool ObjectiveCriteriaForChoosing {get; set;} = false;
        public bool Selected {get; set;} = false;
    }
}
