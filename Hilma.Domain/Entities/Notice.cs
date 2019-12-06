using System;
using System.Collections.Generic;
using AutoMapper;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Enums;
using Newtonsoft.Json;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     Notices are created for procurement to notify markets
    /// </summary>
    public class Notice : BaseEntity
    {
        /// <summary>
        ///     Hilma generated primary key.
        /// </summary>
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Hilma notice number, formatting [year]-[id].
        /// Assigned by Hilma. Used as TED No Doc Ext.
        /// </summary>
        public string NoticeNumber { get; set; }

        /// <summary>
        ///     Type of notice according to EU taxonomy.
        /// </summary>
        public NoticeType Type { get; set; }

        /// <summary>
        /// The regulation number that is used as a legal basis for the notice
        /// </summary>
        public string LegalBasis { get; set; }
        /// <summary>
        ///     Publication status in Hilma.
        /// </summary>
        public PublishState State { get; set; }
        /// <summary>
        ///     Publication status in TED.
        /// </summary>
        public TedPublishState TedPublishState { get; set; }
        /// <summary>
        ///     Validation errors returned by TED, if any.
        /// </summary>
        public List<TedValidationReport> TedValidationErrors { get; set; }
        /// <summary>
        ///     Publication information returned by TED, if published in TED
        /// </summary>
        public TedPublicationInfo TedPublicationInfo { get; set; }
        /// <summary>
        ///     TED assigned submission id. Generated by their API.
        /// </summary>
        public string TedSubmissionId { get; set; }
        /// <summary>
        ///     Ted rejection code, if the publication to TED has been rejected.
        /// </summary>
        public string TedReasonCode { get; set; }
        /// <summary>
        ///     Ted assigned identifier for the notice. Generated by their Open Journal System.
        /// </summary>
        public string TedNoDocExt { get; set; }
        /// <summary>
        ///     Time when the request to publish in TED was sent.
        /// </summary>
        public DateTime? TedPublishRequestSentDate { get; set; }
        /// <summary>
        /// If notice is a corrigendum
        /// </summary>
        public bool IsCorrigendum { get; set; }

        public bool IsMigrated { get; set; }

        /// <summary>
        /// Changes related to corrigendum notice
        /// </summary>
        public List<Change> Changes { get; set; }

        /// <summary>
        /// If national notice has been cancelled
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Why the national notice has been cancelled
        /// </summary>
        public string[] CancelledReason { get; set; }

        /// <summary>
        /// If notice is the latest version. Needed for search functionality when notice has children.
        /// </summary>
        public bool IsLatest { get; set; }

        /// <summary>
        /// Is reduced time limits for receiving tenders applied to this notice. Only for Contact notices.
        /// </summary>
        public bool ReducedTimeLimitsForReceiptOfTenders { get; set; }

        /// <summary>
        ///     Navigational property project.
        /// </summary>
        public ProcurementProject ProcurementProject { get; set; }
        /// <summary>
        ///     Foreign key constraint to project.
        /// </summary>
        public int ProcurementProjectId { get; set; }
        /// <summary>
        ///     Creator of the notice, if created by an application user.
        /// </summary>
        public User Creator { get; set; }
        /// <summary>
        ///     Creator FK.
        /// </summary>
        public Guid? CreatorId { get; set; }
        /// <summary>
        ///     Creator of the notice, if created by Ets API user.
        /// </summary>
        public EtsUser EtsCreator { get; set; }
        /// <summary>
        ///     Ets Creator FK.
        /// </summary>
        public Guid? EtsCreatorId { get; set; }
        /// <summary>
        ///     Ets API user assigned surrogate key for this notice.
        /// </summary>
        public string EtsIdentifier { get; set; }
        /// <summary>
        ///     Parent notice, if this notice is based on another notice in ted.
        /// </summary>
        public Notice Parent { get; set; }
        /// <summary>
        ///     Navigational property for all the children of this notice.
        /// </summary>
        public List<Notice> Children { get; set; }
        /// <summary>
        ///     Parent FK.
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        ///     If notice is fixed, set its child FK.
        /// </summary>
        public int? CorrigendumId { get; set; }
        
        /// <summary>
        /// VII.2 Other additional information for why the corrigendum was made.
        /// </summary>
        //[StringMaxLength(6000)]
        public string[] CorrigendumAdditionalInformation { get; set; }

        /// <summary>
        /// OJS Number for published Ted notices.
        /// <example>2019/S 001-999999</example>
        /// </summary>
        //[RegularExpression(@"^(19|20)\d{2}/S (((00)?[1-9])|([0]?[1-9][0-9])|(1[0-9][0-9])|(2[0-5][0-9]))-\d{6}$",
        //    ErrorMessage = "Notice OJS Number must be correctly formatted")]
        public string NoticeOjsNumber { get; set; }

        /// <summary>
        /// If parent is set, should match the NoticeOjsNumber of the parent
        /// Previous submission to TED regarding the same procurement.
        /// <example>2019/S 001-999999</example>
        /// </summary>
        //[RegularExpression(@"^(19|20)\d{2}/S (((00)?[1-9])|([0]?[1-9][0-9])|(1[0-9][0-9])|(2[0-5][0-9]))-\d{6}$",
        //    ErrorMessage = "Previous notice OJS Number must be correctly formatted")]
        public string PreviousNoticeOjsNumber { get; set; }

        /// <summary>
        ///     Publication date in Hilma.
        /// </summary>
        public DateTime? DatePublished { get; set; }
        /// <summary>
        ///     Information regarding how to get in contact with the contracting
        ///     authority regarding the notice.
        /// </summary>
        public CommunicationInformation CommunicationInformation { get; set; } = new CommunicationInformation();
        /// <summary>
        ///     Assigned contact person for the notice.
        /// </summary>
        public ContactPerson ContactPerson { get; set; } = new ContactPerson();

        /// <summary>
        /// Update notice number. Notice id must have been set before
        /// </summary>
        public void UpdateNoticeNumber()
        {
            NoticeNumber = $"{DateCreated?.Year}-{(Id % 1000000):D6}";
        }

        /// <summary>
        ///     Procurement project information at the time of creation/update of the notice.
        ///     This is the information submitted, we do not want it updated without user
        ///     having knowledge of the fact.
        /// </summary>
        public ProcurementProjectContract Project { get; set; }

        /// <summary>
        ///     Main target of the procurement.
        /// </summary>
        public ProcurementObject ProcurementObject { get; set; } = new ProcurementObject();

        /// <summary>
        ///     Partitioning info.
        /// </summary>
        public LotsInfo LotsInfo { get; set; } = new LotsInfo();
        /// <summary>
        ///     Information about the partitions/lots. Always at least 1.
        /// </summary>
        public ObjectDescription[] ObjectDescriptions { get; set; } = { new ObjectDescription() };

        /// <summary>
        /// Legal, economic, financial and technical information
        /// </summary>
        public ConditionsInformation ConditionsInformation { get; set; } = new ConditionsInformation();

        /// <summary>
        /// Directive 2009/81/EC (Defence notices)
        /// Legal, economic, financial and technical information
        /// </summary>
        public ConditionsInformationDefence ConditionsInformationDefence { get; set; } = new ConditionsInformationDefence();

        /// <summary>
        /// National contracts only.
        /// Conditions for participation.
        /// </summary>
        public ConditionsInformationNational ConditionsInformationNational { get; set; } = new ConditionsInformationNational();

        /// <summary>
        /// IV.1
        /// Information about the procedure
        /// </summary>
        public ProcedureInformation ProcedureInformation { get; set; } = new ProcedureInformation();

        /// <summary>
        /// IV.2
        /// Information regarding tendering process
        /// </summary>
        public TenderingInformation TenderingInformation { get; set; } = new TenderingInformation();

        /// <summary>
        /// IV.3) Rewards and jury
        /// </summary>
        public RewardsAndJury RewardsAndJury { get; set; } = new RewardsAndJury();

        /// <summary>
        ///    Section VI: Miscellaneous information about the notice.
        /// </summary>
        public ComplementaryInformation ComplementaryInformation { get; set;} = new ComplementaryInformation();

        /// <summary>
        ///     VI.4) Procedures for review
        ///     Information about the review process
        /// </summary>
        public ProceduresForReviewInformation ProceduresForReview { get; set; } = new ProceduresForReviewInformation();

        /// <summary>
        /// Section VII: Modifications to the contract/concession
        /// </summary>
        public Modifications Modifications { get; set; } = new Modifications();

        /// <summary>
        /// List of attached files.
        /// </summary>
        [IgnoreMap]
        [JsonIgnore]
        public List<Attachment> Attachments { get; set; }

        public AttachmentInformation AttachmentInformation { get; set; } = new AttachmentInformation();

	    /// <summary>
        ///     Language for notice to be published in to TED.
        /// </summary>
	    public string Language { get; set; } = "FI";

        /// <summary>
        /// Directive 2009/81/EC (Defence notices)
        /// Section V: Award of contract
        /// </summary>
        public ContractAwardDefence[] ContractAwardsDefence { get; set; } = { new ContractAwardDefence() };

        /// <summary>
        /// Statistican information required by Hilma on all contract notices. (EU and national)
        /// </summary>
        public HilmaStatistics HilmaStatistics { get; set; } = new HilmaStatistics();

        /// <summary>
        /// Annex data container.
        /// </summary>
        public Annex Annexes { get; set; }

        /// <summary>
        ///     If this notice could be published.
        /// </summary>
        //[NotMapped]
        public bool CanEdit => State != PublishState.Published;

        
        /// <summary>
        ///     Attempt to set state to published.
        /// </summary>
        /// <returns></returns>
        public bool TryPublish()
        {
            if (State == PublishState.Published)
            {
                return false;
            }

            State = PublishState.Published;
            DatePublished = DateTime.UtcNow;
            return true;
        }
    }
}
