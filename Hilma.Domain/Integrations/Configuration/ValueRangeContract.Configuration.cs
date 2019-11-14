
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ValueRangeContract for Ted integration
    /// </summary>
    public class ValueRangeContractConfiguration
    {
        
        
        public bool Type {get; set;} = false;
        public bool Value {get; set;} = false;
        public bool MinValue {get; set;} = false;
        public bool MaxValue {get; set;} = false;
        public bool Currency {get; set;} = false;
        public bool DisagreeToBePublished {get; set;} = false;
        public bool DoesNotExceedNationalThreshold {get; set;} = false;
    }
}
