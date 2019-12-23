
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of Modifications for Ted integration
    /// </summary>
    public class ModificationsConfiguration
    {
        
        
        public CpvCodeConfiguration MainCpvCode {get; set;} = new CpvCodeConfiguration();
        public CpvCodeConfiguration AdditionalCpvCodes {get; set;} = new CpvCodeConfiguration();
        public bool NutsCodes {get; set;} = false;
        public bool MainsiteplaceWorksDelivery {get; set;} = false;
        public bool DescrProcurement {get; set;} = false;
        public TimeFrameConfiguration TimeFrame {get; set;} = new TimeFrameConfiguration();
        public bool JustificationForDurationOverFourYears {get; set;} = false;
        public bool JustificationForDurationOverEightYears {get; set;} = false;
        public ValueContractConfiguration TotalValue {get; set;} = new ValueContractConfiguration();
        public bool AwardedToGroupOfEconomicOperators {get; set;} = false;
        public ContractorContactInformationConfiguration Contractors {get; set;} = new ContractorContactInformationConfiguration();
        public bool Description {get; set;} = false;
        public bool Reason {get; set;} = false;
        public bool ReasonDescriptionEconomic {get; set;} = false;
        public bool ReasonDescriptionCircumstances {get; set;} = false;
        public ValueContractConfiguration IncreaseBeforeModifications {get; set;} = new ValueContractConfiguration();
        public ValueContractConfiguration IncreaseAfterModifications {get; set;} = new ValueContractConfiguration();
        public bool ValidationState {get; set;} = false;
    }
}
