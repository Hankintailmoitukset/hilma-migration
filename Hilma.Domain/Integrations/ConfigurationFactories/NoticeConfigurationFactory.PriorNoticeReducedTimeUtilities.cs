using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration priorNoticeReducedTimeUtilities = new NoticeContractConfiguration
        {
            PreviousNoticeOjsNumber = true,
            Project = BasicProjectConfiguration,
            LotsInfo = LotsInfoConfigurationDefault,
            Language = true,
            ObjectDescriptions = new ObjectDescriptionConfiguration
            {
                Title = true,
                LotNumber = true,
                AdditionalCpvCodes = new CpvCodeConfiguration { Code = true, VocCodes = new VocCodeConfiguration { Code = true } },
                NutsCodes = true,
                MainsiteplaceWorksDelivery = true,
                DescrProcurement = true,
                EstimatedValue = new ValueRangeContractConfiguration { Currency = true, Value = true },
                AwardCriteria = new AwardCriteriaConfiguration
                {
                    CriterionTypes = true,
                    QualityCriteria = new AwardCriterionDefinitionConfiguration { Criterion = true, Weighting = true },
                    CostCriteria = new AwardCriterionDefinitionConfiguration { Criterion = true, Weighting = true },
                    PriceCriterion = new AwardCriterionDefinitionConfiguration { Weighting = true },
                    CriteriaStatedInProcurementDocuments = true
                },
                TimeFrame = new TimeFrameConfiguration
                {
                    Type = true,
                    BeginDate = true,
                    EndDate = true,
                    CanBeRenewed = true,
                    Days = true,
                    Months = true,
                    RenewalDescription = true
                },
                OptionsAndVariants = new OptionsAndVariantsConfiguration
                {
                    Options = true,
                    OptionsDescription = true,
                    VariantsWillBeAccepted = true
                },
                EuFunds = new EuFundsConfiguration
                {
                    ProcurementRelatedToEuProgram = true,
                    ProjectIdentification = true
                },
                AdditionalInformation = true
            },
            ConditionsInformation = new ConditionsInformationConfiguration
            {
                ProfessionalSuitabilityRequirements = true,
                EconomicCriteriaToParticipate = true,
                EconomicCriteriaDescription = true,
                EconomicRequiredStandards = true,
                TechnicalCriteriaToParticipate = true,
                TechnicalCriteriaDescription = true,
                TechnicalRequiredStandards = true,
                RulesForParticipation = true,
                RestrictedToShelteredProgram = true,
                RestrictedToShelteredWorkshop = true,
                ExecutionOfServiceIsReservedForProfession = true,
                ReferenceToRelevantLawRegulationOrProvision = true,
                ContractPerformanceConditions = true,
                ObligationToIndicateNamesAndProfessionalQualifications = true
            },
            CommunicationInformation = new CommunicationInformationConfiguration
            {
                ProcurementDocumentsAvailable = true,
                ProcurementDocumentsUrl = true,
                AdditionalInformation = true,
                AdditionalInformationAddress = new ContractBodyContactInformationConfiguration
                {
                    OfficialName = true,
                    Department = true,
                    NationalRegistrationNumber = true,
                    Email = true,
                    NutsCodes = true,
                    MainUrl = true,
                    ContactPerson = true,
                    PostalAddress = new PostalAddressConfiguration
                    {
                        Town = true,
                        Country = true,
                        PostalCode = true,
                        StreetAddress = true
                    },
                    TelephoneNumber = true
                },
                ElectronicCommunicationRequiresSpecialTools = true,
                ElectronicCommunicationInfoUrl = true,
                SendTendersOption = true,
                ElectronicAddressToSendTenders = true,
                AddressToSendTenders = ContractBodyContactInformationConfigurationDefault,
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
                EstimatedValue = new ValueRangeContractConfiguration { Currency = true, Value = true },
                MainCpvCode = new CpvCodeConfiguration { Code = true, VocCodes = new VocCodeConfiguration { Code = true } }
            },
            ProcedureInformation = new ProcedureInformationConfiguration
            {
                ElectronicAuctionWillBeUsed = true,
                AdditionalInformationAboutElectronicAuction = true,
                ProcurementGovernedByGPA = true,
                FrameworkAgreement = new FrameworkAgreementInformationConfiguration
                {
                    DynamicPurchasingSystemInvolvesAdditionalPurchasers = true,
                    EnvisagedNumberOfParticipants = true,
                    FrameworkAgreementType = true,
                    IncludesDynamicPurchasingSystem = true,
                    IncludesFrameworkAgreement = true,
                    JustificationForDurationOverFourYears = true,                    
                }
            },
            TenderingInformation = new TenderingInformationConfiguration
            {
                TendersOrRequestsToParticipateDueDateTime = true,
                EstimatedDateOfInvitations = true,
                Languages = true,
                ScheduledStartDateOfAwardProcedures = true,
                EstimatedDateOfContractNoticePublication = true
            },
            ComplementaryInformation = new ComplementaryInformationConfiguration
            {
                AdditionalInformation = true,
                ElectronicOrderingUsed = true,
                ElectronicInvoicingUsed = true,
                ElectronicPaymentUsed = true
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
