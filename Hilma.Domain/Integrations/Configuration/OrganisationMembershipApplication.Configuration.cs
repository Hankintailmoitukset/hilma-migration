
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of OrganisationMembershipApplicationContract for Ted integration
    /// </summary>
    public class OrganisationMembershipApplicationContractConfiguration
    {
        
        
        public bool Subject {get; set;} = false;
        public bool Body {get; set;} = false;
        public bool Header {get; set;} = false;
        public bool SelectionHeader {get; set;} = false;
        public bool ApplicationId {get; set;} = false;
        public bool OrganisationId {get; set;} = false;
        public HandlerContractConfiguration Handlers {get; set;} = new HandlerContractConfiguration();
        public bool ApproverOptions {get; set;} = false;
    }
}
