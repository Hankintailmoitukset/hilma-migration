
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of EtsNoticeSummary for Ted integration
    /// </summary>
    public class EtsNoticeSummaryConfiguration
    {
        
        
        public bool NoticeNumber {get; set;} = false;
        public bool NoticeOjsNumber {get; set;} = false;
        public bool CreationDate {get; set;} = false;
        public bool ModificationDate {get; set;} = false;
        public bool HilmaStatus {get; set;} = false;
        public bool HilmaPublicationDate {get; set;} = false;
        public bool EtsIdentifier {get; set;} = false;
        public bool OrganisationId {get; set;} = false;
        public bool ProjectId {get; set;} = false;
        public bool NoticeId {get; set;} = false;
        public EtsTedPublicationInfoConfiguration TedPublicationInfo {get; set;} = new EtsTedPublicationInfoConfiguration();
        public bool TedStatus {get; set;} = false;
        public bool TedSubmissionId {get; set;} = false;
        public TedValidationReportConfiguration TedValidationReport {get; set;} = new TedValidationReportConfiguration();
        public EtsNoticeContractConfiguration Notice {get; set;} = new EtsNoticeContractConfiguration();
    }
}
