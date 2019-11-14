
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ProcurementObject for Ted integration
    /// </summary>
    public class ProcurementObjectConfiguration
    {
        
        
        public bool ShortDescription {get; set;} = false;
        public ValueRangeContractConfiguration EstimatedValue {get; set;} = new ValueRangeContractConfiguration();
        public CpvCodeConfiguration MainCpvCode {get; set;} = new CpvCodeConfiguration();
        public ValueRangeContractConfiguration TotalValue {get; set;} = new ValueRangeContractConfiguration();
        public ProcurementObjectDefenceConfiguration Defence {get; set;} = new ProcurementObjectDefenceConfiguration();
        public bool ValidationState {get; set;} = false;
    }
}
