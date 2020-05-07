using Hilma.Domain.Attributes;
using System;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    /// Used by frontend search to strictly type filters
    /// </summary>
    [Contract]
    public class SearchFilter
    {
        public string[] SearchTerms { get; set; }
        public string[] SearchOptions { get; set; }
        public string[] NutsSearchTerms { get; set; }
        public string[] CpvSearchTerms { get; set; }
        public string[] NoticeTypes { get; set; }
        public string[] NoticeAdditional { get; set; }
        public string PublishedAfter { get; set; }
        public string PublishedBefore { get; set; }
        public string[] CustomFilter { get; set; }
        public string[] Other { get; set; }
    }
}
