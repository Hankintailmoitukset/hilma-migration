
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of NonAward for Ted integration
    /// </summary>
    public class NonAwardConfiguration
    {
        
        
        public bool FailureReason {get; set;} = false;
        public bool OriginalNoticeSentVia {get; set;} = false;
        public EsenderConfiguration OriginalEsender {get; set;} = new EsenderConfiguration();
        public bool OriginalNoticeSentViaOther {get; set;} = false;
        public bool OriginalNoticeSentDate {get; set;} = false;
    }
}
