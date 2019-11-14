
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of NumberOfTenders for Ted integration
    /// </summary>
    public class NumberOfTendersConfiguration
    {
        
        
        public bool DisagreeTenderInformationToBePublished {get; set;} = false;
        public bool Total {get; set;} = false;
        public bool Sme {get; set;} = false;
        public bool OtherEu {get; set;} = false;
        public bool NonEu {get; set;} = false;
        public bool Electronic {get; set;} = false;
    }
}
