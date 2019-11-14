using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration contractAwardUtilities = new NoticeContractConfiguration
        {
            PreviousNoticeOjsNumber = true,
            Project = new ProcurementProjectContractConfiguration()
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
                    ContractingAuthorityType = false,
                    OtherContractingAuthorityType = false,
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
                    MainActivityUtilities = true,
                    OtherMainActivity = true
                }
            },
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
                    PriceCriterion = new AwardCriterionDefinitionConfiguration { Weighting = true }
                },
                DisagreeAwardCriteriaToBePublished = true,
                AwardContract = new AwardConfiguration
                {
                    ContractAwarded = true,
                    AwardedContract = new ContractAwardConfiguration
                    {
                        ConclusionDate = true,
                        ContractNumber = true,
                        ContractTitle = true,
                        DisagreeContractorInformationToBePublished = true,
                        NumberOfTenders = new NumberOfTendersConfiguration
                        {
                            DisagreeTenderInformationToBePublished = true,
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
                            MaxValue = true,
                            DisagreeToBePublished = true,
                            Type = true
                        },
                        LikelyToBeSubcontracted = true,
                        ValueOfSubcontract = new ValueContractConfiguration { Value = true, Currency = true },
                        ProportionOfValue = true,
                        SubcontractingDescription = true,
                        PricePaidForBargainPurchases = new ValueContractConfiguration { Value = true, Currency = true },
                        NotPublicFields = new ContractAwardNotPublicFieldsConfiguration()
                        {
                            CommunityOrigin = true,
                            NonCommunityOrigin = true,
                            Countries = true,
                            AwardedToTendererWithVariant = true,
                            AbnormallyLowTendersExcluded = true
                        }
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
                    EnvisagedNumber = true,
                    EnvisagedMinimumNumber = true,
                    EnvisagedMaximumNumber = true,
                    ObjectiveCriteriaForChoosing = true,
                    Selected = true
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
                TotalValue = new ValueRangeContractConfiguration()
                {
                    Value = true,
                    Currency = true,
                    MinValue = true,
                    MaxValue = true,
                    DisagreeToBePublished = true,
                    Type = true
                },
                MainCpvCode = new CpvCodeConfiguration { Code = true, VocCodes = new VocCodeConfiguration { Code = true } }
            },
            ProcedureInformation = new ProcedureInformationConfiguration
            {
                ProcedureType = true,
                ElectronicAuctionWillBeUsed = true,
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
            }
        };
    }

}
