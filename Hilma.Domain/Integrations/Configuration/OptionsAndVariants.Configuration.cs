
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of OptionsAndVariants for Ted integration
    /// </summary>
    public class OptionsAndVariantsConfiguration
    {
        
        
        public bool VariantsWillBeAccepted {get; set;} = false;
        public bool PartialOffersWillBeAccepted {get; set;} = false;
        public bool Options {get; set;} = false;
        public bool OptionsDescription {get; set;} = false;
        public bool OptionType {get; set;} = false;
        public bool OptionsDays {get; set;} = false;
        public bool OptionsMonths {get; set;} = false;
    }
}
