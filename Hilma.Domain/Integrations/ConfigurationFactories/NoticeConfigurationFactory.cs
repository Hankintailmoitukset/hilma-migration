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
                        BuyerProfileUrl = true,
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
                            BuyerProfileUrl = true,
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
                    return priorNotice;
                case Enums.NoticeType.PriorInformationReduceTimeLimits:
                    return priorNoticeReducedTime;
                case Enums.NoticeType.PeriodicIndicativeUtilities:
                    return priorNoticeUtilities;
                case Enums.NoticeType.SocialUtilitiesPriorInformation:
                    return priorNoticeSocialUtilities;
                case Enums.NoticeType.Contract:
                    return contractNotice;
                case Enums.NoticeType.ContractAward:
                    return contractAward;
                case Enums.NoticeType.SocialContract:
                    return socialContract;
                case Enums.NoticeType.SocialContractAward:
                    return socialContractAward;
                case Enums.NoticeType.DefenceContract:
                    return defenceContractNotice;
                case Enums.NoticeType.SocialUtilities:
                    return socialUtilities;
                case Enums.NoticeType.DefencePriorInformation:
                    return defencePriorInformation;
                case Enums.NoticeType.SocialPriorInformation:
                    return socialPriorInformation;
                case Enums.NoticeType.ContractAwardUtilities:
                    return contractAwardUtilities;
                case Enums.NoticeType.DefenceContractAward:
                    return defenceContractAward;
                case Enums.NoticeType.ExAnte:
                    return ExAnte(notice);
                case Enums.NoticeType.DesignContest:
                    return designContest;
                case Enums.NoticeType.Concession:
                    return concession;
                default:
                    break;
            }

            throw new NotSupportedException($"Notice type {notice.Type} is not supported");

        }
    }
}
