
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of AnnexD4 for Ted integration
    /// </summary>
    public class AnnexD4Configuration
    {
        
        
        public bool NoTenders {get; set;} = false;
        public bool ProvidedByOnlyParticularOperator {get; set;} = false;
        public bool ReasonForNoCompetition {get; set;} = false;
        public bool Justification {get; set;} = false;
    }
}
