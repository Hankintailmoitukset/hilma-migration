using Microsoft.Azure.Search.Models;

namespace Hilma.Domain.Entities {
    /// <summary>
    ///     Search parameters for azure search
    /// </summary>
    public class HilmaSearchParameters : SearchParameters
    {
        /// <summary>
        ///     Search query for azure search.
        /// </summary>
        public string Search { get; set; }
    }
}
