
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ProcedureInformationDefence for Ted integration
    /// </summary>
    public class ProcedureInformationDefenceConfiguration
    {
        
        
        public CandidateNumberRestrictionsConfiguration CandidateNumberRestrictions {get; set;} = new CandidateNumberRestrictionsConfiguration();
        public AwardCriteriaDefenceConfiguration AwardCriteria {get; set;} = new AwardCriteriaDefenceConfiguration();
    }
}
