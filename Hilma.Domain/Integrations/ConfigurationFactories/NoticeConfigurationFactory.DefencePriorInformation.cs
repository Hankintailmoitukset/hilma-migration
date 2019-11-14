using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration defencePriorInformation = new NoticeContractConfiguration {
            PreviousNoticeOjsNumber = true,
            Project = BasicProjectConfiguration,
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
                    QualityCriteria = new AwardCriterionDefinitionConfiguration { Criterion = true, Weighting = true } ,
                    CostCriteria = new AwardCriterionDefinitionConfiguration { Criterion = true, Weighting = true },
                    PriceCriterion = new AwardCriterionDefinitionConfiguration { Weighting = true }
                },
                AwardContract = null,
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
            ConditionsInformationDefence = new ConditionsInformationDefenceConfiguration {
                FinancingConditions = true,
                RestrictedToShelteredProgrammes = true,
                RestrictedToShelteredWorkshops = true,
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
                ElectronicCommunicationRequiresSpecialTools = false,
                ElectronicCommunicationInfoUrl = true,
                SendTendersOption = false,
                AddressToSendTenders = ContractBodyContactInformationConfigurationDefault,
            },
            ContactPerson = new ContactPersonConfiguration {
                Name = true,
                Email = true,
                Phone = true
            },
            ProcurementObject = new ProcurementObjectConfiguration {
                ShortDescription = false,
                EstimatedValue = new ValueRangeContractConfiguration { Currency = true, Value = true },
                MainCpvCode = new CpvCodeConfiguration { Code = true, VocCodes = new VocCodeConfiguration { Code = true } },
                Defence = new ProcurementObjectDefenceConfiguration {
                    FrameworkAgreement = new FrameworkAgreementInformationConfiguration
                    {
                        IncludesFrameworkAgreement = true,
                    },
                    TimeFrame = new TimeFrameConfiguration
                    {
                        ScheduledStartDateOfAwardProcedures = true
                    },
                    AdditionalInformation = true,
                    TotalQuantityOrScope = new ValueRangeContractConfiguration
                    {
                        Type = true
                    }
                }
            },
            ProcedureInformation = null,
            ComplementaryInformation = new ComplementaryInformationConfiguration {
                Defence = new ComplementaryInformationDefenceConfiguration
                {
                    EuFunds = new EuFundsConfiguration
                    {
                        ProjectIdentification = true,
                        ProcurementRelatedToEuProgram = true
                    },
                },
                AdditionalInformation = true,
                IsRecurringProcurement = false
            },
            LotsInfo = new LotsInfoConfiguration
            {
                DivisionLots = true
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
