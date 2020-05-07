using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// Used when stopping TED publication
    /// Complementary information about the process
    /// </summary>
    [Contract]
    public class StopPublicationInfo
    {
        /// <summary>
        /// Complementary information about the stop publication process
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Attachment information
        /// </summary>
        public AttachmentInfo Attachment { get; set; }
    }
}
