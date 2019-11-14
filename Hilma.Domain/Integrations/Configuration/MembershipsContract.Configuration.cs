
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of MembershipsContract for Ted integration
    /// </summary>
    public class MembershipsContractConfiguration
    {
        
        
        public OrganisationContractConfiguration Member {get; set;} = new OrganisationContractConfiguration();
        public OrganisationContractConfiguration Pending {get; set;} = new OrganisationContractConfiguration();
    }
}
