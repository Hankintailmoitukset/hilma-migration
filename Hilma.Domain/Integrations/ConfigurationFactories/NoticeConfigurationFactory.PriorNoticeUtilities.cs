using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration PriorNoticeUtilities => new NoticeContractConfiguration()
        {
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
                AdditionalInformation = true
            },
            CommunicationInformation = new CommunicationInformationConfiguration()
            {
                ProcurementDocumentsAvailable = false,
                ProcurementDocumentsUrl = false,
                AdditionalInformation = true,
                AdditionalInformationAddress = ContractBodyContactInformationConfigurationDefault,
                ElectronicCommunicationRequiresSpecialTools = true,
                ElectronicCommunicationInfoUrl = true,
                SendTendersOption = false,
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
                AdditionalInformation = true
            },
            TenderingInformation = new TenderingInformationConfiguration()
            {
                EstimatedDateOfContractNoticePublication = true
            },
            ProcedureInformation = new ProcedureInformationConfiguration
            {
                ProcurementGovernedByGPA = true
            }
        };

    }
}
