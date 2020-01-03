
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of AnnexD3 for Ted integration
    /// </summary>
    public class AnnexD3Configuration
    {
        
        
        public bool NoTenders {get; set;} = false;
        public bool ProcedureType {get; set;} = false;
        public bool OtherServices {get; set;} = false;
        public bool ProductsManufacturedForResearch {get; set;} = false;
        public bool AllTenders {get; set;} = false;
        public bool ProvidedByOnlyParticularOperator {get; set;} = false;
        public bool ReasonForNoCompetition {get; set;} = false;
        public bool CrisisUrgency {get; set;} = false;
        public bool ExtremeUrgency {get; set;} = false;
        public bool AdditionalDeliveries {get; set;} = false;
        public bool RepetitionExisting {get; set;} = false;
        public bool CommodityMarket {get; set;} = false;
        public bool AdvantageousTerms {get; set;} = false;
        public bool AdvantageousPurchaseReason {get; set;} = false;
        public bool MaritimeService {get; set;} = false;
        public bool OtherJustification {get; set;} = false;
        public bool Justification {get; set;} = false;
    }
}
