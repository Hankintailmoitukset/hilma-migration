
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of LotContract for Ted integration
    /// </summary>
    public class LotContractConfiguration
    {
        
        
        public bool Title {get; set;} = false;
        public CpvCodeConfiguration CpvCodes {get; set;} = new CpvCodeConfiguration();
        public bool NutsCodes {get; set;} = false;
        public bool MainsiteplaceWorksDelivery {get; set;} = false;
        public bool DescrProcurement {get; set;} = false;
        public bool ValidationState {get; set;} = false;
    }
}
