using Hilma.Domain.DataContracts;
using Hilma.Domain.Integrations.Configuration;
using System;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static ContractBodyContactInformationConfiguration ContractBodyContactInformationConfigurationDefault
        {
            get
            {
                return new ContractBodyContactInformationConfiguration()
                {
                    OfficialName = true,
                    Email = true,
                    NutsCodes = true,
                    MainUrl = true,
                    PostalAddress = new PostalAddressConfiguration()
                    {
                        Town = true,
                        Country = true,
                        PostalCode = true,
                        StreetAddress = true
                    },
                    TelephoneNumber = true
                };
            }
        }

        private static LotsInfoConfiguration LotsInfoConfigurationDefault
        {
            get
            {
                return new LotsInfoConfiguration()
                {
                    DivisionLots = true,
                    QuantityOfLots = true,
                    LotsMaxAwarded = true,
                    LotsMaxAwardedQuantity = true,
                    LotsSubmittedFor = true,
                    LotsSubmittedForQuantity = true,
                    LotCombinationPossible = true,
                    LotCombinationPossibleDescription = true
                };
            }
        }
        private static ProcurementProjectContractConfiguration BasicProjectConfiguration
        {
            get
            {
                return
                new ProcurementProjectContractConfiguration()
                {
                    Title = true,
                    ReferenceNumber = true,
                    ContractType = true,
                    CentralPurchasing = true,
                    JointProcurement = true,
                    ProcurementLaw = true,
                    CoPurchasers = new ContractBodyContactInformationConfiguration()
                    {
                        ContactPerson = true,
                        Department = true,
                        Email = true,
                        MainUrl = true,
                        NationalRegistrationNumber = true,
                        NutsCodes = true,
                        OfficialName = true,
                        PostalAddress = new PostalAddressConfiguration()
                        {
                            Country = true,
                            PostalCode = true,
                            StreetAddress = true,
                            Town = true
                        },
                        TelephoneNumber = true
                    },
                    Organisation = new OrganisationContractConfiguration()
                    {
                        Id = true,
                        ContractingAuthorityType = true,
                        Information = new ContractBodyContactInformationConfiguration()
                        {
                            ContactPerson = true,
                            Department = true,
                            Email = true,
                            MainUrl = true,
                            NationalRegistrationNumber = true,
                            NutsCodes = true,
                            OfficialName = true,
                            PostalAddress = new PostalAddressConfiguration()
                            {
                                Country = true,
                                PostalCode = true,
                                StreetAddress = true,
                                Town = true
                            },
                            TelephoneNumber = true
                        },
                        MainActivity = true,
                        OtherContractingAuthorityType = true,
                        OtherMainActivity = true
                    }
                };
            }
        }
        /// <summary>
        /// Create TED configuration for notice
        /// </summary>
        /// <param name="notice"></param>
        /// <returns></returns>
        public static NoticeContractConfiguration CreateConfiguration(NoticeContract notice)
        {
            switch ( notice.Type)
            {
                case Enums.NoticeType.PriorInformation:
                    return PriorNotice;
                case Enums.NoticeType.PriorInformationReduceTimeLimits:
                    return PriorNoticeReducedTime;
                case Enums.NoticeType.PeriodicIndicativeUtilities:
                    return PriorNoticeUtilities;
                case Enums.NoticeType.PeriodicIndicativeUtilitiesReduceTimeLimits:
                    return PriorNoticeReducedTimeUtilities;
                case Enums.NoticeType.SocialUtilitiesPriorInformation:
                    return PriorNoticeSocialUtilities;
                case Enums.NoticeType.Contract:
                    return ContractNotice;
                case Enums.NoticeType.ContractAward:
                    return ContractAward;
                case Enums.NoticeType.SocialContract:
                    return SocialContract;
                case Enums.NoticeType.SocialContractAward:
                    return SocialContractAward;
                case Enums.NoticeType.DefenceContract:
                    return DefenceContractNotice;
                case Enums.NoticeType.ContractUtilities:
                    return ContractNoticeUtilities;
                case Enums.NoticeType.SocialUtilities:
                    return SocialUtilities;
                case Enums.NoticeType.DefencePriorInformation:
                    return DefencePriorInformation;
                case Enums.NoticeType.SocialPriorInformation:
                    return SocialPriorInformation;
                case Enums.NoticeType.ContractAwardUtilities:
                    return ContractAwardUtilities;
                case Enums.NoticeType.DefenceContractAward:
                    return DefenceContractAward;
                case Enums.NoticeType.ExAnte:
                    return ExAnte(notice);
                case Enums.NoticeType.DesignContest:
                    return DesignContest;
                case Enums.NoticeType.DesignContestResults:
                    return DesignContestResults;
                case Enums.NoticeType.Concession:
                    return Concession;
                case Enums.NoticeType.ConcessionAward:
                    return ConcessionAward;
                case Enums.NoticeType.SocialUtilitiesContractAward:
                    return SocialUtilitiesContractAward;
                case Enums.NoticeType.DpsAward:
                    return notice.Project.ProcurementCategory == Enums.ProcurementCategory.Public ? ContractAward : ContractAwardUtilities;
                case Enums.NoticeType.SocialUtilitiesQualificationSystem:
                    return SocialUtilitiesQualificationSystem;
                case Enums.NoticeType.SocialConcessionPriorInformation:
                    return SocialConcessionPriorInformation;
                case Enums.NoticeType.SocialConcessionAward:
                    return SocialConcessionAward;
                default:
                    break;
            }

            throw new NotSupportedException($"Notice type {notice.Type} is not supported");

        }
    }
}
