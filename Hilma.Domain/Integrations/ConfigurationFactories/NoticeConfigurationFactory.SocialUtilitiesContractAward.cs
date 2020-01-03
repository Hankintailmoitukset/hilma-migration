using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration socialUtilitiesContractAward = new NoticeContractConfiguration
        {
            Language = true,
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
            },
            LotsInfo = new LotsInfoConfiguration()
            {
                DivisionLots = true,
                QuantityOfLots = true
            },
            ObjectDescriptions = new ObjectDescriptionConfiguration()
            {
                Title = true,
                LotNumber = true,
                AdditionalCpvCodes = new CpvCodeConfiguration() { Code = true, VocCodes = new VocCodeConfiguration() { Code = true } },
                NutsCodes = true,
                MainsiteplaceWorksDelivery = true,
                DescrProcurement = true,
                AdditionalInformation = true,
                EuFunds = new EuFundsConfiguration
                {
                    ProcurementRelatedToEuProgram = true,
                    ProjectIdentification = true
                },
                AwardContract = new AwardConfiguration()
                {
                    ContractAwarded = true,
                    AwardedContract = new ContractAwardConfiguration()
                    {
                        ContractNumber = true,
                        ContractTitle = true,
                        ConclusionDate = true,
                        NumberOfTenders = new NumberOfTendersConfiguration
                        {
                            DisagreeTenderInformationToBePublished = true,
                            Total = true,
                            Sme = true,
                            OtherEu = true,
                            NonEu = true,
                            Electronic = true
                        },
                        DisagreeContractorInformationToBePublished = true,
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
                        InitialEstimatedValueOfContract = new ValueContractConfiguration()
                        {
                            Currency = true,
                            Value = true
                        },
                        FinalTotalValue = new ValueRangeContractConfiguration
                        {
                            DisagreeToBePublished = true,
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
                    NoAwardedContract = new NonAwardConfiguration()
                    {
                        FailureReason = true,
                        OriginalNoticeSentVia = true,
                        OriginalNoticeSentViaOther = true,
                        OriginalNoticeSentDate = true
                    }
                }
            },
            ContactPerson = new ContactPersonConfiguration()
            {
                Name = true,
                Email = true,
                Phone = true
            },
            ProcurementObject = new ProcurementObjectConfiguration()
            {
                ShortDescription = true,
                MainCpvCode = new CpvCodeConfiguration() { Code = true, VocCodes = new VocCodeConfiguration() { Code = true } },
                TotalValue = new ValueRangeContractConfiguration
                {
                    DisagreeToBePublished = true,
                    Type = true,
                    Value = true,
                    Currency = true,
                    MinValue = true,
                    MaxValue = true
                }
            },
            ProcedureInformation = new ProcedureInformationConfiguration()
            {
                ProcedureType = true,
                FrameworkAgreement = new FrameworkAgreementInformationConfiguration()
                {
                    IncludesFrameworkAgreement = true,
                },
                MainFeaturesAward = true,
                UrlNationalProcedure = true
            },
            ProceduresForReview = new ProceduresForReviewInformationConfiguration()
            {
                ReviewProcedure = true,
                ReviewBody = ContractBodyContactInformationConfigurationDefault
            },
            ComplementaryInformation = new ComplementaryInformationConfiguration(),
            Annexes = new AnnexConfiguration()
            {
                D2 = new AnnexD2Configuration()
                {
                    NoTenders = true,
                    PureResearch = true,
                    ProvidedByOnlyParticularOperator = true,
                    ReasonForNoCompetition = true,
                    ExtremeUrgency = true,
                    RepetitionExisting = true,
                    AdvantageousTerms = true,
                    AdvantageousPurchaseReason = true,
                    BargainPurchase = true,
                    
                    Justification = true                    
                }
            }
        };

    }
}
