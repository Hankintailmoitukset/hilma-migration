using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration priorNoticeReducedTime = new NoticeContractConfiguration()
        {
            Project = BasicProjectConfiguration,
            LotsInfo = LotsInfoConfigurationDefault,
            ProcurementObject = new ProcurementObjectConfiguration()
            {
                ShortDescription = true,
                EstimatedValue = new ValueRangeContractConfiguration() { Currency = true, Value = true },
                MainCpvCode = new CpvCodeConfiguration() { Code = true, VocCodes = new VocCodeConfiguration() { Code = true } },
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
                AwardCriteria = new AwardCriteriaConfiguration()
                {
                    CriterionTypes = true,
                    CostCriteria = new AwardCriterionDefinitionConfiguration { Criterion = true, Weighting = true },
                    QualityCriteria = new AwardCriterionDefinitionConfiguration { Criterion = true, Weighting = true },
                    PriceCriterion = new AwardCriterionDefinitionConfiguration { Weighting = true }
                },
                TimeFrame = new TimeFrameConfiguration() {
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
                TendersMustBePresentedAsElectronicCatalogs = false,
                EuFunds = new EuFundsConfiguration
                {
                    ProcurementRelatedToEuProgram= true,
                    ProjectIdentification = true
                },

                AdditionalInformation = true
            },
            ConditionsInformation = new ConditionsInformationConfiguration
            {
                ContractPerformanceConditions = true,
                EconomicCriteriaDescription = true,
                EconomicCriteriaToParticipate = true,
                EconomicRequiredStandards = true,
                ExecutionOfServiceIsReservedForProfession = true,
                ObligationToIndicateNamesAndProfessionalQualifications = true,
                ProfessionalSuitabilityRequirements = true,
                ReferenceToRelevantLawRegulationOrProvision = true,
                RestrictedToShelteredProgram = true,
                RestrictedToShelteredWorkshop = true,
                TechnicalCriteriaDescription = true,
                TechnicalCriteriaToParticipate = true,
                TechnicalRequiredStandards =true
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
            
            ComplementaryInformation = new ComplementaryInformationConfiguration()
            {
                ElectronicInvoicingUsed = true,
                ElectronicOrderingUsed = true,
                ElectronicPaymentUsed= true,
                AdditionalInformation = true
            },
            ProcedureInformation = new ProcedureInformationConfiguration
            {
                ElectronicAuctionWillBeUsed= true,
                AdditionalInformationAboutElectronicAuction= true,
                ProcurementGovernedByGPA= true,
                FrameworkAgreement = new FrameworkAgreementInformationConfiguration
                {
                    DynamicPurchasingSystemInvolvesAdditionalPurchasers = true,
                    EnvisagedNumberOfParticipants = true,
                    FrameworkAgreementType = true,
                    IncludesDynamicPurchasingSystem = false,
                    IncludesFrameworkAgreement = true,
                    JustificationForDurationOverFourYears = true
                }
            },
            TenderingInformation = new TenderingInformationConfiguration
            {
                EstimatedDateOfContractNoticePublication = true
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
        };
    }
}
