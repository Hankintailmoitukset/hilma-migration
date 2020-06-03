using Hilma.Domain.Attributes;

namespace Hilma.Domain.Configuration {
    /// <summary>
    ///     Configuration related to user uploaded files.
    /// </summary>
    [Contract]
    public class FileConfigurationContract
    {
        /// <summary>
        ///     Which extensions are allowed
        /// </summary>
        public string AllowedExtensions { get; set; }

        /// <summary>
        ///     Disable malware scan for uploaded files.
        ///     This is usually wanted in local development.
        /// </summary>
        public bool DisableFileScan { get; set; }

        /// <summary>
        ///     CDN Endpoint
        /// </summary>
        public string CdnEndpoint { get; set; }
    }
}
