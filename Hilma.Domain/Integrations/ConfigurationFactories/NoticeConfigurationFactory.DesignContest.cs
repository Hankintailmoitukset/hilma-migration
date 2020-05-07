using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration DesignContest => new NoticeContractConfiguration {
            Project = new ProcurementProjectContractConfiguration()
            {
                Title = true,
                ReferenceNumber = true,
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
            ObjectDescriptions = new ObjectDescriptionConfiguration {
                AdditionalCpvCodes = new CpvCodeConfiguration { Code = true, VocCodes = new VocCodeConfiguration { Code = true } },
                DescrProcurement = true,
                EuFunds = new EuFundsConfiguration {
                    ProcurementRelatedToEuProgram = true,
                    ProjectIdentification = true
                }
            },
            CommunicationInformation = new CommunicationInformationConfiguration{
                ProcurementDocumentsAvailable = true,
                ProcurementDocumentsUrl = true,
                AdditionalInformation = true,
                SendTendersOption = true,
                ElectronicCommunicationRequiresSpecialTools = true
            },
            ConditionsInformation = new ConditionsInformationConfiguration {
                CiriteriaForTheSelectionOfParticipants = true,
                ParticipationIsReservedForProfession = true,
                IndicateProfession = true
            },
            ContactPerson = new ContactPersonConfiguration {
                Name = true,
                Email = true,
                Phone = true
            },
            ProcurementObject = new ProcurementObjectConfiguration {
                MainCpvCode = new CpvCodeConfiguration { Code = true, VocCodes = new VocCodeConfiguration { Code = true } }
            },
            ProcedureInformation = new ProcedureInformationConfiguration {
                ContestType = true,
                ContestParticipants = new ValueRangeContractConfiguration { Type = true, Value = true, MinValue = true, MaxValue = true },
                NamesOfParticipantsAlreadySelected = true,
                CriteriaForEvaluationOfProjects = true
            },
            TenderingInformation = new TenderingInformationConfiguration {
                TendersOrRequestsToParticipateDueDateTime = true,
                EstimatedDateOfInvitations = true,
                Languages = true,
            },
            ComplementaryInformation = new ComplementaryInformationConfiguration {
                AdditionalInformation = true
            },
            ProceduresForReview = new ProceduresForReviewInformationConfiguration {
                ReviewBody = ContractBodyContactInformationConfigurationDefault,
                ReviewProcedure = true
            },
            RewardsAndJury = new RewardsAndJuryConfiguration
            {
                DecisionOfTheJuryIsBinding = true,
                DetailsOfPayments = true,
                NamesOfSelectedMembersOfJury = true,
                NumberAndValueOfPrizes = true,
                PrizeAwarded = true,
                ServiceContractAwardedToWinner = true
            }
        };
    }

}
