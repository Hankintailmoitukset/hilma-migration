using System.Collections.Generic;
using System.Linq;
using Hilma.Domain.DataContracts;
using Hilma.Domain.DataContracts.EtsContracts;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Entities
{
    public static class NoticeExtensions
    {

        /// <summary>
        ///     Creates a notice entity from Ets api dto. Sets front-end validation to all valid.
        /// </summary>
        /// <param name="dto">The data from API</param>
        /// <param name="etsId">API user assigned surrogate key</param>
        /// <returns></returns>
        public static Notice CreateNotice(this EtsNoticeContract dto, string etsId)
        {
            var rv = new Notice
            {
                Type = dto.Type,
                LegalBasis = dto.LegalBasis,
                EtsIdentifier = etsId,
                ComplementaryInformation = dto.ComplementaryInformation,
                ConditionsInformation = dto.ConditionsInformation,
                ConditionsInformationDefence = dto.ConditionsInformationDefence,
                ConditionsInformationNational = dto.ConditionsInformationNational,
                PreviousNoticeOjsNumber = dto.PreviousNoticeOjsNumber,
                NoticeOjsNumber = dto.NoticeOjsNumber,
                TenderingInformation = dto.TenderingInformation,
                RewardsAndJury = dto.RewardsAndJury,
                ProcedureInformation = dto.ProcedureInformation,
                CommunicationInformation = dto.CommunicationInformation,
                ContactPerson = dto.ContactPerson,
                ProcurementObject = new ProcurementObject
                {
                    ShortDescription = dto.ShortDescription,
                    EstimatedValue = dto.EstimatedValue,
                    TotalValue = dto.TotalValue,
                    MainCpvCode = dto.MainCpvCode,
                    Defence = dto.Defence
                },
                LotsInfo = dto.LotsInfo,
                ObjectDescriptions = dto.ObjectDescriptions,
                Project = new ProcurementProjectContract
                {
                    Title = dto.Project.Title,
                    ContractType = dto.Project.ContractType,
                    ReferenceNumber = dto.Project.ReferenceNumber,
                    State = PublishState.Draft,
                    CoPurchasers = dto.Project.CoPurchasers,
                    Organisation = new OrganisationContract
                    {
                        Information = dto.Organisation.Information,
                        ContractingAuthorityType = dto.Organisation.ContractingAuthorityType,
                        OtherContractingAuthorityType = dto.Organisation.OtherContractingAuthorityType,
                        MainActivity = dto.Organisation.MainActivity,
                        OtherMainActivity = dto.Organisation.OtherMainActivity,
                    },
                    DefenceCategory = dto.Project.DefenceCategory,
                    DefenceSupplies = dto.Project.DefenceSupplies,
                    DefenceWorks = dto.Project.DefenceWorks,
                    ProcurementCategory = dto.Project.ProcurementCategory,
                    ProcurementLaw = dto.Project.ProcurementLaw,
                    CentralPurchasing = dto.Project.CentralPurchasing,
                    JointProcurement = dto.Project.JointProcurement,
                    Publish = NoticeTypeExtensions.IsNational(dto.Type) ? PublishType.ToHilma : PublishType.ToTed,
                    AgricultureWorks = dto.Project.AgricultureWorks
                },
                ProceduresForReview = dto.ProceduresForReview,
                State = PublishState.Draft,
                TedPublishState = TedPublishState.Undefined,
                IsLatest = true,
                AttachmentInformation = new AttachmentInformation()
                {
                    Links = dto.Links ?? new Link[0]
                },
                IsCorrigendum = dto.IsCorrigendum,
                Annexes = dto.Annexes,
                CorrigendumAdditionalInformation = dto.CorrigendumAdditionalInformation,
                Language = dto.Language
            };

            return rv;
        }

        /// <summary>
        /// Canceling a national notice
        /// </summary>
        /// <param name="parentDto">Parent dto</param>
        /// <param name="etsId">Ets identifier</param>
        /// <returns>The notice</returns>
        public static Notice CancelNotice(this EtsNoticeContract parentDto, string etsId)
        {
            var notice = parentDto.CreateNotice(etsId);
            notice.IsCancelled = true;
            notice.IsCorrigendum = false;
            notice.CancelledReason = parentDto.CancelledReason;
            notice.TenderingInformation.TendersOrRequestsToParticipateDueDateTime = null;
            notice.TenderingInformation.EstimatedDateOfContractNoticePublication = null;
            notice.TenderingInformation.EstimatedDateOfInvitations = null;
            return notice;
        }

        public static Notice Update(this Notice noticeEntity, NoticeContract dto)
        {
            noticeEntity.Language = dto.Language;
            noticeEntity.IsCorrigendum = dto.IsCorrigendum;
            noticeEntity.ReducedTimeLimitsForReceiptOfTenders = dto.ReducedTimeLimitsForReceiptOfTenders;
            noticeEntity.CorrigendumAdditionalInformation = dto.CorrigendumAdditionalInformation;
            noticeEntity.IsLatest = dto.IsLatest;
            noticeEntity.CommunicationInformation = dto.CommunicationInformation;
            noticeEntity.Project = dto.Project;
            noticeEntity.ContactPerson = dto.ContactPerson;
            noticeEntity.ProcurementObject = dto.ProcurementObject;
            noticeEntity.ComplementaryInformation = dto.ComplementaryInformation;
            noticeEntity.TedPublishState = dto.TedPublishState;
            noticeEntity.ObjectDescriptions = dto.ObjectDescriptions;

            noticeEntity.ConditionsInformation = dto.ConditionsInformation;
            noticeEntity.ConditionsInformationDefence = dto.ConditionsInformationDefence;
            noticeEntity.ConditionsInformationNational = dto.ConditionsInformationNational;

            noticeEntity.ProcedureInformation = dto.ProcedureInformation;
            noticeEntity.ProceduresForReview = dto.ProceduresForReview;
            noticeEntity.LotsInfo = dto.LotsInfo;
            noticeEntity.Modifications = dto.Modifications;
            // Might not be ok to let previous submission Id to be changed, might change!
            noticeEntity.PreviousNoticeOjsNumber = dto.PreviousNoticeOjsNumber;
            
            noticeEntity.TenderingInformation = dto.TenderingInformation;
            noticeEntity.RewardsAndJury = dto.RewardsAndJury;
            noticeEntity.AttachmentInformation = dto.AttachmentInformation;
            noticeEntity.ContractAwardsDefence = dto.ContractAwardsDefence;
            noticeEntity.HilmaStatistics = dto.HilmaStatistics;
            noticeEntity.Annexes = dto.Annexes;
 
            return noticeEntity;
        }

    }
}
