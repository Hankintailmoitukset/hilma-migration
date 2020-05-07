using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration SocialConcessionAward => new NoticeContractConfiguration
        {
            PreviousNoticeOjsNumber = true,
            Language = true,
            Project = new ProcurementProjectContractConfiguration()
            {
                Title = true,
                ReferenceNumber = true,
                ContractType = true,
                ProcurementLaw = true,
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
                    MainActivityUtilities = true,
                    OtherContractingAuthorityType = true,
                    OtherMainActivity = true
                }
            },
            LotsInfo = new LotsInfoConfiguration()
            {
                DivisionLots = true,
                QuantityOfLots = true
            },
            ObjectDescriptions = new ObjectDescriptionConfiguration
            {
                Title = true,
                LotNumber = true,
                AdditionalCpvCodes = new CpvCodeConfiguration { Code = true, VocCodes = new VocCodeConfiguration { Code = true } },
                NutsCodes = true,
                MainsiteplaceWorksDelivery = true,
                DescrProcurement = true,
                TimeFrame = new TimeFrameConfiguration
                {
                    Type = true,
                    BeginDate = true,
                    EndDate = true,
                    Days = true,
                    Months = true,
                },
                EuFunds = new EuFundsConfiguration
                {
                    ProcurementRelatedToEuProgram = true,
                    ProjectIdentification = true
                },
                AdditionalInformation = true,
                AwardContract = new AwardConfiguration()
                {
                    ContractAwarded = true,
                    AwardedContract = new ContractAwardConfiguration()
                    {
                        ConclusionDate = true,
                        NumberOfTenders = new NumberOfTendersConfiguration() {
                            Total = true,
                            Sme = true,
                            OtherEu = true,
                            NonEu = true,
                            Electronic = true,
                        },
                        Contractors = new ContractorContactInformationConfiguration()
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
                        InitialEstimatedValueOfContract = new ValueContractConfiguration() { Value = true, Currency = true },
                        FinalTotalValue = new ValueRangeContractConfiguration() { Value = true, Currency = true },
                        ConcessionRevenue = new ValueContractConfiguration() { Value = true, Currency = true },
                        PricesAndPayments = new ValueContractConfiguration() { Value = true, Currency = true },
                        ConcessionValueAdditionalInformation = true
                    },
                    NoAwardedContract = new NonAwardConfiguration()
                    {
                        FailureReason = true,
                        OriginalNoticeSentVia = true,
                        OriginalNoticeSentViaOther = true,
                        OriginalEsender = new EsenderConfiguration()
                        {
                            Login = true,
                            CustomerLogin = true,
                            TedNoDocExt = true
                        },
                        OriginalNoticeSentDate = true
                    }
                }
            },
            ProcurementObject = new ProcurementObjectConfiguration
            {
                ShortDescription = true,
                EstimatedValue = new ValueRangeContractConfiguration() {
                    Type = true,
                    Value = true,
                    Currency = true
                },
                TotalValue = new ValueRangeContractConfiguration()
                {
                    Type = true,
                    Value = true,
                    MinValue = true,
                    MaxValue = true,
                    Currency = true
                },
                MainCpvCode = new CpvCodeConfiguration { Code = true, VocCodes = new VocCodeConfiguration { Code = true } }
            },
            ProcedureInformation = new ProcedureInformationConfiguration
            {
                ProcedureType = true,
                MainFeaturesAward = true
            },
            ComplementaryInformation = new ComplementaryInformationConfiguration
            {
                AdditionalInformation = true
            },
            ContactPerson = new ContactPersonConfiguration()
            {
                Name = true,
                Email = true,
                Phone = true
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
            Annexes = new AnnexConfiguration()
            {
                D4 = new AnnexD4Configuration()
                {
                    NoTenders = true,
                    ProvidedByOnlyParticularOperator = true,
                    ReasonForNoCompetition = true,

                    Justification = true
                }
            }
        };
    }

}
