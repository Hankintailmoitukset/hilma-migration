
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of EtsProjectContract for Ted integration
    /// </summary>
    public class EtsProjectContractConfiguration
    {
        
        
        public bool Id {get; set;} = false;
        public bool ContractType {get; set;} = false;
        public bool ReferenceNumber {get; set;} = false;
        public bool Title {get; set;} = false;
        public bool ProcurementCategory {get; set;} = false;
        public bool JointProcurement {get; set;} = false;
        public bool ProcurementLaw {get; set;} = false;
        public bool CentralPurchasing {get; set;} = false;
        public ContractBodyContactInformationConfiguration CoPurchasers {get; set;} = new ContractBodyContactInformationConfiguration();
        public bool DefenceWorks {get; set;} = false;
        public bool DefenceSupplies {get; set;} = false;
        public DefenceCategoryConfiguration DefenceCategory {get; set;} = new DefenceCategoryConfiguration();
        public bool DisagreeToPublishNoticeBasedOnDefenceServiceCategory4 {get; set;} = false;
        public bool AgricultureWorks {get; set;} = false;
    }
}
