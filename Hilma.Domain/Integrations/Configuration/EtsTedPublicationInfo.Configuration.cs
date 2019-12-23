
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of EtsTedPublicationInfo for Ted integration
    /// </summary>
    public class EtsTedPublicationInfoConfiguration
    {
        
        
        public bool DocumentNumber {get; set;} = false;
        public TedLinksConfiguration Links {get; set;} = new TedLinksConfiguration();
        public bool OrderNumberInSeries {get; set;} = false;
        public bool PublicationDate {get; set;} = false;
        public bool PublicationRequestedDate {get; set;} = false;
    }
}
