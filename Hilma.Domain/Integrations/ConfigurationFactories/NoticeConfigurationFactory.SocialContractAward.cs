using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration SocialContractAward => new NoticeContractConfiguration {
            PreviousNoticeOjsNumber = true,
            Project = BasicProjectConfiguration,
            LotsInfo = new LotsInfoConfiguration()
            {
                DivisionLots = true,
                QuantityOfLots = true
            },
            ObjectDescriptions = new ObjectDescriptionConfiguration {
                Title = true,
                LotNumber = true,
                AdditionalCpvCodes = new CpvCodeConfiguration { Code = true, VocCodes = new VocCodeConfiguration { Code = true } },
                NutsCodes = true,
                MainsiteplaceWorksDelivery = true,
                DescrProcurement = true,
                EstimatedValue = new ValueRangeContractConfiguration { Currency = false, Value = false },
                TendersMustBePresentedAsElectronicCatalogs = false,
                EuFunds = new EuFundsConfiguration {
                    ProcurementRelatedToEuProgram = true,
                    ProjectIdentification = true
                },
                AdditionalInformation = true,
                AwardContract = new AwardConfiguration
                {
                    ContractAwarded = true,
                    AwardedContract = new ContractAwardConfiguration
                    {
                        ConclusionDate = true,
                        ContractNumber = true,
                        ContractTitle = true,
                        NumberOfTenders = new NumberOfTendersConfiguration
                        {
                            Total = true,
                            Sme = true,
                            OtherEu = true,
                            NonEu = true,
                            Electronic = true
                        },
                        Contractors = new ContractorContactInformationConfiguration
                        {
                            OfficialName = true,
                            NationalRegistrationNumber = true,
                            NutsCodes = true,
                            PostalAddress = new PostalAddressConfiguration
                            {
                                StreetAddress = true,
                                PostalCode = true,
                                Town = true,
                                Country = true
                            },
                            TelephoneNumber = true,
                            Email = true,
                            MainUrl = true,
                            IsSmallMediumEnterprise = true
                        },
                        FinalTotalValue = new ValueRangeContractConfiguration
                        {
                            Type = true,
                            Value = true,
                            Currency = true,
                            MinValue = true,
                            MaxValue = true
                        },
                        LikelyToBeSubcontracted = true,
                        ValueOfSubcontract = new ValueContractConfiguration
                        {
                            Value = true,
                            Currency = true
                        },
                        ProportionOfValue = true,
                        SubcontractingDescription = true
                    },
                    NoAwardedContract = new NonAwardConfiguration
                    {
                        FailureReason = true,
                        OriginalNoticeSentVia = true,
                        OriginalNoticeSentViaOther = true,
                        OriginalNoticeSentDate = true
                    }
                }
            },
            ContactPerson = new ContactPersonConfiguration {
                Name = true,
                Email = true, 
                Phone = true
            },
            ProcurementObject = new ProcurementObjectConfiguration {
                ShortDescription = true,
                MainCpvCode = new CpvCodeConfiguration { Code = true, VocCodes = new VocCodeConfiguration { Code = true } },
                EstimatedValue = new ValueRangeContractConfiguration
                {
                    Value = false
                },
                TotalValue = new ValueRangeContractConfiguration
                {
                    Type = true,
                    Value = true,
                    Currency = true,
                    MinValue = true,
                    MaxValue = true
                }
            },
            ProcedureInformation = new ProcedureInformationConfiguration {
                ProcedureType = true,
                AcceleratedProcedure = false,
                JustificationForAcceleratedProcedure = false,
                ElectronicAuctionWillBeUsed = false,
                AdditionalInformationAboutElectronicAuction = false,
                ProcurementGovernedByGPA = false,
                ReductionRecourseToReduceNumberOfSolutions = false,
                ReserveRightToAwardWithoutNegotiations = false,
                UrlNationalProcedure = true,
                MainFeaturesAward = true,
                FrameworkAgreement = new FrameworkAgreementInformationConfiguration {
                    DynamicPurchasingSystemInvolvesAdditionalPurchasers = false,
                    EnvisagedNumberOfParticipants = false,
                    FrameworkAgreementType = true,
                    IncludesDynamicPurchasingSystem = false,
                    IncludesFrameworkAgreement = true,
                    JustificationForDurationOverFourYears = false
                }
            },
            ComplementaryInformation = new ComplementaryInformationConfiguration {
                AdditionalInformation = true,
                IsRecurringProcurement = false,
                EstimatedTimingForFurtherNoticePublish = false,
                ElectronicOrderingUsed = false,
                ElectronicInvoicingUsed = false,
                ElectronicPaymentUsed = false
            },
            ProceduresForReview = new ProceduresForReviewInformationConfiguration {
                ReviewBody = ContractBodyContactInformationConfigurationDefault,
                ReviewProcedure = true
            },
            AttachmentInformation = new AttachmentInformationConfiguration {
                Description = true,
                Links = new LinkConfiguration {
                    Description = true,
                    Url = true
                }
            },
            Annexes = new AnnexConfiguration
            {
                D1 = new AnnexD1Configuration
                {
                    NoTenders = true,
                    SuppliesManufacturedForResearch = false,
                    ProvidedByOnlyParticularOperator = true,
                    ExtremeUrgency = true,
                    AdditionalDeliveries = false,
                    RepetitionExisting = true,
                    DesignContestAward = false,
                    CommodityMarket = false,
                    AdvantageousTerms = true
                }
            }
        };
    }

}
