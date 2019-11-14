
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ContractAward for Ted integration
    /// </summary>
    public class ContractAwardConfiguration
    {
        
        
        public bool ConclusionDate {get; set;} = false;
        public bool ContractNumber {get; set;} = false;
        public bool ContractTitle {get; set;} = false;
        public NumberOfTendersConfiguration NumberOfTenders {get; set;} = new NumberOfTendersConfiguration();
        public bool DisagreeContractorInformationToBePublished {get; set;} = false;
        public ContractorContactInformationConfiguration Contractors {get; set;} = new ContractorContactInformationConfiguration();
        public ValueContractConfiguration InitialEstimatedValueOfContract {get; set;} = new ValueContractConfiguration();
        public ValueRangeContractConfiguration FinalTotalValue {get; set;} = new ValueRangeContractConfiguration();
        public bool LikelyToBeSubcontracted {get; set;} = false;
        public ValueContractConfiguration ValueOfSubcontract {get; set;} = new ValueContractConfiguration();
        public bool ProportionOfValue {get; set;} = false;
        public bool SubcontractingDescription {get; set;} = false;
        public ExAnteSubcontractingConfiguration ExAnteSubcontracting {get; set;} = new ExAnteSubcontractingConfiguration();
        public ValueContractConfiguration PricePaidForBargainPurchases {get; set;} = new ValueContractConfiguration();
        public ContractAwardNotPublicFieldsConfiguration NotPublicFields {get; set;} = new ContractAwardNotPublicFieldsConfiguration();
    }
}
