
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of LotsInfo for Ted integration
    /// </summary>
    public class LotsInfoConfiguration
    {
        
        
        public bool DivisionLots {get; set;} = false;
        public bool QuantityOfLots {get; set;} = false;
        public bool LotsSubmittedFor {get; set;} = false;
        public bool LotsSubmittedForQuantity {get; set;} = false;
        public bool LotsMaxAwarded {get; set;} = false;
        public bool LotsMaxAwardedQuantity {get; set;} = false;
        public bool LotCombinationPossible {get; set;} = false;
        public bool LotCombinationPossibleDescription {get; set;} = false;
        public bool ValidationState {get; set;} = false;
    }
}
