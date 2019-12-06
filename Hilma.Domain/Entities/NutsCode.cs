using Hilma.Domain.Attributes;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     The Nomenclature of Territorial Units for Statistics (NUTS)
    /// </summary>
    /// <remarks>
    ///     https://simap.ted.europa.eu/web/simap/nuts
    /// </remarks>
    [Contract]
    public class NutsCode
    {
        /// <summary>
        ///     The code itself.
        /// </summary>
        //[Required]
        public string Code { get; set; }

        /// <summary>
        ///     Clear text explanation for the code.
        /// </summary>
        public string Name { get; set; }
    }
}
