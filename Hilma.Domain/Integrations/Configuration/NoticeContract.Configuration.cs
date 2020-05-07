
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of NoticeContract for Ted integration
    /// </summary>
    public class NoticeContractConfiguration
    {
        
        public bool DateCreated {get; set;} = false;
        public bool DateModified {get; set;} = false;
        
        public bool Id {get; set;} = false;
        public bool ProcurementProjectId {get; set;} = false;
        public bool ParentId {get; set;} = false;
        public bool CorrigendumId {get; set;} = false;
        public bool NoticeNumber {get; set;} = false;
        public bool ReducedTimeLimitsForReceiptOfTenders {get; set;} = false;
        public bool CorrigendumAdditionalInformation {get; set;} = false;
        public bool CorrigendumPreviousNoticeNumber {get; set;} = false;
        public bool CreatorSystem {get; set;} = false;
        public bool CreatorId {get; set;} = false;
        public bool Type {get; set;} = false;
        public bool LegalBasis {get; set;} = false;
        public ProcurementProjectContractConfiguration Project {get; set;} = new ProcurementProjectContractConfiguration();
        public LotsInfoConfiguration LotsInfo {get; set;} = new LotsInfoConfiguration();
        public ObjectDescriptionConfiguration ObjectDescriptions {get; set;} = new ObjectDescriptionConfiguration();
        public CommunicationInformationConfiguration CommunicationInformation {get; set;} = new CommunicationInformationConfiguration();
        public ContactPersonConfiguration ContactPerson {get; set;} = new ContactPersonConfiguration();
        public ProcurementObjectConfiguration ProcurementObject {get; set;} = new ProcurementObjectConfiguration();
        public ConditionsInformationConfiguration ConditionsInformation {get; set;} = new ConditionsInformationConfiguration();
        public ConditionsInformationDefenceConfiguration ConditionsInformationDefence {get; set;} = new ConditionsInformationDefenceConfiguration();
        public ConditionsInformationNationalConfiguration ConditionsInformationNational {get; set;} = new ConditionsInformationNationalConfiguration();
        public ComplementaryInformationConfiguration ComplementaryInformation {get; set;} = new ComplementaryInformationConfiguration();
        public bool DatePublished {get; set;} = false;
        public bool State {get; set;} = false;
        public bool TedPublishState {get; set;} = false;
        public bool TedSubmissionId {get; set;} = false;
        public bool TedReasonCode {get; set;} = false;
        public bool TedPublishRequestSentDate {get; set;} = false;
        public TedPublicationInfoConfiguration TedPublicationInfo {get; set;} = new TedPublicationInfoConfiguration();
        public TedValidationReportConfiguration TedValidationErrors {get; set;} = new TedValidationReportConfiguration();
        public bool NoticeOjsNumber {get; set;} = false;
        public bool PreviousNoticeOjsNumber {get; set;} = false;
        public ProcedureInformationConfiguration ProcedureInformation {get; set;} = new ProcedureInformationConfiguration();
        public TenderingInformationConfiguration TenderingInformation {get; set;} = new TenderingInformationConfiguration();
        public RewardsAndJuryConfiguration RewardsAndJury {get; set;} = new RewardsAndJuryConfiguration();
        public ResultsOfContestConfiguration ResultsOfContest {get; set;} = new ResultsOfContestConfiguration();
        public bool TedNoDocExt {get; set;} = false;
        public bool Links {get; set;} = false;
        public AttachmentViewModelConfiguration Attachments {get; set;} = new AttachmentViewModelConfiguration();
        public bool HasAttachments {get; set;} = false;
        public ChangeConfiguration Changes {get; set;} = new ChangeConfiguration();
        public bool IsCorrigendum {get; set;} = false;
        public bool IsMigrated {get; set;} = false;
        public bool IsCancelled {get; set;} = false;
        public bool CancelledReason {get; set;} = false;
        public bool IsLatest {get; set;} = false;
        public bool Language {get; set;} = false;
        public ProceduresForReviewInformationConfiguration ProceduresForReview {get; set;} = new ProceduresForReviewInformationConfiguration();
        public AttachmentInformationConfiguration AttachmentInformation {get; set;} = new AttachmentInformationConfiguration();
        public ModificationsConfiguration Modifications {get; set;} = new ModificationsConfiguration();
        public ContractAwardDefenceConfiguration ContractAwardsDefence {get; set;} = new ContractAwardDefenceConfiguration();
        public HilmaStatisticsConfiguration HilmaStatistics {get; set;} = new HilmaStatisticsConfiguration();
        public AnnexConfiguration Annexes {get; set;} = new AnnexConfiguration();
        public ModifierConfiguration Modifiers {get; set;} = new ModifierConfiguration();
        public bool DepartmentId {get; set;} = false;
        public bool IsPrivateSmallValueProcurement {get; set;} = false;
    }
}
