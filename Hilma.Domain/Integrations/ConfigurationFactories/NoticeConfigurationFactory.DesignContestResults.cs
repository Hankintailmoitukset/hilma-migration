using Hilma.Domain.Integrations.Configuration;

namespace Hilma.Domain.Integrations.ConfigurationFactories
{
    public partial class NoticeConfigurationFactory
    {
        private static NoticeContractConfiguration DesignContestResults => new NoticeContractConfiguration {
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
                CriteriaForEvaluationOfProjects = true,
                DisagreeCriteriaForEvaluationOfProjectsPublish = true
            },
            ComplementaryInformation = new ComplementaryInformationConfiguration {
                AdditionalInformation = true
            },
            ConditionsInformation = null,
            ProceduresForReview = new ProceduresForReviewInformationConfiguration {
                ReviewBody = ContractBodyContactInformationConfigurationDefault,
                ReviewProcedure = true
            },
            ResultsOfContest = new ResultsOfContestConfiguration
            {
                // All the things
            }
        };
    }

}
