
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ObjectDescription for Ted integration
    /// </summary>
    public class ObjectDescriptionConfiguration
    {
        
        
        public bool Title {get; set;} = false;
        public bool LotNumber {get; set;} = false;
        public CpvCodeConfiguration MainCpvCode {get; set;} = new CpvCodeConfiguration();
        public bool QuantityOrScope {get; set;} = false;
        public CpvCodeConfiguration AdditionalCpvCodes {get; set;} = new CpvCodeConfiguration();
        public bool NutsCodes {get; set;} = false;
        public bool MainsiteplaceWorksDelivery {get; set;} = false;
        public bool DescrProcurement {get; set;} = false;
        public bool DisagreeAwardCriteriaToBePublished {get; set;} = false;
        public AwardCriteriaConfiguration AwardCriteria {get; set;} = new AwardCriteriaConfiguration();
        public ValueRangeContractConfiguration EstimatedValue {get; set;} = new ValueRangeContractConfiguration();
        public TimeFrameConfiguration TimeFrame {get; set;} = new TimeFrameConfiguration();
        public CandidateNumberRestrictionsConfiguration CandidateNumberRestrictions {get; set;} = new CandidateNumberRestrictionsConfiguration();
        public OptionsAndVariantsConfiguration OptionsAndVariants {get; set;} = new OptionsAndVariantsConfiguration();
        public bool TendersMustBePresentedAsElectronicCatalogs {get; set;} = false;
        public EuFundsConfiguration EuFunds {get; set;} = new EuFundsConfiguration();
        public bool AdditionalInformation {get; set;} = false;
        public AwardConfiguration AwardContract {get; set;} = new AwardConfiguration();
        public QualificationSystemDurationConfiguration QualificationSystemDuration {get; set;} = new QualificationSystemDurationConfiguration();
        public bool ValidationState {get; set;} = false;
    }
}
