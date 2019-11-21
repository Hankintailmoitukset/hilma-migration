
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ProcedureInformationNational for Ted integration
    /// </summary>
    public class ProcedureInformationNationalConfiguration
    {
        
        
        public bool OtherProcedure {get; set;} = false;
        public bool AdditionalProcedureInformation {get; set;} = false;
        public bool TransparencyType {get; set;} = false;
        public bool LimitedNumberOfParticipants {get; set;} = false;
        public bool NumberOfParticipants {get; set;} = false;
        public bool SelectionCriteria {get; set;} = false;
    }
}
