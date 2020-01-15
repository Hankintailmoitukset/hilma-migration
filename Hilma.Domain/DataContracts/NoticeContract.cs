using System;
using AutoMapper;
using System.Collections.Generic;
using AutoMapper.Attributes;
using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Data contract describing a notice to vuejs app.
    /// </summary>
    [MapsFrom(typeof(Notice))]
    [Contract]
    public class NoticeContract : BaseEntity
    {
        /// <summary>
        ///     Primary key of ne notice.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///     ProjectId of the project this notice belongs to.
        /// </summary>
        public int ProcurementProjectId { get; set; }
        /// <summary>
        ///     If the notice is parented, the id of the parent.
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        ///     If notice is fixed, set its child FK.
        /// </summary>
        public int? CorrigendumId { get; set; }

        /// <summary>
        /// Hilma notice number, formatting [year]-[id].
        /// Assigned by Hilma. Used as TED No Doc Ext.
        /// </summary>
        public string NoticeNumber { get; set; }

        /// <summary>
        /// Is reduced time limits for receiving tenders applied to this notice. Only for Contact notices.
        /// </summary>
        public bool ReducedTimeLimitsForReceiptOfTenders { get; set; }

        /// <summary>
        ///     VII.2) Other additional information for why the corrigendum was made.
        /// </summary>
        public string[] CorrigendumAdditionalInformation { get; set; } = new string[0];

        /// <summary>
        /// Notice number (no doc ext) of the previous notice for corrigendum
        /// </summary>
        public string CorrigendumPreviousNoticeNumber { get; set; }

        /// <summary>
        ///     Creator Id for user-created (as opposed to Ets API created) notices.
        /// </summary>
        public Guid? CreatorId { get; set; }
        /// <summary>
        ///     Type of the notice. See <see cref="NoticeType"/> for more details.
        /// </summary>
        public NoticeType Type { get; set; }
        /// <summary>
        /// The regulation number that is used as a legal basis for the notice
        /// </summary>
        public string LegalBasis { get; set; }

        /// <summary>
        ///     Project contract at the time of notice creation / update.
        /// </summary>
        public ProcurementProjectContract Project { get; set; }
        /// <summary>
        ///     II.1.6)  Information about lots
        ///     Details on partitioning of the notice.
        /// </summary>
        public LotsInfo LotsInfo { get; set; }
        /// <summary>
        ///     II.2) Description
        ///     The partitions of the notice. If not partitioned, the one partition contained
        ///     holds the information for entire notice.
        /// </summary>
        public ObjectDescription[] ObjectDescriptions { get; set; }
        /// <summary>
        ///     I.3) Communication
        ///     Information on how access information required to make an offer.
        /// </summary>
        public CommunicationInformation CommunicationInformation { get; set; }
        /// <summary>
        ///     I.1) Contact person
        /// </summary>
        public ContactPerson ContactPerson { get; set; }

        /// <summary>
        ///     Section II: Object
        /// </summary>
        public ProcurementObject ProcurementObject { get; set; }

        /// <summary>
        ///     Section III: Legal, economic, financial and technical information
        /// </summary>
        public ConditionsInformation ConditionsInformation { get; set; }

        /// <summary>
        ///     Directive 2009/81/EC (Defence contracts)
        ///     Section III: Legal, economic, financial and technical information
        /// </summary>
        public ConditionsInformationDefence ConditionsInformationDefence { get; set; }

        /// <summary>
        /// National contracts only.
        /// Conditions for participation.
        /// </summary>
        public ConditionsInformationNational ConditionsInformationNational { get; set; }

        /// <summary>
        ///     Section VI: Complementary information
        /// </summary>
        public ComplementaryInformation ComplementaryInformation { get; set; }
        /// <summary>
        ///     Hilma publication date.
        /// </summary>
        public DateTime? DatePublished { get; set; }
        /// <summary>
        ///     Status of publication to Hilma.
        /// </summary>
        public PublishState State { get; set; }
        /// <summary>
        ///     Status of publication to TED.
        /// </summary>
        public TedPublishState TedPublishState { get; set; }
        /// <summary>
        ///     TED assigned submission id for this notice.
        /// </summary>
        public string TedSubmissionId { get; set; }
        /// <summary>
        ///     Failure code from ted, in case publication has failed.
        /// </summary>
        public string TedReasonCode { get; set; }
        /// <summary>
        ///     Timestamp for initiation of ted publication process
        /// </summary>
        public DateTime? TedPublishRequestSentDate { get; set; }
        /// <summary>
        ///     Information regarding TED publication process
        /// </summary>
        public TedPublicationInfo TedPublicationInfo { get; set; }
        /// <summary>
        ///     Errors returned by TED
        /// </summary>
        public List<TedValidationReport> TedValidationErrors { get; set; }

        /// <summary>
        ///     Ted assigned identifier
        ///     Ojs: Open Journal System
        /// </summary>
        public string NoticeOjsNumber { get; set; }

        /// <summary>
        ///     Ted assigned identifier of the previous notice, if this one
        ///     is a continuation. Automatically filled, if created as child,
        ///     manually filled for standalone notices, that require it.
        /// </summary>
        [CorrigendumLabel("number_oj", "IV.2.1")]
        public string PreviousNoticeOjsNumber { get; set; }

        /// <summary>
        ///     IV.1) Description
        ///     Information about the procurement procedure
        /// </summary>
        public ProcedureInformation ProcedureInformation { get; set; }

        /// <summary>
        ///     IV.2) Administrative information
        /// </summary>
        public TenderingInformation TenderingInformation { get; set; }

        /// <summary>
        /// IV.3) Rewards and jury
        /// </summary>
        public RewardsAndJury RewardsAndJury { get; set; }

        /// <summary>
        /// Section V: Results of contest
        /// </summary>
        public ResultsOfContest ResultsOfContest { get; set; }

        /// <summary>
        /// Another TED assigned identifier. They never end.
        /// </summary>
        public string TedNoDocExt { get; set; }

        /// <summary>
        ///     List of links attached to this notice.
        /// </summary>
        public string[] Links { get; set; }
        /// <summary>
        ///     Attachments for this notice, as SAS-links.
        /// </summary>
        [IgnoreMap]
        public AttachmentViewModel[] Attachments { get; set; }

        /// <summary>
        ///     Notice changes for corrigendum notice
        /// </summary>
        public List<Change> Changes { get; set; }

        /// <summary>
        /// If notice is a corrigendum
        /// </summary>
        public bool IsCorrigendum { get; set; }

        /// <summary>
        /// Set to true if notice is migrated from previous Hilma
        /// </summary>
        public bool IsMigrated { get; set; }

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
        ///     Language for notice to be published in to TED.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        ///     VI.4) Procedures for review
        ///     Information about the review process
        /// </summary>
        public ProceduresForReviewInformation ProceduresForReview { get; set; }

        /// <summary>
        /// Additional information for the notice
        /// </summary>
        public AttachmentInformation AttachmentInformation { get; set; }

        /// <summary>
        /// Section VII: Modifications to the contract/concession
        /// </summary>
        public Modifications Modifications { get; set; }

        /// <summary>
        /// Directive 2009/81/EC (Defence notices)
        /// Section V: Award of contract
        /// </summary>
        public ContractAwardDefence[] ContractAwardsDefence { get; set; }

        /// <summary>
        /// Statistics information required by Hilma on all contract notices. (EU and national)
        /// </summary>
        public HilmaStatistics HilmaStatistics { get; set; }

        /// <summary>
        ///     AD1-AD4) Contains annex sections.
        /// </summary>
        public Annex Annexes { get; set; }

        /// <summary>
        /// Modification information. For Hilma use only
        /// </summary>
        public Modifier[] Modifiers { get; set; }
    }
}
