using System;
using Hilma.Domain.Attributes;
using Hilma.Domain.Validators;

namespace Hilma.Domain.Entities
{
    /// <summary>
    /// Conditions for opening of tenders
    /// </summary>
    [Contract]
    public class TenderOpeningConditions
    {
        /// <summary>
        /// Opening date and time in UTC
        /// </summary>
        [CorrigendumLabel("opening_conditions", "IV.2.7")]
        public DateTime? OpeningDateAndTime { get; set; }

        /// <summary>
        /// Place
        /// </summary>
        [CorrigendumLabel("opening_place", "IV.2.7")]
        [StringMaxLength(400)]
        public string[] Place { get; set; }

        /// <summary>
        /// Information about authorised persons and opening procedure
        /// </summary>
        [CorrigendumLabel("opening_addit_info", "IV.2.7")]
        [StringMaxLength(1000)]
        public string[] InformationAboutAuthorisedPersons { get; set; }
    }
}
