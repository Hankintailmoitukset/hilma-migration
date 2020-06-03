using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// AttachmentInfo for stopping TED publication
    /// </summary>
    [Contract]
    public class AttachmentInfo
    {
        /// <summary>
        /// Base64 encoded file
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Filename of the attachment
        /// </summary>
        public string Filename { get; set; }
    }
}
