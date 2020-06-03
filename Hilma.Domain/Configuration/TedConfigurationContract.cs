using Hilma.Domain.Attributes;

namespace Hilma.Domain.Configuration
{
    /// <summary>
    ///     Configuration related to TED integration by ETS users (change notice status in simulation or stop publication).
    /// </summary>
    [Contract]
    public class TedConfigurationContract
    {
        /// <summary>
        /// Ted username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Ted user password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Ted api url.
        /// </summary>
        /// <example>
        /// https://esentool.ted.europa.eu/api/simulation/latest/
        /// </example>
        public string ESenderApiUrl { get; set; }

        /// <summary>
        /// If publishing to TED simulation environment
        /// </summary>
        public bool IsSimulation { get; set; }
    }
}
