
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ConditionsInformationNational for Ted integration
    /// </summary>
    public class ConditionsInformationNationalConfiguration
    {
        
        
        public bool ParticipantSuitabilityCriteria {get; set;} = false;
        public bool RequiredCertifications {get; set;} = false;
        public bool AdditionalInformation {get; set;} = false;
        public bool ValidationState {get; set;} = false;
        public bool ReservedForShelteredWorkshopOrProgram {get; set;} = false;
    }
}
