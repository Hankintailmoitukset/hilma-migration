using Hilma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.Configuration
{
    [Contract]
    public class ReviewBodyConfigurationContract
    {
        public string OfficialName { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public string MainUrl { get; set; }
    }
}
