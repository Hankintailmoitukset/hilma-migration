
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of FrameworkAgreementInformation for Ted integration
    /// </summary>
    public class FrameworkAgreementInformationConfiguration
    {
        
        
        public bool IncludesFrameworkAgreement {get; set;} = false;
        public bool FrameworkAgreementType {get; set;} = false;
        public bool EnvisagedNumberOfParticipants {get; set;} = false;
        public bool FrameworkEnvisagedType {get; set;} = false;
        public bool IncludesDynamicPurchasingSystem {get; set;} = false;
        public bool DynamicPurchasingSystemInvolvesAdditionalPurchasers {get; set;} = false;
        public bool JustificationForDurationOverFourYears {get; set;} = false;
        public bool JustificationForDurationOverSevenYears {get; set;} = false;
        public bool JustificationForDurationOverEightYears {get; set;} = false;
        public bool DynamicPurchasingSystemWasTerminated {get; set;} = false;
        public ValueRangeContractConfiguration EstimatedTotalValue {get; set;} = new ValueRangeContractConfiguration();
        public TimeFrameConfiguration Duration {get; set;} = new TimeFrameConfiguration();
        public bool FrequencyAndValue {get; set;} = false;
    }
}
