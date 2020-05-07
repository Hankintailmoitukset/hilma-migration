using System.ComponentModel.DataAnnotations;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     Common Procurement Vocabulary. EU-sanctioned list of
    ///     6000 9-number codes for different products.
    /// </summary>
    /// <remarks>
    ///     https://eur-lex.europa.eu/LexUriServ/LexUriServ.do?uri=OJ:L:2008:074:0001:0375:FI:PDF
    /// </remarks>
    [Contract]
    public class CpvCode
    {
        /// <summary>
        ///     The code itself.
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        ///     Clear text explanation for the code. Supplied in the notice language. 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Optional 4-letter modifiers for the code.
        /// </summary>
        /// <example>
        ///     If we have code 600000000 and we imagine that means a car.
        ///     CA46 might mean "gasoline powered" and CA47 might mean
        ///     "diesel powered". These are made up, so be careful.
        /// </example>
        [CorrigendumLabel("cpv_supplem", "II.1.2")]
        public VocCode[] VocCodes { get; set; }
    }
}
