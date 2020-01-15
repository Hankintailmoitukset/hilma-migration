using Hilma.Domain.Attributes;

namespace Hilma.Domain.Configuration {
    /// <summary>
    ///     Configuration related to user uploaded files.
    /// </summary>
    [Contract]
    public class FileConfigurationContract
    {
        /// <summary>
        ///     Which extensions are forbidden
        /// </summary>
        public string ForbiddenExtensions { get; set; }

        /// <summary>
        ///     Disable malware scan for uploaded files.
        ///     This is usually wanted in local development.
        /// </summary>
        public bool DisableFileScan { get; set; }
    }
}
