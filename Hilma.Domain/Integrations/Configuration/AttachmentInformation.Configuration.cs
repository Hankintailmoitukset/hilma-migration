
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of AttachmentInformation for Ted integration
    /// </summary>
    public class AttachmentInformationConfiguration
    {
        
        
        public bool Description {get; set;} = false;
        public LinkConfiguration Links {get; set;} = new LinkConfiguration();
        public bool ValidationState {get; set;} = false;
    }
}
