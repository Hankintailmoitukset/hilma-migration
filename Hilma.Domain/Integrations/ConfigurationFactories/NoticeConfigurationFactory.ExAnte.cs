using Hilma.Domain.DataContracts;
using Hilma.Domain.Enums;
using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration ExAnte(NoticeContract notice) => new NoticeContractConfiguration
        {
            PreviousNoticeOjsNumber = true, // IV.2
            ConditionsInformation = null,
            Project = new ProcurementProjectContractConfiguration // ok
            {
                Title = true, // II.1.1.1
                ReferenceNumber = true, // II.1.1.2
                ContractType = true, // II.1.3
                Organisation = new OrganisationContractConfiguration // I.1
                {
                    Id = true,
                    ContractingAuthorityType = true,
                    Information = new ContractBodyContactInformationConfiguration
                    {
                        ContactPerson = true,
                        Department = true,
                        Email = true,
                        MainUrl = true,
                        NationalRegistrationNumber = true,
                        NutsCodes = true,
                        OfficialName = true,
                        PostalAddress = new PostalAddressConfiguration
                        {
                            Country = true,
                            PostalCode = true,
                            StreetAddress = true,
                            Town = true
                        },
                        TelephoneNumber = true
                    },
                    MainActivity = true,
                    MainActivityUtilities = true,
                    OtherMainActivity = true,
                    OtherContractingAuthorityType = true
                }
            },
            
            LotsInfo = new LotsInfoConfiguration // Ok
            {
                DivisionLots = true,
                QuantityOfLots = true,
                // Does F15 does not have these additional fields
                // LotCombinationPossible = false,
                // LotCombinationPossibleDescription = false
            },
            ObjectDescriptions = new ObjectDescriptionConfiguration()
            {
                Title = true, // II.2.1.1
                LotNumber = true, // II.2.1.2
                AdditionalCpvCodes = new CpvCodeConfiguration // II.2.2
                { Code = true, VocCodes = new VocCodeConfiguration { Code = true } },
                NutsCodes = true, // II.2.3.1
                MainsiteplaceWorksDelivery = true, // II.2.3.2
                DescrProcurement = true, // II.2.4.1
                DisagreeAwardCriteriaToBePublished = true, // II.2.5.1
                AwardCriteria = new AwardCriteriaConfiguration // II.2.5
                {
                    CriterionTypes = true,
                    QualityCriteria = new AwardCriterionDefinitionConfiguration { Criterion = true, Weighting = true },
                    CostCriteria = new AwardCriterionDefinitionConfiguration { Criterion = true, Weighting = true },
                    PriceCriterion = new AwardCriterionDefinitionConfiguration { Weighting = true },
                    Criterion = true
                },
                AdditionalInformation = true, // II.2.14
                EuFunds = new EuFundsConfiguration // II.2.13
                {
                    ProcurementRelatedToEuProgram = true,
                    ProjectIdentification = true
                },
                OptionsAndVariants = new OptionsAndVariantsConfiguration // II.2.11
                {
                    Options = true,
                    OptionsDescription = true
                },
                AwardContract = new AwardConfiguration
                {
                    ContractAwarded = true,
                    AwardedContract = new ContractAwardConfiguration
                    {
                        ContractTitle = true,
                        ConclusionDate = true, // V.2.1
                        DisagreeContractorInformationToBePublished = true, // V.2.3.1
                        Contractors = new ContractorContactInformationConfiguration // V.2.3
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
                        FinalTotalValue = new ValueRangeContractConfiguration // V.2.4
                        {
                            DisagreeToBePublished = true,
                            Value = true,
                            Currency = true,
                            MinValue = true,
                            MaxValue = true
                        },
                        LikelyToBeSubcontracted = true, // V.2.5.1
                        ValueOfSubcontract = new ValueContractConfiguration // V.2.5.2-3
                        {
                            Value = true,
                            Currency = true
                        },
                        ProportionOfValue = true, // V.2.5.4
                        SubcontractingDescription = true, // V.2.5.5
                        ExAnteSubcontracting = notice.Project.ProcurementCategory == ProcurementCategory.Defence ? new ExAnteSubcontractingConfiguration // V.2.5
                        {
                            AllOrCertainSubcontractsWillBeAwarded = true,
                            ShareOfContractWillBeSubcontracted = true,
                            ShareOfContractWillBeSubcontractedMaxPercentage = true,
                            ShareOfContractWillBeSubcontractedMinPercentage = true,
                        } : null
                    },

                }
            },
            ContactPerson = new ContactPersonConfiguration // ? Part of organisation i guess
            {
                Name = true,
                Email = true,
                Phone = true
            },
            ProcurementObject = new ProcurementObjectConfiguration
            {
                ShortDescription = true, // II.1.4
                TotalValue = new ValueRangeContractConfiguration
                { // II.1.7
                    Currency = true,
                    Value = true,
                    DisagreeToBePublished = true,
                    Type = true,
                    MaxValue = true,
                    MinValue = true
                },
                MainCpvCode = new CpvCodeConfiguration // II.1.2
                { Code = true, VocCodes = new VocCodeConfiguration { Code = true } },
            },
            ComplementaryInformation = new ComplementaryInformationConfiguration // VI.3.1
            {
                AdditionalInformation = true,
            },
            ProcedureInformation = new ProcedureInformationConfiguration
            {
                ProcedureType = true, // IV.1.1
                ProcurementGovernedByGPA = true, // IV.1.8
                FrameworkAgreement = new FrameworkAgreementInformationConfiguration // IV.1.3
                {
                    IncludesFrameworkAgreement = true,
                }
            },
            ProceduresForReview = new ProceduresForReviewInformationConfiguration // VI.4
            {
                ReviewBody = ContractBodyContactInformationConfigurationDefault,
                ReviewProcedure = true
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
                    AdvantageousTerms = true
                }
            }
        };
    }
}
