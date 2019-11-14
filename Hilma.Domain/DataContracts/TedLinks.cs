using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Describes links to TED publication in various languages.
    /// </summary>
    [Contract]
    public class TedLinks
    {
        /// <summary>
        ///     Link to finnish version.
        /// </summary>
        public string FI { get; set; }
        /// <summary>
        ///     Link to swedish version.
        /// </summary>
        public string SV { get; set; }
        /// <summary>
        ///     Link to english version.
        /// </summary>
        public string EN { get; set; }
    }
}
