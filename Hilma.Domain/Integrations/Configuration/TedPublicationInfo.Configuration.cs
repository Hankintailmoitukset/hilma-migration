
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of TedPublicationInfo for Ted integration
    /// </summary>
    public class TedPublicationInfoConfiguration
    {
        
        
        public bool Ojs_number {get; set;} = false;
        public bool No_doc_ojs {get; set;} = false;
        public bool Publication_date {get; set;} = false;
        public TedLinksConfiguration Ted_links {get; set;} = new TedLinksConfiguration();
    }
}
