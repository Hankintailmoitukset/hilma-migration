
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of AwardCriteria for Ted integration
    /// </summary>
    public class AwardCriteriaConfiguration
    {
        
        
        public bool CriterionTypes {get; set;} = false;
        public AwardCriterionDefinitionConfiguration QualityCriteria {get; set;} = new AwardCriterionDefinitionConfiguration();
        public AwardCriterionDefinitionConfiguration CostCriteria {get; set;} = new AwardCriterionDefinitionConfiguration();
        public AwardCriterionDefinitionConfiguration PriceCriterion {get; set;} = new AwardCriterionDefinitionConfiguration();
        public bool Criterion {get; set;} = false;
        public bool CriteriaStatedInProcurementDocuments {get; set;} = false;
    }
}
