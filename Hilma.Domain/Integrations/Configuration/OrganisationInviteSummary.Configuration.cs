
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of OrganisationInviteSummary for Ted integration
    /// </summary>
    public class OrganisationInviteSummaryConfiguration
    {
        
        
        public bool Id {get; set;} = false;
        public bool OrganisationName {get; set;} = false;
        public bool OrganisationIdentifier {get; set;} = false;
        public bool InviterName {get; set;} = false;
        public bool InviterEmail {get; set;} = false;
    }
}
