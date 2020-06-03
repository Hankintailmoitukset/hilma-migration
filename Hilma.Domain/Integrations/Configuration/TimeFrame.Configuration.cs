
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of TimeFrame for Ted integration
    /// </summary>
    public class TimeFrameConfiguration
    {
        
        
        public bool Type {get; set;} = false;
        public bool Days {get; set;} = false;
        public bool Months {get; set;} = false;
        public bool Years {get; set;} = false;
        public bool BeginDate {get; set;} = false;
        public bool EndDate {get; set;} = false;
        public bool CanBeRenewed {get; set;} = false;
        public bool RenewalDescription {get; set;} = false;
        public bool ScheduledStartDateOfAwardProcedures {get; set;} = false;
        public bool IsOverFourYears {get; set;} = false;
        public bool IsOverEightYears {get; set;} = false;
    }
}
