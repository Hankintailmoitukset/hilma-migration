
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of Watcher for Ted integration
    /// </summary>
    public class WatcherConfiguration
    {
        
        
        public bool Id {get; set;} = false;
        public bool Name {get; set;} = false;
        public bool SearchParameters {get; set;} = false;
        public bool FrontEndParameters {get; set;} = false;
    }
}
