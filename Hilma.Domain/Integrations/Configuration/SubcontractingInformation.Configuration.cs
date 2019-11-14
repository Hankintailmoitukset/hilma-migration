
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of SubcontractingInformation for Ted integration
    /// </summary>
    public class SubcontractingInformationConfiguration
    {
        
        
        public bool TendererHasToIndicateShare {get; set;} = false;
        public bool TendererHasToIndicateChange {get; set;} = false;
        public bool CaMayOblige {get; set;} = false;
        public bool SuccessfulTenderer {get; set;} = false;
        public bool SuccessfulTendererMin {get; set;} = false;
        public bool SuccessfulTendererMax {get; set;} = false;
        public bool SuccessfulTendererToSpecify {get; set;} = false;
    }
}
