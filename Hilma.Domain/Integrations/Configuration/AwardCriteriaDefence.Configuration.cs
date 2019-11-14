
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of AwardCriteriaDefence for Ted integration
    /// </summary>
    public class AwardCriteriaDefenceConfiguration
    {
        
        
        public bool CriterionTypes {get; set;} = false;
        public bool EconomicCriteriaTypes {get; set;} = false;
        public AwardCriterionDefinitionConfiguration Criteria {get; set;} = new AwardCriterionDefinitionConfiguration();
    }
}
