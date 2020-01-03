
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of Modifier for Ted integration
    /// </summary>
    public class ModifierConfiguration
    {
        
        
        public bool DateModified {get; set;} = false;
        public bool UserId {get; set;} = false;
        public bool Name {get; set;} = false;
        public bool Email {get; set;} = false;
    }
}
