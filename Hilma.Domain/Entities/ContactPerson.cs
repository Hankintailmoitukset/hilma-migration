using Hilma.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     Stores a contact person.
    /// </summary>
    [Contract]
    public class ContactPerson
    {
        /// <summary>
        ///     Name (given + family) of the contact person.
        /// </summary>
        /// <example>
        ///     Erin Example
        /// </example>
        [MaxLength(300)]
        [CorrigendumLabel("contactpoint", "I.1")]
        public string Name { get; set; }

        /// <summary>
        ///     Email address of the contact person.
        /// </summary>
        /// <example>
        ///     erin.example@innofactor.com
        /// </example>
        [MaxLength(200)]
        public string Email { get; set; }

        /// <summary>
        ///     Phone number of the contact person. Format is important for TED, refer to the example.
        /// </summary>
        /// <example>
        ///     +358 123123123
        /// </example>
        [MaxLength(100)]
        public string Phone { get; set; }
    }
}
