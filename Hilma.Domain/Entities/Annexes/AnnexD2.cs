using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Entities.Annexes {
    /// <summary>
    /// Annex D2 for direct purchase.
    /// </summary>
    [Contract]
    public class AnnexD2 : IJustifiable
    {
        /// <summary>
        ///     AD2.1.1) Got no valid tenders.
        ///     Not sent to TED, only to toggle visibility of AD1.1.2.
        /// </summary>
        [CorrigendumLabel("d_no_tenders_in_response_call", "AD2.1")]
        public bool NoTenders { get; set; }

        /// <summary>
        ///     AD2.1.2) The contract involved is purely for the purpose of research,
        ///     experiment, study or development under the conditions stated in
        ///     the Directive
        /// </summary>
        [CorrigendumLabel("d_pure_research", "AD2.1")]
        public bool PureResearch { get; set; }

        /// <summary>
        ///     AD2.1.3) The solution is only provided by particular economic operator.
        ///     Not sent to TED, only to toggle visibility of AD2.1.4
        /// </summary>
        public bool ProvidedByOnlyParticularOperator { get; set; }

        /// <summary>
        ///     AD2.1.4) The reason why there was no competition in section AD2.1.3
        /// </summary>
        [CorrigendumLabel("d_can_provided_only", "AD2.1")]
        public ReasonForNoCompetition ReasonForNoCompetition { get; set; }

        /// <summary>
        ///     AD2.1.5) Extreme urgency brought about by events unforeseeable for the contracting
        ///     authority and in accordance with the strict conditions stated in the Directive
        /// </summary>
        [CorrigendumLabel("d_extreme_urgency_ca", "AD2.1")]
        public bool ExtremeUrgency { get; set; }

        /// <summary>
        ///     AD2.1.6) Additional deliveries by the original supplier ordered under the strict
        ///     conditions stated in the Directive
        /// </summary>
        [CorrigendumLabel("d_addit_ordered", "AD2.1")]
        public bool AdditionalDeliveries { get; set; }

        /// <summary>
        ///     AD2.1.7) New works / services, constituting a repetition of existing works / services
        ///     and ordered in accordance with the strict conditions stated in the Directive
        /// </summary>
        [CorrigendumLabel("d_repetition_existing", "AD2.1")]
        public bool RepetitionExisting { get; set; }

        /// <summary>
        ///     AD2.1.8) Service contract to be awarded to the winner or one of winners under the
        /// rules of a design contest
        /// </summary>
        [CorrigendumLabel("d_service_contract", "AD2.1")]
        public bool DesignContestAward { get; set; }

        /// <summary>
        ///     AD2.1.9) Procurement of supplies quoted and purchased on a commodity market
        /// </summary>
        [CorrigendumLabel("d_commodity_market", "AD2.1")]
        public bool CommodityMarket { get; set; }

        /// <summary>
        ///     AD2.1.10) Purchase of supplies or services on particularly advantageous terms
        ///     Not sent to ted, toggles visibility of  AD1.1.12
        /// </summary>
        public bool AdvantageousTerms { get; set; }

        /// <summary>
        ///     AD2.1.11) Why there is advantageous reasons to procure directly
        /// </summary>
        [CorrigendumLabel("d_advantageous_terms", "AD2.1")]
        public AdvantageousPurchaseReason AdvantageousPurchaseReason { get; set; }

        /// <summary>
        ///     AD1.1.12) Bargain purchase taking advantage of a particularly
        ///     advantageous opportunity available for a very short time at a price
        ///     considerably lower than market prices.
        /// </summary>
        [CorrigendumLabel("d_bargain_advantage", "AD2.1")]
        public bool BargainPurchase { get; set; }

        /// <summary>
        ///     AD2.3.1) Please explain in a clear and comprehensive manner why the award of the
        ///     contract without prior publication in the Official Journal of the European
        ///     Union is lawful
        /// </summary>
        [CorrigendumLabel("d_explain", "AD2.3")]
        public string[] Justification { get; set; }
    }
}
