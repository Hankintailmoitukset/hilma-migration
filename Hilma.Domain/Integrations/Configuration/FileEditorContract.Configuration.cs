
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of FileEditorContract for Ted integration
    /// </summary>
    public class FileEditorContractConfiguration
    {
        
        
        public bool Service {get; set;} = false;
        public bool Container {get; set;} = false;
        public bool SasToken {get; set;} = false;
        public bool BlobName {get; set;} = false;
        public bool FileName {get; set;} = false;
        public bool PreviewUrl {get; set;} = false;
    }
}
