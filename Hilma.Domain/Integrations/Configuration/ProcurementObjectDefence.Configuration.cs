
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ProcurementObjectDefence for Ted integration
    /// </summary>
    public class ProcurementObjectDefenceConfiguration
    {
        
        
        public bool MainsiteplaceWorksDelivery {get; set;} = false;
        public bool NutsCodes {get; set;} = false;
        public FrameworkAgreementInformationConfiguration FrameworkAgreement {get; set;} = new FrameworkAgreementInformationConfiguration();
        public CpvCodeConfiguration AdditionalCpvCodes {get; set;} = new CpvCodeConfiguration();
        public SubcontractingInformationConfiguration Subcontract {get; set;} = new SubcontractingInformationConfiguration();
        public OptionsAndVariantsConfiguration OptionsAndVariants {get; set;} = new OptionsAndVariantsConfiguration();
        public ValueRangeContractConfiguration TotalQuantityOrScope {get; set;} = new ValueRangeContractConfiguration();
        public bool TotalQuantity {get; set;} = false;
        public DefenceRenewalsConfiguration Renewals {get; set;} = new DefenceRenewalsConfiguration();
        public TimeFrameConfiguration TimeFrame {get; set;} = new TimeFrameConfiguration();
        public bool AdditionalInformation {get; set;} = false;
    }
}
