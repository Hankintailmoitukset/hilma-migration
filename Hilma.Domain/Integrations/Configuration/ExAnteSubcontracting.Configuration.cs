
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ExAnteSubcontracting for Ted integration
    /// </summary>
    public class ExAnteSubcontractingConfiguration
    {
        
        
        public bool AllOrCertainSubcontractsWillBeAwarded {get; set;} = false;
        public bool ShareOfContractWillBeSubcontracted {get; set;} = false;
        public bool ShareOfContractWillBeSubcontractedMinPercentage {get; set;} = false;
        public bool ShareOfContractWillBeSubcontractedMaxPercentage {get; set;} = false;
    }
}
