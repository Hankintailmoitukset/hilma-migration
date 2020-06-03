using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration SocialUtilitiesQualificationSystem => new NoticeContractConfiguration
        {
            Language = true,
            PreviousNoticeOjsNumber = true,
            Project = BasicProjectConfiguration,
            LotsInfo = new LotsInfoConfiguration(),
            ObjectDescriptions = new ObjectDescriptionConfiguration()
            {
                AdditionalCpvCodes = new CpvCodeConfiguration() { Code = true, VocCodes = new VocCodeConfiguration() { Code = true } },
                NutsCodes = true,
                MainsiteplaceWorksDelivery = true,
                DescrProcurement = true,
                AdditionalInformation = true,
                QualificationSystemDuration = new QualificationSystemDurationConfiguration()
                {
                    Type = true,
                    BeginDate = true,
                    EndDate = true,
                    Renewal = true,
                    NecessaryFormalities = true                    
                },
                EuFunds = new EuFundsConfiguration
                {
                    ProcurementRelatedToEuProgram = true,
                    ProjectIdentification = true
                }
            },
            CommunicationInformation = new CommunicationInformationConfiguration()
            {
                ProcurementDocumentsAvailable = true,
                ProcurementDocumentsUrl = true,
                DocumentsEntirelyInHilma = true,
                AdditionalInformation = true,
                AdditionalInformationAddress = ContractBodyContactInformationConfigurationDefault,
                ElectronicCommunicationRequiresSpecialTools = true,
                ElectronicCommunicationInfoUrl = true,
                SendTendersOption = true,
                AddressToSendTenders = ContractBodyContactInformationConfigurationDefault,
                ElectronicAddressToSendTenders = true
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
            },
            ComplementaryInformation = new ComplementaryInformationConfiguration()
            {
                AdditionalInformation = true,
            },
            ConditionsInformation = new ConditionsInformationConfiguration()
            {
                ReservedOrganisationServiceMission = true,
                RestrictedToShelteredProgram = true,
                RestrictedToShelteredWorkshop = true,
                ExecutionOfServiceIsReservedForProfession = true,
                ReferenceToRelevantLawRegulationOrProvision = true,
                ContractPerformanceConditions = true,
                ObligationToIndicateNamesAndProfessionalQualifications = true,
                QualificationSystemConditions = new QualificationSystemConditionConfiguration()
                {
                     Conditions = true,
                     Methods = true
                }
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
