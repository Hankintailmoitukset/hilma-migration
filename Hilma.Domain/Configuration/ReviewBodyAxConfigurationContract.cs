using Hilma.Domain.Attributes;

namespace Hilma.Domain.Configuration
{
    [Contract]
    public class ReviewBodyAxConfigurationContract
    {
        public string OfficialName { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public string MainUrl { get; set; }
        public string[] ReviewProcedure { get; set; }
    }
}
