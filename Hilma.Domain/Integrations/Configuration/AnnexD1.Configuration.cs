
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of AnnexD1 for Ted integration
    /// </summary>
    public class AnnexD1Configuration
    {
        
        
        public bool NoTenders {get; set;} = false;
        public bool ProcedureType {get; set;} = false;
        public bool SuppliesManufacturedForResearch {get; set;} = false;
        public bool ProvidedByOnlyParticularOperator {get; set;} = false;
        public bool ReasonForNoCompetition {get; set;} = false;
        public bool ExtremeUrgency {get; set;} = false;
        public bool AdditionalDeliveries {get; set;} = false;
        public bool RepetitionExisting {get; set;} = false;
        public bool DesignContestAward {get; set;} = false;
        public bool CommodityMarket {get; set;} = false;
        public bool AdvantageousTerms {get; set;} = false;
        public bool AdvantageousPurchaseReason {get; set;} = false;
        public bool Justification {get; set;} = false;
    }
}
