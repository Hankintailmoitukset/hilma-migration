using System;
using System.Collections.Generic;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;

namespace Hilma.Domain.DataContracts.EtsContracts
{
    /// <summary>
    ///     Contract for creating notices via Ets API
    /// </summary>
    public class EtsNoticeContract
    {
        /// <summary>
        /// Internal identifier of created Hilma notice. Cannot be assigned on creations.
        /// This identifier can be user to generate URL to public notice in Hilma
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///     Default empty constructor for mappers.
        /// </summary>
        public EtsNoticeContract() { }

        /// <summary>
        /// Create EtsNoticeContract from Notice dbo
        /// </summary>
        /// <param name="dbo"></param>
        public EtsNoticeContract(Notice dbo)
        {
            Id = dbo.Id;
            CommunicationInformation = dbo.CommunicationInformation;
            ComplementaryInformation = dbo.ComplementaryInformation;
            ConditionsInformation = dbo.ConditionsInformation;
            ConditionsInformationDefence = dbo.ConditionsInformationDefence;
            ContactPerson = dbo.ContactPerson;
            EstimatedValue = dbo.ProcurementObject.EstimatedValue;
            LotsInfo = dbo.LotsInfo;
            MainCpvCode = dbo.ProcurementObject.MainCpvCode;
            ObjectDescriptions = dbo.ObjectDescriptions;
            NoticeOjsNumber = dbo.NoticeOjsNumber;
            PreviousNoticeOjsNumber = dbo.PreviousNoticeOjsNumber;

            Project = new EtsProjectContract {
                Id = dbo.Project.Id,
                ContractType = dbo.Project.ContractType,
                ReferenceNumber = dbo.Project.ReferenceNumber,
                Title = dbo.Project.Title,
                CentralPurchasing = dbo.Project.CentralPurchasing,
                DefenceCategory = dbo.Project.DefenceCategory,
                DefenceSupplies = dbo.Project.DefenceSupplies,
                DefenceWorks = dbo.Project.DefenceWorks,
                JointProcurement = dbo.Project.JointProcurement,
                ProcurementCategory = dbo.Project.ProcurementCategory,
                ProcurementLaw = dbo.Project.ProcurementLaw,
                AgricultureWorks = dbo.Project.AgricultureWorks,
                CoPurchasers = dbo.Project.CoPurchasers
            };
            Organisation = new EtsOrganisationContract
            {
                Information = dbo.Project.Organisation.Information,
                ContractingAuthorityType = dbo.Project.Organisation.ContractingAuthorityType,
                MainActivity = dbo.Project.Organisation.MainActivity,
                OtherContractingAuthorityType = dbo.Project.Organisation.OtherContractingAuthorityType,
                OtherMainActivity = dbo.Project.Organisation.OtherMainActivity
            };
            ShortDescription = dbo.ProcurementObject.ShortDescription;
            TenderingInformation = dbo.TenderingInformation;
            RewardsAndJury = dbo.RewardsAndJury;
            ProcedureInformation = dbo.ProcedureInformation;
            ProceduresForReview = dbo.ProceduresForReview;
            Modifications = dbo.Modifications;
            Type = dbo.Type;
            LegalBasis = dbo.LegalBasis;
            Language = dbo.Language;
            Links = dbo.AttachmentInformation.Links;
            Defence = dbo.ProcurementObject.Defence;
            IsCorrigendum = dbo.IsCorrigendum;
            Changes = dbo.Changes;
            CorrigendumAdditionalInformation = dbo.CorrigendumAdditionalInformation;
            ContractAwardsDefence = dbo.ContractAwardsDefence;
        }

        /// <summary>
        /// The regulation number that is used as a legal basis for the notice
        /// </summary>
        public string LegalBasis { get; set; }

        /// <summary>
        ///     I.3) Communication
        ///     Information regarding communication about the notice in question.
        /// </summary>
        public CommunicationInformation CommunicationInformation { get; set; } = new CommunicationInformation();

        /// <summary>
        ///     Section VI: Complementary information
        ///     Complementary information regarding procurement
        /// </summary>
        public ComplementaryInformation ComplementaryInformation { get; set; } = new ComplementaryInformation();

        /// <summary>
        ///     Section III: Legal, economic, financial and technical information
        ///     Legal, economic, financial and technical requirements to participate.
        /// </summary>
        public ConditionsInformation ConditionsInformation { get; set; } = new ConditionsInformation();

        /// <summary>
        ///     Directive 2009/81/EC
        ///     Section III: Legal, economic, financial and technical information
        ///     Legal, economic, financial and technical requirements to participate.
        /// </summary>
        public ConditionsInformationDefence ConditionsInformationDefence { get; set; } = new ConditionsInformationDefence();

        /// <summary>
        /// National contracts only.
        /// Conditions for participation.
        /// </summary>
        public ConditionsInformationNational ConditionsInformationNational { get; set; } = new ConditionsInformationNational();

        /// <summary>
        ///     I.1) Name and addresses
        ///     Nominated contact person for this notice.
        /// </summary>
        public ContactPerson ContactPerson { get; set; } = new ContactPerson();


        /// <summary>
        ///     II.1.5) Estimated total value
        ///     Estimated monetary value of the tender described by this notice.
        /// </summary>
        public ValueRangeContract EstimatedValue { get; set; } = new ValueRangeContract();

        /// <summary>
        ///     List of link URLs, including protocol. Displayed in attachments section
        ///     of the notice as links to external resource.
        /// </summary>
        public Link[] Links { get; set; } = new Link[0];

        /// <summary>
        ///     II.1.6) Information about lots
        ///     Information about how this tender is partitioned.
        /// </summary>
        public LotsInfo LotsInfo { get; set; } = new LotsInfo();

        /// <summary>
        ///     II.1.2) Main CPV code
        ///     Information about classification of goods, works or services that are target
        ///     of the tendering described in this notice.
        /// </summary>
        public CpvCode MainCpvCode { get; set; }

        /// <summary>
        ///     II.2) Description
        ///     Details about the desired goods/works/services. If does defined as partitioned
        ///     in <see cref="LotsInfo" /> section, should contain exactly one description
        ///     for the whole tender. If defined as partitioned in, number of objects neets to
        ///     match number of lots defined in <see cref="LotsInfo" />.
        /// </summary>
        public ObjectDescription[] ObjectDescriptions { get; set; } = new ObjectDescription[0];

        /// <summary>
        ///     I.1) Name and addresses
        ///     Information about the tendering organisation.
        /// </summary>
        public EtsOrganisationContract Organisation { get; set; } = new EtsOrganisationContract();

        /// <summary>
        /// OJS Number for published Ted notices.
        /// <example>2019/S 001-999999</example>
        /// Can be null. 
        /// </summary>
        public string NoticeOjsNumber { get; set; }

        /// <summary>
        ///     IV.2.1) Previous publication concerning this procedure
        ///     If this tender is related to a tender previously published in TED, the TED submissionId
        ///     of that previous TED must be given.
        ///     If tender is not related, leave as null. If the previous tender was described by a notice
        ///     published via new Hilma or Hilma Ets API, parenting the new notice to that notice
        ///     will take care of filling the submission ID automatically.
        /// </summary>
        public string PreviousNoticeOjsNumber { get; set; }

        /// <summary>
        ///     Section II: Object
        ///     Information about the procurement this notice is part of. In Hilma, all Notices belong to a
        ///     project that links Notices concerning same procurement together. If creating a new project,
        ///     the project is created based on this information. If attaching the notice to existing project,
        ///     the project is updated with details given here.
        /// </summary>
        public EtsProjectContract Project { get; set; } = new EtsProjectContract();

        /// <summary>
        ///     Sales pitch for the tender to get vendors interested in making an offer.
        /// </summary>
        public string[] ShortDescription { get; set; }

        /// <summary>
        ///     II.1.3) Type of contract
        ///     Type of notice described in this dto.
        /// </summary>
        public NoticeType Type { get; set; }

        /// <summary>
        ///     IV.2) Administrative information
        ///     Describes the tendering process.
        /// </summary>
        public TenderingInformation TenderingInformation { get; set; } = new TenderingInformation();

        /// <summary>
        /// IV.3) Rewards and jury
        /// </summary>
        public RewardsAndJury RewardsAndJury { get; set; } = new RewardsAndJury();

        /// <summary>
        /// Language in which the notice is published. Works with FI, SV or EN
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Section IV: Procedure
        /// Information about the procurement procedure
        /// </summary>
        public ProcedureInformation ProcedureInformation { get; set; } = new ProcedureInformation() { FrameworkAgreement = new FrameworkAgreementInformation() };

        /// <summary>
        ///     VI.4) Procedures for review
        ///     Information about the review process
        /// </summary>
        public ProceduresForReviewInformation ProceduresForReview { get; set; } = new ProceduresForReviewInformation() { ReviewBody = new ContractBodyContactInformation() };

        /// <summary>
        /// Section VII: Modifications to the contract/concession
        /// </summary>
        public Modifications Modifications { get; set; } = new Modifications();

        /// <summary>
        /// In case of defence contract (Directive 2009/81/EC), additional fields will be set here.
        /// </summary>
        public ProcurementObjectDefence Defence { get; set; } = new ProcurementObjectDefence();

        /// <summary>
        /// If corrigenduming a notice.
        /// </summary>
        public bool IsCorrigendum { get; set; }

        /// <summary>
        /// If the national notice should be cancelled
        /// Provide parent id
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Why the national notice has been cancelled
        /// </summary>
        public string[] CancelledReason { get; set; }

        /// <summary>
        /// Corrigendum notice changes are populated by Hilma.
        /// </summary>
        public List<Change> Changes { get; set; } = new List<Change>();

        /// <summary>
        /// VII.2 Other additional information for why the corrigendum was made.
        /// </summary>
        public string[] CorrigendumAdditionalInformation { get; set; }

        /// <summary>
        /// Directive 2009/81/EC (Defence notices)
        /// Section V: Award of contract
        /// </summary>
        public ContractAwardDefence[] ContractAwardsDefence { get; set; } = { new ContractAwardDefence() };

        /// <summary>
        ///     AD1-AD4) Contains annex sections AD1-AD4.
        /// </summary>
        public Annex Annexes { get; set; }
    }
}
