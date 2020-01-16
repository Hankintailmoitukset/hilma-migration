using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
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
            // NOTE(JanneF): If you edit this functionality, please also update /common/services/documentation.ts documentation generator!
            var rv = new Notice
            {
                // 1:1
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
                ResultsOfContest = dto.ResultsOfContest,
                ProcedureInformation = dto.ProcedureInformation,
                CommunicationInformation = dto.CommunicationInformation,
                ContactPerson = dto.ContactPerson,

                LotsInfo = dto.LotsInfo,
                ObjectDescriptions = dto.ObjectDescriptions ?? new ObjectDescription[0],
                IsCorrigendum = dto.IsCorrigendum,
                IsCancelled = dto.IsCancelled,
                CancelledReason =  dto.CancelledReason,
                Annexes = dto.Annexes,
                CorrigendumAdditionalInformation = dto.CorrigendumAdditionalInformation,
                Language = dto.Language,
                ProceduresForReview = dto.ProceduresForReview,

                // Flattened stuff
                ProcurementObject = new ProcurementObject
                {
                    ShortDescription = dto.ShortDescription,
                    EstimatedValue = dto.EstimatedValue,
                    EstimatedValueCalculationMethod = dto.EstimatedValueCalculationMethod,
                    TotalValue = dto.TotalValue,
                    MainCpvCode = dto.MainCpvCode,
                    Defence = dto.Defence
                },
                Project = new ProcurementProjectContract
                {
                    Title = dto.Project?.Title,
                    ContractType = dto.Project?.ContractType ?? ContractType.Undefined,
                    ReferenceNumber = dto.Project?.ReferenceNumber,
                    State = PublishState.Draft,
                    CoPurchasers = dto.Project?.CoPurchasers,
                    Organisation = new OrganisationContract
                    {
                        Information = dto.Organisation?.Information,
                        ContractingAuthorityType = dto.Organisation?.ContractingAuthorityType ?? ContractingAuthorityType.Undefined,
                        OtherContractingAuthorityType = dto.Organisation?.OtherContractingAuthorityType,
                        ContractingType = dto.Organisation?.ContractingType ?? ContractingType.Undefined,
                        MainActivity = dto.Organisation?.MainActivity ?? MainActivity.Undefined,
                        OtherMainActivity = dto.Organisation?.OtherMainActivity,
                        MainActivityUtilities = dto.Organisation?.MainActivityUtilities ?? MainActivityUtilities.Undefined
                    },
                    DefenceCategory = dto.Project?.DefenceCategory,
                    DisagreeToPublishNoticeBasedOnDefenceServiceCategory4 = dto.Project?.DisagreeToPublishNoticeBasedOnDefenceServiceCategory4,
                    DefenceSupplies = dto.Project?.DefenceSupplies ?? Supplies.Undefined,
                    DefenceWorks = dto.Project?.DefenceWorks ?? Works.Undefined,
                    ProcurementCategory = GetProcurementCategory( dto.LegalBasis, dto.Project?.ProcurementCategory ?? ProcurementCategory.Undefined ),
                    ProcurementLaw = dto.Project?.ProcurementLaw ?? new string[0],
                    CentralPurchasing = dto.Project?.CentralPurchasing ?? false,
                    JointProcurement = dto.Project?.JointProcurement ?? false,
                    Publish = dto.Type.IsNational() ? PublishType.ToHilma : PublishType.ToTed,
                    AgricultureWorks = dto.Project.AgricultureWorks
                },
                AttachmentInformation = new AttachmentInformation()
                {
                    Links = dto.Links ?? new Link[0]
                },

                // mandatory setup
                State = PublishState.Draft,
                TedPublishState = TedPublishState.Undefined,
                IsLatest = true,
            };

            return rv;
        }

        private static ProcurementCategory GetProcurementCategory(string legalBasis, ProcurementCategory procurementCategory)
        {
            if (procurementCategory != ProcurementCategory.Undefined)
                return procurementCategory;
            
            switch (legalBasis)
            {
                case "32009L0081":
                    return  ProcurementCategory.Defence;
                case "32014L0023":
                    return ProcurementCategory.Lisence;
                case "32014L0025":
                    return ProcurementCategory.Utility;
                case "32014L0024":
                    return ProcurementCategory.Public;
                default:
                    return ProcurementCategory.Public;
            }
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
            noticeEntity.IsCancelled = dto.IsCancelled;
            noticeEntity.CancelledReason = dto.CancelledReason;
            noticeEntity.ReducedTimeLimitsForReceiptOfTenders = dto.ReducedTimeLimitsForReceiptOfTenders;
            noticeEntity.CorrigendumAdditionalInformation = dto.CorrigendumAdditionalInformation;
            noticeEntity.IsLatest = dto.IsLatest;
            noticeEntity.CommunicationInformation = dto.CommunicationInformation;
            noticeEntity.Project = dto.Project;
            noticeEntity.ContactPerson = dto.ContactPerson;
            noticeEntity.ProcurementObject = dto.ProcurementObject;
            noticeEntity.ComplementaryInformation = dto.ComplementaryInformation;
            noticeEntity.TedPublishState = dto.TedPublishState;
            noticeEntity.ObjectDescriptions = dto.ObjectDescriptions ?? new ObjectDescription[0];

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
            noticeEntity.ResultsOfContest = dto.ResultsOfContest;
            noticeEntity.HilmaStatistics = dto.HilmaStatistics;
            noticeEntity.Annexes = dto.Annexes;
 
            return noticeEntity;
        }

    }
}
