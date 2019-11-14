using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration socialPriorInformation = new NoticeContractConfiguration {
            PreviousNoticeOjsNumber = true,
            Project = BasicProjectConfiguration,
            LotsInfo = new LotsInfoConfiguration()
            {
                DivisionLots = true,
                QuantityOfLots = true,
                LotsMaxAwarded = true,
                LotsMaxAwardedQuantity = true,
                LotsSubmittedFor = false,
                LotsSubmittedForQuantity = true,
                LotCombinationPossible = true,
                LotCombinationPossibleDescription = true
            },
            ObjectDescriptions = new ObjectDescriptionConfiguration {
                Title = true,
                LotNumber = true,
                AdditionalCpvCodes = new CpvCodeConfiguration { Code = true, VocCodes = new VocCodeConfiguration { Code = true } },
                NutsCodes = true,
                MainsiteplaceWorksDelivery = true,
                DescrProcurement = true,
                EstimatedValue = new ValueRangeContractConfiguration { Currency = false, Value = false },
                /*AwardCriteria = new AwardCriteriaConfiguration {
                    CriterionTypes = true,
                    QualityCriteria = new AwardCriterionDefinitionConfiguration { Criterion = true, Weighting = true } ,
                    CostCriteria = new AwardCriterionDefinitionConfiguration { Criterion = true, Weighting = true },
                    PriceCriterion = new AwardCriterionDefinitionConfiguration { Weighting = true }
                },*/
                TimeFrame = new TimeFrameConfiguration {
                    Type = false,
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
                    ProcurementRelatedToEuProgram = false,
                    ProjectIdentification = false
                },
                AdditionalInformation = true
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
            TenderingInformation = new TenderingInformationConfiguration {
                TendersMustBeValidForMonths = false,
                TendersMustBeValidUntil = false,
                TendersOrRequestsToParticipateDueDateTime = true,
                TendersMustBeValidOption = false, 
                EstimatedDateOfInvitations = false,
                Languages = true,
                EstimatedDateOfContractNoticePublication = true
            },
            ComplementaryInformation = new ComplementaryInformationConfiguration
            {
                AdditionalInformation = true
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
