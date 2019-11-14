
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ContractAwardDefence for Ted integration
    /// </summary>
    public class ContractAwardDefenceConfiguration
    {
        
        
        public bool LotNumber {get; set;} = false;
        public bool LotTitle {get; set;} = false;
        public bool ContractAwardDecisionDate {get; set;} = false;
        public NumberOfTendersConfiguration NumberOfTenders {get; set;} = new NumberOfTendersConfiguration();
        public ContractorContactInformationConfiguration Contractor {get; set;} = new ContractorContactInformationConfiguration();
        public ValueContractConfiguration EstimatedValue {get; set;} = new ValueContractConfiguration();
        public bool ContractValueType {get; set;} = false;
        public ValueContractConfiguration FinalTotalValue {get; set;} = new ValueContractConfiguration();
        public ValueContractConfiguration LowestOffer {get; set;} = new ValueContractConfiguration();
        public ValueContractConfiguration HighestOffer {get; set;} = new ValueContractConfiguration();
        public TimeFrameConfiguration AnnualOrMonthlyValue {get; set;} = new TimeFrameConfiguration();
        public bool LikelyToBeSubcontracted {get; set;} = false;
        public ValueContractConfiguration ValueOfSubcontract {get; set;} = new ValueContractConfiguration();
        public bool ProportionOfValue {get; set;} = false;
        public bool ValueOfSubcontractNotKnown {get; set;} = false;
        public bool SubcontractingDescription {get; set;} = false;
        public bool AllOrCertainSubcontractsWillBeAwarded {get; set;} = false;
        public bool ShareOfContractWillBeSubcontracted {get; set;} = false;
        public bool ShareOfContractWillBeSubcontractedMinPercentage {get; set;} = false;
        public bool ShareOfContractWillBeSubcontractedMaxPercentage {get; set;} = false;
        public bool ValidationState {get; set;} = false;
    }
}
