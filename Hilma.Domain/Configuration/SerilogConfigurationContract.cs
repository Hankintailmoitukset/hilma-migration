using Hilma.Domain.Attributes;

namespace Hilma.Domain.Configuration
{
    [Contract]
    public class SerilogConfigurationContract
    {
        public string WorkspaceId { get; set; }
        public string AuthenticationId { get; set; }
        public string LogName { get; set; }
    }
}
