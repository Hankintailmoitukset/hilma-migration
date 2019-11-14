using Hilma.Domain.Attributes;
using System.Collections.Generic;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Describes collection of issues that prevent publication to TED.
    /// </summary>
    [Contract]
    public class TedValidationReport
    {
        /// <summary>
        ///     Type of error reported. Technical, business rule etc.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        ///     List of problems.
        /// </summary>
        public List<TedValidationItem> Items { get; set; }
    }
}
