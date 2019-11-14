using Hilma.Domain.Attributes;

namespace Hilma.Domain.Configuration
{
    [Contract]
    public class AuthenticationConfigContract
    {
        public string HilmaAppId { get; set; }
        public string B2CAuthority { get; set; }
        public string B2CReset { get; set; }
        public string B2CWebUserScope { get; set; }
    }
}
