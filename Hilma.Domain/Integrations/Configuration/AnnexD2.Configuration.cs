
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of AnnexD2 for Ted integration
    /// </summary>
    public class AnnexD2Configuration
    {
        
        
        public bool NoTenders {get; set;} = false;
        public bool PureResearch {get; set;} = false;
        public bool ProvidedByOnlyParticularOperator {get; set;} = false;
        public bool ReasonForNoCompetition {get; set;} = false;
        public bool ExtremeUrgency {get; set;} = false;
        public bool AdditionalDeliveries {get; set;} = false;
        public bool RepetitionExisting {get; set;} = false;
        public bool DesignContestAward {get; set;} = false;
        public bool CommodityMarket {get; set;} = false;
        public bool AdvantageousTerms {get; set;} = false;
        public bool AdvantageousPurchaseReason {get; set;} = false;
        public bool BargainPurchase {get; set;} = false;
        public bool Justification {get; set;} = false;
    }
}
