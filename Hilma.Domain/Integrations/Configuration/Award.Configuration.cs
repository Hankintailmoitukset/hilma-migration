
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of Award for Ted integration
    /// </summary>
    public class AwardConfiguration
    {
        
        
        public bool ContractAwarded {get; set;} = false;
        public NonAwardConfiguration NoAwardedContract {get; set;} = new NonAwardConfiguration();
        public ContractAwardConfiguration AwardedContract {get; set;} = new ContractAwardConfiguration();
    }
}
