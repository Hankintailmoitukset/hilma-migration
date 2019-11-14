using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Entities.Annexes {
    /// <summary>
    /// Annex D1 for direct purchase.
    /// </summary>
    [Contract]
    public class AnnexD1 : IJustifiable
    {
        /// <summary>
        ///     AD1.1.1) Got no valid tenders.
        ///     Not sent to TED, only to toggle visibility of AD1.1.2.
        /// </summary>
        public bool NoTenders { get; set; }

        /// <summary>
        ///     AD1.1.2) Type of the negotiated procedure: Open or Restricted.
        /// </summary>
        [CorrigendumLabel("d_just_for_wo_pub_201424", "AD1.1")]
        public AnnexProcedureType ProcedureType { get; set; }

        /// <summary>
        ///     AD1.1.3) The supplies are manufactured purely for research/study/etc.
        /// </summary>
        [CorrigendumLabel("d_manuf_for_research", "AD1.1")]
        public bool SuppliesManufacturedForResearch { get; set; }

        /// <summary>
        ///     AD1.1.4) The solution is only provided by particular economic operator.
        ///     Not sent to TED, only to toggle visibility of AD1.1.5
        /// </summary>
        public bool ProvidedByOnlyParticularOperator { get; set; }

        /// <summary>
        ///     AD1.1.5) The reason why There was no competition in section AD1.1.4
        /// </summary>
        [CorrigendumLabel("d_can_provided_only", "AD1.1")]
        public ReasonForNoCompetition ReasonForNoCompetition { get; set; }

        /// <summary>
        ///     AD1.1.6) Extreme urgency brought about by events unforeseeable for the contracting
        ///     authority and in accordance with the strict conditions stated in the Directive
        /// </summary>
        [CorrigendumLabel("d_extreme_urgency_ca", "AD1.1")]
        public bool ExtremeUrgency { get; set; }

        /// <summary>
        ///     AD1.1.7) Additional deliveries by the original supplier ordered under the strict
        ///     conditions stated in the Directive
        /// </summary>
        [CorrigendumLabel("d_addit_ordered", "AD1.1")]
        public bool AdditionalDeliveries { get; set; }

        /// <summary>
        ///     AD1.1.8) New works / services, constituting a repetition of existing works / services
        ///     and ordered in accordance with the strict conditions stated in the Directive
        /// </summary>
        [CorrigendumLabel("d_repetition_existing", "AD1.1")]
        public bool RepetitionExisting { get; set; }

        /// <summary>
        ///     AD1.1.9) Service contract to be awarded to the winner or one of winners under the
        /// rules of a design contest
        /// </summary>
        [CorrigendumLabel("d_service_contract", "AD1.1")]
        public bool DesignContestAward { get; set; }

        /// <summary>
        ///     AD1.1.10) Procurement of supplies quoted and purchased on a commodity market
        /// </summary>
        [CorrigendumLabel("d_commodity_market", "AD1.1")]
        public bool CommodityMarket { get; set; }

        /// <summary>
        ///     AD1.1.11) Purchase of supplies or services on particularly advantageous terms
        ///     Not sent to ted, toggles visibility of  AD1.1.12
        /// </summary>
        public bool AdvantageousTerms { get; set; }

        /// <summary>
        ///     AD1.1.12) Why there is advantageous reasons to procure directly
        /// </summary>
        [CorrigendumLabel("d_advantageous_terms", "AD1.1")]
        public AdvantageousPurchaseReason AdvantageousPurchaseReason { get; set; }

        /// <summary>
        ///     AD1.3.1) Please explain in a clear and comprehensive manner why the award of the
        ///     contract without prior publication in the Official Journal of the European
        ///     Union is lawful
        /// </summary>
        [CorrigendumLabel("d_explain", "AD1.3")]
        public string[] Justification { get; set; }
    }
}
