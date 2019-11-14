namespace Hilma.Domain.Configuration
{
    public class ServiceBusConfigurationContract
    {
        public bool PublishDisabled { get; set; }
        public bool SetDummyTedInformationOnPublish { get; set; }
        public string JoinRequestQueue { get; set; }
        public string TedPublishQueue { get; set; }
    }
}
