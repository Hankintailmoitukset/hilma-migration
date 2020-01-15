
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of SearchFilter for Ted integration
    /// </summary>
    public class SearchFilterConfiguration
    {
        
        
        public bool SearchTerms {get; set;} = false;
        public bool NutsSearchTerms {get; set;} = false;
        public bool CpvSearchTerms {get; set;} = false;
        public bool NoticeTypes {get; set;} = false;
        public bool NoticeAdditional {get; set;} = false;
        public bool PublishedAfter {get; set;} = false;
        public bool PublishedBefore {get; set;} = false;
        public bool CustomFilter {get; set;} = false;
        public bool Other {get; set;} = false;
    }
}
