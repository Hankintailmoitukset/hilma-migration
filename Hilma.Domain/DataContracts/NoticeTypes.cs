using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// Extension class for notice types
    /// </summary>
    [Configuration]
    public class NoticeTypes
    {
        public NoticeType[] SupportNoticeTypes { get; } =
        {
            NoticeType.PriorInformation,
            NoticeType.PriorInformationReduceTimeLimits,
            NoticeType.PeriodicIndicativeUtilities,
            NoticeType.SocialUtilitiesPriorInformation,
            NoticeType.Contract,
            NoticeType.ContractAward,
            NoticeType.ContractAwardUtilities,
            NoticeType.ContractUtilities,
            NoticeType.Modification,
            NoticeType.SocialContract,
            NoticeType.SocialContractAward,
            NoticeType.DefenceContract,
            NoticeType.ExAnte,
            NoticeType.SocialUtilities,
            NoticeType.DefencePriorInformation,
            NoticeType.SocialPriorInformation,
            NoticeType.DefenceContractAward,
            NoticeType.NationalDirectAward,
            NoticeType.NationalPriorInformation,
            NoticeType.NationalContract,
            NoticeType.NationalAgricultureContract,
            NoticeType.DesignContest,
            NoticeType.NationalDesignContest,
            NoticeType.NationalDefencePriorInformation,
            NoticeType.NationalDefenceContract,
            NoticeType.PriorInformationReduceTimeLimits,
            NoticeType.Concession
        };

        public NoticeType[] PublicNotices { get; } = new[] {
            NoticeType.PriorInformation,
            NoticeType.Contract,
            NoticeType.ContractAward,
            NoticeType.Modification,
            NoticeType.SocialPriorInformation
        };

        /// <summary>
        /// National notices
        /// </summary>
        public NoticeType[] NationalNotices { get; } = new[] {
            NoticeType.NationalPriorInformation,
            NoticeType.NationalContract,
            NoticeType.NationalDesignContest,
            NoticeType.NationalDirectAward,
            NoticeType.NationalExAnte,
            NoticeType.NationalDefencePriorInformation,
            NoticeType.NationalDefenceContract,
            NoticeType.NationalAgricultureContract
        };

        /// <summary>
        /// Prior information notice types
        /// </summary>
        public NoticeType[] PriorInformationNotices { get; } = new[] {
            NoticeType.PriorInformation,
            NoticeType.PriorInformationReduceTimeLimits,
            NoticeType.PeriodicIndicativeUtilities,
            NoticeType.DefencePriorInformation,
            NoticeType.SocialPriorInformation,
            NoticeType.SocialUtilitiesPriorInformation,
            NoticeType.NationalPriorInformation
        };

        /// <summary>
        /// Contract notice types
        /// </summary>
        public NoticeType[] ContractNotices { get; } = new[] {
            NoticeType.Contract,
            NoticeType.ContractUtilities,
            NoticeType.DefenceContract,
            NoticeType.SocialContract,
            NoticeType.SocialUtilities,
            NoticeType.NationalContract,
            NoticeType.NationalAgricultureContract
        };

        /// <summary>
        /// Contract Award notice types
        /// </summary>
        public NoticeType[] ContractAwardNotices { get; } = new[] {
            NoticeType.ContractAward,
            NoticeType.ContractAwardUtilities,
            NoticeType.SocialContractAward,
            NoticeType.DefenceContractAward,
            NoticeType.ConcessionAward
        };

        /// <summary>
        /// Utilities notice types
        /// </summary>
        public  NoticeType[] UtilitiesNotices { get; } = new[] {
            NoticeType.PeriodicIndicativeUtilities,
            NoticeType.PeriodicIndicativeUtilitiesReduceTimeLimits,
            NoticeType.ContractAwardUtilities,
            NoticeType.ContractUtilities,
            NoticeType.QualificationSystemUtilities,
            NoticeType.SocialUtilities,
            NoticeType.SocialUtilitiesPriorInformation
        };

        /// <summary>
        /// Social notice types
        /// </summary>
        public NoticeType[] SocialNotices { get; } = new[] {
            NoticeType.SocialContract,
            NoticeType.SocialUtilities,
            NoticeType.SocialPriorInformation,
            NoticeType.SocialContractAward,
            NoticeType.SocialConcessions,
            NoticeType.SocialUtilitiesPriorInformation
        };

        /// <summary>
        /// Defence notices
        /// </summary>
        public  NoticeType[] DefenceNotices { get; } = new[] {
            NoticeType.DefenceConcession,
            NoticeType.DefencePriorInformation,
            NoticeType.DefenceContract,
            NoticeType.DefenceContractAward,
            NoticeType.DefenceContractConcessionnaire,
            NoticeType.DefenceContractSub,
            NoticeType.DefenceSimplifiedContract,
            NoticeType.NationalDefencePriorInformation,
            NoticeType.NationalDefenceContract
        };

    }
    
}
