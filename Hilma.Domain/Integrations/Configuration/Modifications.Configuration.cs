
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of Modifications for Ted integration
    /// </summary>
    public class ModificationsConfiguration
    {
        
        
        public bool Description {get; set;} = false;
        public bool AffectedLot {get; set;} = false;
        public bool Reason {get; set;} = false;
        public bool ReasonDescriptionEconomic {get; set;} = false;
        public bool ReasonDescriptionCircumstances {get; set;} = false;
        public ValueContractConfiguration IncreaseBeforeModifications {get; set;} = new ValueContractConfiguration();
        public ValueContractConfiguration IncreaseAfterModifications {get; set;} = new ValueContractConfiguration();
        public bool ValidationState {get; set;} = false;
    }
}
