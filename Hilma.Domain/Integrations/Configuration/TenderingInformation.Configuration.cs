
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of TenderingInformation for Ted integration
    /// </summary>
    public class TenderingInformationConfiguration
    {
        
        
        public bool TendersOrRequestsToParticipateDueDateTime {get; set;} = false;
        public bool EstimatedDateOfInvitations {get; set;} = false;
        public bool Languages {get; set;} = false;
        public bool TendersMustBeValidOption {get; set;} = false;
        public bool TendersMustBeValidUntil {get; set;} = false;
        public bool TendersMustBeValidForMonths {get; set;} = false;
        public bool EstimatedDateOfContractNoticePublication {get; set;} = false;
        public TenderOpeningConditionsConfiguration TenderOpeningConditions {get; set;} = new TenderOpeningConditionsConfiguration();
        public DefenceAdministrativeInformationConfiguration Defence {get; set;} = new DefenceAdministrativeInformationConfiguration();
        public TimeFrameConfiguration EstimatedExecutionTimeFrame {get; set;} = new TimeFrameConfiguration();
        public bool ScheduledStartDateOfAwardProcedures {get; set;} = false;
        public bool ValidationState {get; set;} = false;
    }
}
