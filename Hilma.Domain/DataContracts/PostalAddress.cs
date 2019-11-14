using Hilma.Domain.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     Represents an address.
    /// </summary>
    [Owned][Contract]
    public class PostalAddress
    {
        /// <summary>
        ///     Street address.
        /// </summary>
        [CorrigendumLabel("address_postal", "I.1")]
        public string StreetAddress { get; set; }

        /// <summary>
        ///     Postal/zip code.
        /// </summary>
        [CorrigendumLabel("address_postcode", "I.1")]
        public string PostalCode { get; set; }

        /// <summary>
        ///     The town for the address.
        /// </summary>
        [CorrigendumLabel("address_town", "I.1")]
        public string Town { get; set; }

        /// <summary>
        ///     The country for the address.
        /// </summary>
        [CorrigendumLabel("address_country", "I.1")]
        public string Country { get; set; }
    }
}
