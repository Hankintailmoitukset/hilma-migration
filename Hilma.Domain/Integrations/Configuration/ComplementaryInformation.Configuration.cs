
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ComplementaryInformation for Ted integration
    /// </summary>
    public class ComplementaryInformationConfiguration
    {
        
        
        public bool IsRecurringProcurement {get; set;} = false;
        public bool EstimatedTimingForFurtherNoticePublish {get; set;} = false;
        public bool ElectronicOrderingUsed {get; set;} = false;
        public bool ElectronicInvoicingUsed {get; set;} = false;
        public bool ElectronicPaymentUsed {get; set;} = false;
        public bool AdditionalInformation {get; set;} = false;
        public ComplementaryInformationDefenceConfiguration Defence {get; set;} = new ComplementaryInformationDefenceConfiguration();
        public bool ValidationState {get; set;} = false;
    }
}
