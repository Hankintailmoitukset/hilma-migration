using System;
using System.ComponentModel.DataAnnotations;
using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;

namespace Hilma.Domain.DataContracts
{
    [Contract]
    public class DepartmentContract
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// Hilma specific additional specifier for Official Name, to distinguish multiple
        /// departments of same organisation from each other.
        /// </summary>
        [MaxLength(300)]
        public string Department { get; set; }

        /// <summary>
        /// Location code for the organisation
        /// </summary>
        [Required]
        [MinLength(1), MaxLength(20)]
        public string[] NutsCodes { get; set; }

        /// <summary>
        ///     Postal address for the contact.
        /// </summary>
        public PostalAddress PostalAddress { get; set; }

        /// <summary>
        ///     Phone number for the contact. Format is important.
        /// </summary>
        /// <example>
        ///     +358 123123123
        /// </example>
        [MaxLength(100)]
        public string TelephoneNumber { get; set; }

        /// <summary>
        ///     Contact email.
        /// </summary>
        /// <example>
        ///     tendering@innofactor.com
        /// </example>
        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        /// <summary>
        /// Contact point for the organisation.
        /// </summary>
        [MaxLength(300)]
        public string ContactPerson { get; set; }

        /// <summary>
        ///     Url, including the protocol, for additional info.
        /// </summary>
        /// <example>
        ///     https://www.innofactor.com
        /// </example>
        [MaxLength(200)]
        public string MainUrl { get; set; }

        public ContractingAuthorityType ContractingAuthorityType { get; set; }

        /// <summary>
        ///     Asked if ContractingAuthorityType is "Other"
        /// </summary>
        [MaxLength(1000)]
        public string OtherContractingAuthorityType { get; set; }

        /// <summary>
        /// Used in F24 and F25 to determine type of main activity:
        ///  (in the case of a notice published by a contracting authority)
        ///  or
        ///  (in the case of a notice published by a contracting entity)
        /// </summary>
        public ContractingType ContractingType { get; set; }

        /// <summary>
        ///     Primary field of operations of the organisation.
        /// </summary>
        public MainActivity MainActivity { get; set; }

        /// <summary>
        ///     Asked if MainActivity is "Other"
        /// </summary>
        public string OtherMainActivity { get; set; }

        /// <summary>
        ///     Main activity utilities.
        /// </summary>
        public MainActivityUtilities MainActivityUtilities { get; set; }

        /// <summary>
        ///     Vuejs application persistent validation state.
        /// </summary>
        public ValidationState ValidationState { get; set; }
    }
}
