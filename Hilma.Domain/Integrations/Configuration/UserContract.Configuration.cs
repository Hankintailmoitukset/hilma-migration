
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of UserContract for Ted integration
    /// </summary>
    public class UserContractConfiguration
    {
        
        
        public bool Id {get; set;} = false;
        public bool Language {get; set;} = false;
        public bool Name {get; set;} = false;
        public bool ContactEmail {get; set;} = false;
        public bool FavouritedNotices {get; set;} = false;
        public WatcherConfiguration SavedWatchers {get; set;} = new WatcherConfiguration();
        public bool IsAdmin {get; set;} = false;
    }
}
