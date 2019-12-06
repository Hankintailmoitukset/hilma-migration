using Hilma.Domain.Attributes;
using System;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// Directive 2009/81/EC (Defence notices!)
    /// </summary>
    [Contract]
    public class OjsNumber
    {
        /// <summary>
        /// Ojs number
        /// <example>2019/S 001-999999</example>
        /// </summary>
        //[RegularExpression(@"^(19|20)\d{2}/S (((00)?[1-9])|([0]?[1-9][0-9])|(1[0-9][0-9])|(2[0-5][0-9]))-\d{6}$",
        //    ErrorMessage = "Previous notice OJS Number must be correctly formatted")]
        public string Number { get; set; }

        /// <summary>
        /// Of
        /// </summary>
        public DateTime? Date { get; set; }
    }
}
