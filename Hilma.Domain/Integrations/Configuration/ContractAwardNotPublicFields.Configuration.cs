
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of ContractAwardNotPublicFields for Ted integration
    /// </summary>
    public class ContractAwardNotPublicFieldsConfiguration
    {
        
        
        public bool CommunityOrigin {get; set;} = false;
        public bool NonCommunityOrigin {get; set;} = false;
        public bool Countries {get; set;} = false;
        public bool AwardedToTendererWithVariant {get; set;} = false;
        public bool AbnormallyLowTendersExcluded {get; set;} = false;
    }
}
