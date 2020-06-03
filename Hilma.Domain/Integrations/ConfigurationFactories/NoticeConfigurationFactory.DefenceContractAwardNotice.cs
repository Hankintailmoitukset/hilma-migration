using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration DefenceContractAward => new NoticeContractConfiguration {
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
                AwardCriteria = new AwardCriteriaConfiguration {
                    CriterionTypes = true,
                    QualityCriteria = new AwardCriterionDefinitionConfiguration { Criterion = true, Weighting = true },
                    CostCriteria = new AwardCriterionDefinitionConfiguration { Criterion = true, Weighting = true },
                    PriceCriterion = new AwardCriterionDefinitionConfiguration { Weighting = true }
                },
                AwardContract = new AwardConfiguration(),
                TimeFrame = new TimeFrameConfiguration {
                    Type = true,
                    BeginDate = true,
                    EndDate = true,
                    CanBeRenewed = true,
                    Days = true,
                    Months = true,
                    RenewalDescription = true
                },
                CandidateNumberRestrictions = new CandidateNumberRestrictionsConfiguration {
                    EnvisagedNumber = true,
                    EnvisagedMinimumNumber = true,
                    EnvisagedMaximumNumber = true,
                    ObjectiveCriteriaForChoosing = true,
                    Selected = true
                },
                OptionsAndVariants = new OptionsAndVariantsConfiguration {
                    Options = true,
                    OptionsDescription = true,
                    VariantsWillBeAccepted = true
                },
                TendersMustBePresentedAsElectronicCatalogs = true,
                EuFunds = new EuFundsConfiguration {
                    ProcurementRelatedToEuProgram = true,
                    ProjectIdentification = true
                },
                AdditionalInformation = true,
                MainCpvCode = new CpvCodeConfiguration
                {
                    Code = true
                },
                QuantityOrScope = true
            },
            ConditionsInformationDefence = null,
            CommunicationInformation = new CommunicationInformationConfiguration {
                ProcurementDocumentsAvailable = false,
                ProcurementDocumentsUrl = true,
                AdditionalInformation = false,
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
                SendTendersOption = false,
                AddressToSendTenders = ContractBodyContactInformationConfigurationDefault,
                SpecsAndAdditionalDocuments = false
            },
            ContactPerson = new ContactPersonConfiguration {
                Name = true,
                Email = true,
                Phone = true
            },
            ProcurementObject = new ProcurementObjectConfiguration {
                ShortDescription = true,
                EstimatedValue = new ValueRangeContractConfiguration { Currency = true, Value = true },
                MainCpvCode = new CpvCodeConfiguration { Code = true, VocCodes = new VocCodeConfiguration { Code = true } },
                Defence = new ProcurementObjectDefenceConfiguration {
                    FrameworkAgreement = new FrameworkAgreementInformationConfiguration
                    {
                        IncludesFrameworkAgreement = true,
                    }
                },
                TotalValue = new ValueRangeContractConfiguration { Currency = true, Value = true, Type = true, MaxValue = true, MinValue = true },
            },
            ProcedureInformation = new ProcedureInformationConfiguration {
                ProcedureType = true,
                AcceleratedProcedure = true,
                JustificationForAcceleratedProcedure = true,
                ElectronicAuctionWillBeUsed = true,
                AdditionalInformationAboutElectronicAuction = true,
                ProcurementGovernedByGPA = true,
                ReductionRecourseToReduceNumberOfSolutions = true,
                ReserveRightToAwardWithoutNegotiations = true,
                FrameworkAgreement = new FrameworkAgreementInformationConfiguration {
                    EnvisagedNumberOfParticipants = true,
                    FrameworkAgreementType = true,
                    IncludesFrameworkAgreement = true
                }
            },
            TenderingInformation = new TenderingInformationConfiguration {
                TendersMustBeValidForMonths = true,
                TendersMustBeValidUntil = true,
                TendersOrRequestsToParticipateDueDateTime = false,
                TendersMustBeValidOption = true,
                EstimatedDateOfInvitations = false,
                Languages = true,
                TenderOpeningConditions = new TenderOpeningConditionsConfiguration {
                    OpeningDateAndTime = true,
                    Place = true,
                    InformationAboutAuthorisedPersons = true
                },
                Defence = new DefenceAdministrativeInformationConfiguration
                {
                    HasPreviousContractNoticeOjsNumber = true,
                    PreviousContractNoticeOjsNumber = new OjsNumberConfiguration { Number = true, Date = true },
                    HasPreviousExAnteOjsNumber = true,
                    PreviousExAnteOjsNumber = new OjsNumberConfiguration { Number = true, Date = true },
                    Languages = false
                }
            },
            ComplementaryInformation = new ComplementaryInformationConfiguration {
                IsRecurringProcurement = false,
                Defence = new ComplementaryInformationDefenceConfiguration
                {
                    EuFunds = new EuFundsConfiguration
                    {
                        ProjectIdentification = true,
                        ProcurementRelatedToEuProgram = true
                    },
                },
                AdditionalInformation = true,
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
            },
            ContractAwardsDefence = new ContractAwardDefenceConfiguration {

                AllOrCertainSubcontractsWillBeAwarded = true,
                AnnualOrMonthlyValue = new TimeFrameConfiguration {
                    BeginDate = true,
                    Type = true,
                    Years = true,
                    ScheduledStartDateOfAwardProcedures = true,
                    CanBeRenewed = true,
                    Days = true,
                    EndDate = true,
                    Months = true,
                    RenewalDescription = true
                },
                ContractAwardDecisionDate = true,
                ContractNumber = true,
                Contractor = new ContractorContactInformationConfiguration() {
                ContractId  =true,
                Email = true,
                PostalAddress = new PostalAddressConfiguration() {
                    Country = true,
                    PostalCode = true,
                    Town = true,
                    StreetAddress = true
                },
                IsSmallMediumEnterprise = true,
                MainUrl = true,
                NationalRegistrationNumber = true,
                NutsCodes = true,
                OfficialName = true,
                TelephoneNumber = true,
                ValidationState = true
                },
                ContractValueType = true,
                EstimatedValue = new ValueContractConfiguration() {
                Currency = true,
                Value = true
                },
                FinalTotalValue = new ValueContractConfiguration() {
                    Currency = true,
                    Value = true
                },
                HighestOffer = new ValueContractConfiguration() {
                    Currency = true,
                    Value = true
                },
                LikelyToBeSubcontracted = true,
                LotNumber = true,
                LotTitle = true,
                LowestOffer = new ValueContractConfiguration() {
                    Currency = true,
                    Value = true
                },
                NumberOfTenders = new NumberOfTendersConfiguration() {
                    DisagreeTenderInformationToBePublished = true,
                    Electronic = true,
                    NonEu = true,
                    OtherEu = true,
                    Sme = true,
                    Total = true
                },
                ProportionOfValue = true,
                ShareOfContractWillBeSubcontracted = true,
                ShareOfContractWillBeSubcontractedMaxPercentage =  true,
                ShareOfContractWillBeSubcontractedMinPercentage = true,
                SubcontractingDescription = true,
                ValidationState = true,
                ValueOfSubcontract = new ValueContractConfiguration() {
                    Currency = true,
                    Value = true
                },
                ValueOfSubcontractNotKnown = true
            },
            Annexes = new AnnexConfiguration
            {
                D3 = new AnnexD3Configuration
                {
                    NoTenders = true,
                    ProductsManufacturedForResearch = true,
                    ProvidedByOnlyParticularOperator = true,
                    ExtremeUrgency = true,
                    AdditionalDeliveries = true,
                    RepetitionExisting = true,
                    AdvantageousPurchaseReason = true,
                    AllTenders = true,
                    CrisisUrgency = true,
                    Justification = true,
                    MaritimeService = true,
                    OtherJustification = true,
                    OtherServices = true,
                    ProcedureType = true,
                    ReasonForNoCompetition = true,
                    CommodityMarket = true,
                    AdvantageousTerms = true
                }
            }
        };
    }

}
