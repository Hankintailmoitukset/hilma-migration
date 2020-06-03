using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     Contact information section for additional contracting body information on Hilma form.
    /// </summary>
    //[Owned]
    [Contract]
    public class ContractBodyContactInformation
    {
        /// <summary>
        /// Official name of the contracting body 
        /// </summary>
        //[Required]
        [CorrigendumLabel("name_official", "I.3")]
        //[MaxLength(300)]
        public string OfficialName { get; set; }

        /// <summary>
        /// National registration number of the contracting body
        /// </summary>
        /// <example>1732626-9</example>
        [CorrigendumLabel("national_id", "I.3")]
        //[MaxLength(100)]
        public string NationalRegistrationNumber { get; set; }

        /// <summary>
        /// Hilma specific additional specifier for Official Name, to distinguish multiple
        /// departments of same organisation from each other.
        /// </summary>
        //[MaxLength(300)]
        public string Department { get; set; }

        /// <summary>
        /// Location code for the organisation
        /// </summary>
        //[Required]
        //[MinLength(1), MaxLength(20)]
        [CorrigendumLabel("nutscode", "I.3")]
        public string[] NutsCodes { get; set; } = new string[0];

        /// <summary>
        ///     Postal address for the contact.
        /// </summary>
        [CorrigendumLabel("address_postal", "I.3")]
        public PostalAddress PostalAddress { get; set; }

        /// <summary>
        ///     Phone number for the contact. Format is important.
        /// </summary>
        /// <example>
        ///     +358 123123123
        /// </example>
        //[MaxLength(100)]
        [CorrigendumLabel("address_phone", "I.3")]
        public string TelephoneNumber { get; set; }

        /// <summary>
        ///     Contact email.
        /// </summary>
        /// <example>
        ///     tendering@innofactor.com
        /// </example>
        //[Required]
        //[MaxLength(200)]
        [CorrigendumLabel("address_email", "I.3")]
        public string Email { get; set; }

        /// <summary>
        /// Contact point for the organisation.
        /// </summary>
        //[MaxLength(300)]
        [CorrigendumLabel("contact_point", "I.3")]
        public string ContactPerson { get; set; }

        /// <summary>
        ///     Url, including the protocol, for additional info.
        /// </summary>
        /// <example>
        ///     https://www.innofactor.com
        /// </example>
        //[MaxLength(200)]
        [CorrigendumLabel("url_general", "I.3")]
        public string MainUrl { get; set; }

        /// <summary>
        ///     Vuejs application persistent validation state.
        /// </summary>
        public ValidationState ValidationState { get; set; }
    }
}
