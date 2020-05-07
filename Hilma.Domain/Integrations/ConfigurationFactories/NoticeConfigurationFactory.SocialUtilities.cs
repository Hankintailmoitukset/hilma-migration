using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration SocialUtilities => new NoticeContractConfiguration {
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
                QuantityOfLots = true,
                LotCombinationPossible = true,
                LotCombinationPossibleDescription = true
            },
            ObjectDescriptions = new ObjectDescriptionConfiguration()
            {
                Title = true,
                LotNumber = true,
                AdditionalCpvCodes = new CpvCodeConfiguration() { Code = true, VocCodes = new VocCodeConfiguration() { Code = true } },
                NutsCodes = true,
                MainsiteplaceWorksDelivery = true,
                DescrProcurement = true,
                EstimatedValue = new ValueRangeContractConfiguration() { Currency = true, Value = true },
                AdditionalInformation = true,
                EuFunds = new EuFundsConfiguration
                {
                    ProcurementRelatedToEuProgram = true,
                    ProjectIdentification = true
                },
                TimeFrame = new TimeFrameConfiguration() {
                   Type = true,
                   
                },
            },
            CommunicationInformation = new CommunicationInformationConfiguration()
            {
                ProcurementDocumentsAvailable = true,
                ProcurementDocumentsUrl = true,
                AdditionalInformation = true,
                AdditionalInformationAddress = ContractBodyContactInformationConfigurationDefault,
                ElectronicCommunicationRequiresSpecialTools = true,
                ElectronicCommunicationInfoUrl = true,
                SendTendersOption = true,
                AddressToSendTenders = ContractBodyContactInformationConfigurationDefault,
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
                EstimatedValue = new ValueRangeContractConfiguration() { Currency = true, Value = true },
                MainCpvCode = new CpvCodeConfiguration() { Code = true, VocCodes = new VocCodeConfiguration() { Code = true } },
            },
            ComplementaryInformation = new ComplementaryInformationConfiguration()
            {
                AdditionalInformation = true,
                ElectronicInvoicingUsed = true,
                ElectronicOrderingUsed = true,
                ElectronicPaymentUsed = true
            },
            TenderingInformation = new TenderingInformationConfiguration()
            {
                TendersOrRequestsToParticipateDueDateTime = true,
                Languages = true
            },
            ConditionsInformation = new ConditionsInformationConfiguration()
            {
                RulesForParticipation = true,
                ReservedOrganisationServiceMission = true,
                RestrictedToShelteredProgram = true,
                RestrictedToShelteredWorkshop = true,
                ExecutionOfServiceIsReservedForProfession = true,
                ReferenceToRelevantLawRegulationOrProvision = true,
                ContractPerformanceConditions = true,
                ObligationToIndicateNamesAndProfessionalQualifications = true
            },
            ProcedureInformation = new ProcedureInformationConfiguration()
            {
                ProcedureType = true,
                FrameworkAgreement = new FrameworkAgreementInformationConfiguration()
                {
                    IncludesFrameworkAgreement = true,
                    JustificationForDurationOverEightYears = true,
                },
                MainFeaturesAward = true,
                UrlNationalProcedure = true
            },
            ProceduresForReview = new ProceduresForReviewInformationConfiguration()
            {
                ReviewProcedure = true,
                ReviewBody = new ContractBodyContactInformationConfiguration()
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
                }
            }
        };

    }
}
