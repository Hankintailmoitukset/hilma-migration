using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration SocialContract => new NoticeContractConfiguration {
            PreviousNoticeOjsNumber = true,
            Project = BasicProjectConfiguration,
            LotsInfo = LotsInfoConfigurationDefault,
            ObjectDescriptions = new ObjectDescriptionConfiguration {
                Title = true,
                LotNumber = true,
                AdditionalCpvCodes = new CpvCodeConfiguration { Code = true, VocCodes = new VocCodeConfiguration { Code = true } },
                NutsCodes = true,
                MainsiteplaceWorksDelivery = true,
                DescrProcurement = true,
                EstimatedValue = new ValueRangeContractConfiguration { Currency = true, Value = true },
                /*AwardCriteria = new AwardCriteriaConfiguration {
                    CriterionTypes = true,
                    QualityCriteria = new AwardCriterionDefinitionConfiguration { Criterion = true, Weighting = true } ,
                    CostCriteria = new AwardCriterionDefinitionConfiguration { Criterion = true, Weighting = true },
                    PriceCriterion = new AwardCriterionDefinitionConfiguration { Weighting = true }
                },*/
                TimeFrame = new TimeFrameConfiguration {
                    Type = true,
                    BeginDate = true,
                    EndDate = true,
                    CanBeRenewed = false,
                    Days = true,
                    Months = true,
                    RenewalDescription = true
                },
                /*CandidateNumberRestrictions = new CandidateNumberRestrictionsConfiguration {
                    EnvisagedNumber = true,
                    EnvisagedMinimumNumber = true,
                    EnvisagedMaximumNumber = true,
                    ObjectiveCriteriaForChoosing = true,
                    Selected = true
                },*/
                /*OptionsAndVariants = new OptionsAndVariantsConfiguration {
                    Options = true,
                    OptionsDescription = true,
                    VariantsWillBeAccepted = true
                },*/
                TendersMustBePresentedAsElectronicCatalogs = false,
                EuFunds = new EuFundsConfiguration {
                    ProcurementRelatedToEuProgram = true,
                    ProjectIdentification = true
                },
                AdditionalInformation = true
            },
            ConditionsInformation = new ConditionsInformationConfiguration {
                ProfessionalSuitabilityRequirements = false,
                EconomicCriteriaToParticipate = false,
                EconomicCriteriaDescription = false,
                EconomicRequiredStandards = false,
                TechnicalCriteriaToParticipate = false,
                TechnicalCriteriaDescription = false,
                TechnicalRequiredStandards = false,
                RestrictedToShelteredProgram = true,
                RestrictedToShelteredWorkshop = true,
                ReservedOrganisationServiceMission = true,
                RulesForParticipation = true,
                ExecutionOfServiceIsReservedForProfession = true,
                ReferenceToRelevantLawRegulationOrProvision = true,
                ContractPerformanceConditions = true,
                ObligationToIndicateNamesAndProfessionalQualifications = true
            },
            CommunicationInformation = new CommunicationInformationConfiguration {
                ProcurementDocumentsAvailable = true,
                ProcurementDocumentsUrl = true,
                AdditionalInformation = true,
                AdditionalInformationAddress = new ContractBodyContactInformationConfiguration {
                    OfficialName = true,
                    NationalRegistrationNumber = true,
                    Email = true,
                    NutsCodes = true,
                    MainUrl = true,
                    PostalAddress = new PostalAddressConfiguration {
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
                AddressToSendTenders = ContractBodyContactInformationConfigurationDefault,
            },
            ContactPerson = new ContactPersonConfiguration {
                Name = true,
                Email = true,
                Phone = true
            },
            ProcurementObject = new ProcurementObjectConfiguration {
                ShortDescription = true,
                EstimatedValue = new ValueRangeContractConfiguration { Currency = true, Value = true },
                MainCpvCode = new CpvCodeConfiguration { Code = true, VocCodes = new VocCodeConfiguration { Code = true } }
            },
            ProcedureInformation = new ProcedureInformationConfiguration {
                ProcedureType = true,
                AcceleratedProcedure = false,
                JustificationForAcceleratedProcedure = false,
                ElectronicAuctionWillBeUsed = false,
                AdditionalInformationAboutElectronicAuction = false,
                ProcurementGovernedByGPA = false,
                ReductionRecourseToReduceNumberOfSolutions = false,
                ReserveRightToAwardWithoutNegotiations = false,
                UrlNationalProcedure = true,
                MainFeaturesAward = true,
                FrameworkAgreement = new FrameworkAgreementInformationConfiguration {
                    DynamicPurchasingSystemInvolvesAdditionalPurchasers = false,
                    EnvisagedNumberOfParticipants = false,
                    FrameworkAgreementType = true,
                    IncludesDynamicPurchasingSystem = false,
                    IncludesFrameworkAgreement = true,
                    JustificationForDurationOverFourYears = true
                }
            },
            TenderingInformation = new TenderingInformationConfiguration {
                TendersMustBeValidForMonths = false,
                TendersMustBeValidUntil = false,
                TendersOrRequestsToParticipateDueDateTime = true,
                TendersMustBeValidOption = false, 
                EstimatedDateOfInvitations = false,
                Languages = true
            },
            ComplementaryInformation = new ComplementaryInformationConfiguration {
                AdditionalInformation = true,
                IsRecurringProcurement = false,
                EstimatedTimingForFurtherNoticePublish = false,
                ElectronicOrderingUsed = true,
                ElectronicInvoicingUsed = true,
                ElectronicPaymentUsed = true
            },
            ProceduresForReview = new ProceduresForReviewInformationConfiguration {
                ReviewBody = ContractBodyContactInformationConfigurationDefault,
                ReviewProcedure = true
            },
            AttachmentInformation = new AttachmentInformationConfiguration {
                Description = true,
                Links = new LinkConfiguration {
                    Description = true,
                    Url = true
                }
            }
        };
    }

}
