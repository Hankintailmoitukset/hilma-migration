
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of RewardsAndJury for Ted integration
    /// </summary>
    public class RewardsAndJuryConfiguration
    {
        
        
        public bool PrizeAwarded {get; set;} = false;
        public bool NumberAndValueOfPrizes {get; set;} = false;
        public bool DetailsOfPayments {get; set;} = false;
        public bool ServiceContractAwardedToWinner {get; set;} = false;
        public bool DecisionOfTheJuryIsBinding {get; set;} = false;
        public bool NamesOfSelectedMembersOfJury {get; set;} = false;
        public bool ValidationState {get; set;} = false;
    }
}
