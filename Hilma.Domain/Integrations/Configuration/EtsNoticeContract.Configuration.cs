
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of EtsNoticeContract for Ted integration
    /// </summary>
    public class EtsNoticeContractConfiguration
    {
        
        
        public bool Id {get; set;} = false;
        public ValueRangeContractConfiguration TotalValue {get; set;} = new ValueRangeContractConfiguration();
        public bool LegalBasis {get; set;} = false;
        public CommunicationInformationConfiguration CommunicationInformation {get; set;} = new CommunicationInformationConfiguration();
        public ComplementaryInformationConfiguration ComplementaryInformation {get; set;} = new ComplementaryInformationConfiguration();
        public ConditionsInformationConfiguration ConditionsInformation {get; set;} = new ConditionsInformationConfiguration();
        public ConditionsInformationDefenceConfiguration ConditionsInformationDefence {get; set;} = new ConditionsInformationDefenceConfiguration();
        public ConditionsInformationNationalConfiguration ConditionsInformationNational {get; set;} = new ConditionsInformationNationalConfiguration();
        public ContactPersonConfiguration ContactPerson {get; set;} = new ContactPersonConfiguration();
        public ValueRangeContractConfiguration EstimatedValue {get; set;} = new ValueRangeContractConfiguration();
        public bool EstimatedValueCalculationMethod {get; set;} = false;
        public LinkConfiguration Links {get; set;} = new LinkConfiguration();
        public LotsInfoConfiguration LotsInfo {get; set;} = new LotsInfoConfiguration();
        public CpvCodeConfiguration MainCpvCode {get; set;} = new CpvCodeConfiguration();
        public ObjectDescriptionConfiguration ObjectDescriptions {get; set;} = new ObjectDescriptionConfiguration();
        public EtsOrganisationContractConfiguration Organisation {get; set;} = new EtsOrganisationContractConfiguration();
        public bool NoticeOjsNumber {get; set;} = false;
        public bool PreviousNoticeOjsNumber {get; set;} = false;
        public EtsProjectContractConfiguration Project {get; set;} = new EtsProjectContractConfiguration();
        public bool ShortDescription {get; set;} = false;
        public bool Type {get; set;} = false;
        public TenderingInformationConfiguration TenderingInformation {get; set;} = new TenderingInformationConfiguration();
        public RewardsAndJuryConfiguration RewardsAndJury {get; set;} = new RewardsAndJuryConfiguration();
        public ResultsOfContestConfiguration ResultsOfContest {get; set;} = new ResultsOfContestConfiguration();
        public bool Language {get; set;} = false;
        public ProcedureInformationConfiguration ProcedureInformation {get; set;} = new ProcedureInformationConfiguration();
        public ProceduresForReviewInformationConfiguration ProceduresForReview {get; set;} = new ProceduresForReviewInformationConfiguration();
        public ModificationsConfiguration Modifications {get; set;} = new ModificationsConfiguration();
        public ProcurementObjectDefenceConfiguration Defence {get; set;} = new ProcurementObjectDefenceConfiguration();
        public bool IsCorrigendum {get; set;} = false;
        public bool IsCancelled {get; set;} = false;
        public bool CancelledReason {get; set;} = false;
        public ChangeConfiguration Changes {get; set;} = new ChangeConfiguration();
        public bool CorrigendumAdditionalInformation {get; set;} = false;
        public ContractAwardDefenceConfiguration ContractAwardsDefence {get; set;} = new ContractAwardDefenceConfiguration();
        public AnnexConfiguration Annexes {get; set;} = new AnnexConfiguration();
    }
}
