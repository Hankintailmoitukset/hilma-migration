using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration ContractAward => new NoticeContractConfiguration
        {
            PreviousNoticeOjsNumber = true,
            Project = BasicProjectConfiguration,
            LotsInfo = new LotsInfoConfiguration()
            {
                DivisionLots = true,
                QuantityOfLots = true,
            },
            ObjectDescriptions = new ObjectDescriptionConfiguration
            {
                Title = true,
                LotNumber = true,
                AdditionalCpvCodes = new CpvCodeConfiguration { Code = true, VocCodes = new VocCodeConfiguration { Code = true } },
                NutsCodes = true,
                MainsiteplaceWorksDelivery = true,
                DescrProcurement = true,
                AwardCriteria = new AwardCriteriaConfiguration
                {
                    CriterionTypes = true,
                    QualityCriteria = new AwardCriterionDefinitionConfiguration { Criterion = true, Weighting = true },
                    CostCriteria = new AwardCriterionDefinitionConfiguration { Criterion = true, Weighting = true },
                    PriceCriterion = new AwardCriterionDefinitionConfiguration { Weighting = true },
                },
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
                },
                CandidateNumberRestrictions = new CandidateNumberRestrictionsConfiguration
                {
                    EnvisagedNumber = false,
                    EnvisagedMinimumNumber = false,
                    EnvisagedMaximumNumber = false,
                    ObjectiveCriteriaForChoosing = false,
                    Selected = false
                },
                OptionsAndVariants = new OptionsAndVariantsConfiguration
                {
                    Options = true,
                    OptionsDescription = true,
                },
                TendersMustBePresentedAsElectronicCatalogs = true,
                EuFunds = new EuFundsConfiguration
                {
                    ProcurementRelatedToEuProgram = true,
                    ProjectIdentification = true
                },
                AdditionalInformation = true
            },
            ContactPerson = new ContactPersonConfiguration
            {
                Name = true,
                Email = true,
                Phone = true
            },
            ProcurementObject = new ProcurementObjectConfiguration
            {
                ShortDescription = true,
                TotalValue = new ValueRangeContractConfiguration { Currency = true, Value = true, Type = true, MaxValue = true, MinValue = true },
                MainCpvCode = new CpvCodeConfiguration { Code = true, VocCodes = new VocCodeConfiguration { Code = true } }
            },
            ProcedureInformation = new ProcedureInformationConfiguration
            {
                ProcedureType = true,
                AcceleratedProcedure = true,
                JustificationForAcceleratedProcedure = true,
                ProcurementGovernedByGPA = true,
                FrameworkAgreement = new FrameworkAgreementInformationConfiguration
                {
                    FrameworkAgreementType = true,
                    IncludesDynamicPurchasingSystem = true,
                    IncludesFrameworkAgreement = true,
                }
            },
            ComplementaryInformation = new ComplementaryInformationConfiguration
            {
                AdditionalInformation = true,
            },
            ProceduresForReview = new ProceduresForReviewInformationConfiguration
            {
                ReviewBody = ContractBodyContactInformationConfigurationDefault,
                ReviewProcedure = true

            },
            AttachmentInformation = new AttachmentInformationConfiguration
            {
                Description = true,
                Links = new LinkConfiguration
                {
                    Description = true,
                    Url = true
                }
            },
            Annexes = new AnnexConfiguration
            {
                D1 = new AnnexD1Configuration
                {
                    NoTenders = true,
                    SuppliesManufacturedForResearch = true,
                    ProvidedByOnlyParticularOperator = true,
                    ExtremeUrgency = true,
                    AdditionalDeliveries = true,
                    RepetitionExisting = true,
                    DesignContestAward = true,
                    CommodityMarket = true,
                    AdvantageousTerms = true,
                    AdvantageousPurchaseReason = true,
                    Justification = true,
                    ReasonForNoCompetition = true
                }
            }
        };
    }

}
