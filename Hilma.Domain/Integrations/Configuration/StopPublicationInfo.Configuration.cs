
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of StopPublicationInfo for Ted integration
    /// </summary>
    public class StopPublicationInfoConfiguration
    {
        
        
        public bool Description {get; set;} = false;
        public AttachmentInfoConfiguration Attachment {get; set;} = new AttachmentInfoConfiguration();
    }
}
