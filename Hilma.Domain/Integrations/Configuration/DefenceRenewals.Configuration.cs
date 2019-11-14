
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of DefenceRenewals for Ted integration
    /// </summary>
    public class DefenceRenewalsConfiguration
    {
        
        
        public bool CanBeRenewed {get; set;} = false;
        public ValueRangeContractConfiguration Amount {get; set;} = new ValueRangeContractConfiguration();
        public TimeFrameConfiguration SubsequentContract {get; set;} = new TimeFrameConfiguration();
    }
}
