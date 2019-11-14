
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of HilmaStatistics for Ted integration
    /// </summary>
    public class HilmaStatisticsConfiguration
    {
        
        
        public bool EnergyEfficiencyConsidered {get; set;} = false;
        public bool InnovationConsidered {get; set;} = false;
        public bool SMEParticipationConsidered {get; set;} = false;
        public bool ValidationState {get; set;} = false;
    }
}
