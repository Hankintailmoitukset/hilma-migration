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
    public class VocCode
    {
        /// <summary>
        ///     The code itself.
        /// </summary>
        [Required]
        [CorrigendumLabel("cpv_main", "II.1.2")]
        public string Code { get; set; }

        /// <summary>
        ///     Clear text explanation for the code.
        /// </summary>
        public string Name { get; set; }
    }
}
