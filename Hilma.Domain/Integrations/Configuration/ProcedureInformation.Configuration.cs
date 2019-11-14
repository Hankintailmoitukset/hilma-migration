
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ProcedureInformation for Ted integration
    /// </summary>
    public class ProcedureInformationConfiguration
    {
        
        
        public bool ProcedureType {get; set;} = false;
        public bool AcceleratedProcedure {get; set;} = false;
        public bool JustificationForAcceleratedProcedure {get; set;} = false;
        public bool ContestType {get; set;} = false;
        public ValueRangeContractConfiguration ContestParticipants {get; set;} = new ValueRangeContractConfiguration();
        public FrameworkAgreementInformationConfiguration FrameworkAgreement {get; set;} = new FrameworkAgreementInformationConfiguration();
        public bool ReductionRecourseToReduceNumberOfSolutions {get; set;} = false;
        public bool ReserveRightToAwardWithoutNegotiations {get; set;} = false;
        public bool ElectronicAuctionWillBeUsed {get; set;} = false;
        public bool AdditionalInformationAboutElectronicAuction {get; set;} = false;
        public bool NamesOfParticipantsAlreadySelected {get; set;} = false;
        public bool ProcurementGovernedByGPA {get; set;} = false;
        public bool CriteriaForEvaluationOfProjects {get; set;} = false;
        public bool UrlNationalProcedure {get; set;} = false;
        public bool MainFeaturesAward {get; set;} = false;
        public ProcedureInformationDefenceConfiguration Defence {get; set;} = new ProcedureInformationDefenceConfiguration();
        public ProcedureInformationNationalConfiguration National {get; set;} = new ProcedureInformationNationalConfiguration();
        public bool ValidationState {get; set;} = false;
    }
}
