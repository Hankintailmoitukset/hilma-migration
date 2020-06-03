using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration SocialConcessionPriorInformation => new NoticeContractConfiguration()
        {
            Language = true,
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
                    MainActivityUtilities = true,
                    OtherContractingAuthorityType = true,
                    OtherMainActivity = true
                }
            },
            LotsInfo = new LotsInfoConfiguration()
            {
                DivisionLots = true,
                QuantityOfLots = true,
                LotsMaxAwarded = true,
                LotsMaxAwardedQuantity = true,
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
                EuFunds = new EuFundsConfiguration() {
                    ProcurementRelatedToEuProgram = true,
                    ProjectIdentification = true
                },
                AdditionalInformation = true
            },
            CommunicationInformation = new CommunicationInformationConfiguration()
            {
                ProcurementDocumentsAvailable = true,
                ProcurementDocumentsUrl = true,
                AdditionalInformation = true,
                AdditionalInformationAddress = new ContractBodyContactInformationConfiguration()
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
                },
                SendTendersOption = true,
                AddressToSendTenders = new ContractBodyContactInformationConfiguration()
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
                },
                ElectronicCommunicationRequiresSpecialTools = true
            },
            ConditionsInformation = new ConditionsInformationConfiguration()
            {
                RulesForParticipation = true,
                RestrictedToShelteredWorkshop = true,
                RestrictedToShelteredProgram = true,
                ExecutionOfServiceIsReservedForProfession = true,
                ReferenceToRelevantLawRegulationOrProvision = true,
                ContractPerformanceConditions = true,
                ObligationToIndicateNamesAndProfessionalQualifications = true
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
                ElectronicOrderingUsed = true,
                ElectronicInvoicingUsed = true,
                ElectronicPaymentUsed = true,
                AdditionalInformation = true
            },
            ProcedureInformation = new ProcedureInformationConfiguration(),
            TenderingInformation = new TenderingInformationConfiguration()
            {
                TendersOrRequestsToParticipateDueDateTime = true,
                Languages = true
            },
            ProceduresForReview = new ProceduresForReviewInformationConfiguration()
            {
                ReviewBody = ContractBodyContactInformationConfigurationDefault,
                ReviewProcedure = true
            }
        };

    }
}
