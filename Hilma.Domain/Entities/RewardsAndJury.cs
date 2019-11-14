using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Entities
{
    /// <summary>
    /// IV.3) Rewards and jury
    /// </summary>
    [Contract]
    public class RewardsAndJury
    {
        #region IV.3.1) Information about prize(s)
        /// <summary>
        /// A prize/prizes will be awarded
        /// </summary>
        [CorrigendumLabel("prize_will_awarded", "IV.3.1")]
        public bool PrizeAwarded { get; set; }

        /// <summary>
        /// Number and value of the prize(s) to be awarded
        /// </summary>
        [CorrigendumLabel("dc_number_value_prizes", "IV.3.1")]
        public string[] NumberAndValueOfPrizes { get; set; }
        #endregion

        /// <summary>
        /// IV.3.2) Details of payments to all participants
        /// </summary>
        [CorrigendumLabel("dc_details_payment", "IV.3.2")]
        public string[] DetailsOfPayments { get; set; }

        /// <summary>
        /// IV.3.3) Follow-up contracts
        /// Any service contract following the contest will be awarded to the winner or winners of the contest 
        /// </summary>
        [CorrigendumLabel("dc_awarded_to_winner", "IV.3.3")]
        public bool ServiceContractAwardedToWinner { get; set; }

        /// <summary>
        /// IV.3.4) Decision of the jury
        /// The decision of the jury is binding on the contracting authority/entity
        /// </summary>
        [CorrigendumLabel("dc_decision_binding", "IV.3.4")]
        public bool DecisionOfTheJuryIsBinding { get; set; }

        /// <summary>
        /// IV.3.5) Names of the selected members of the jury
        /// </summary>
        [CorrigendumLabel("dc_names_jury", "IV.3.5")]
        public string[] NamesOfSelectedMembersOfJury { get; set; }

        #region VueJS
        /// <summary>
        ///     Validation state for Vuejs application.
        /// </summary>
        public ValidationState ValidationState { get; set; }
        #endregion
    }
}
