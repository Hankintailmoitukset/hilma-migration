using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Entities.Annexes {
    /// <summary>
    /// Annex D3 for direct purchase
    /// </summary>
    [Contract]
    public class AnnexD3 : IJustifiable
    {
        /// <summary>
        ///     AD3.1.1) Got no valid tenders.
        ///     Not sent to TED, only to toggle visibility of AD1.1.2.
        /// </summary>
        public bool NoTenders { get; set; }

        /// <summary>
        ///     AD3.1.2) Type of the negotiated procedure: Open or Restricted.
        /// </summary>
        [CorrigendumLabel("d_just_for_wo_pub_200981", "AD3.1")]
        public AnnexProcedureType ProcedureType { get; set; }

        /// <summary>
        /// AD3.1.3) The contract concerns research and development services other than
        /// those referred to in Article 13 of Directive 2009/81/EC (for services
        /// and supplies only)
        /// </summary>
        [CorrigendumLabel("d_explain", "AD3.1")]
        public bool OtherServices { get; set; }

        /// <summary>
        ///     AD3.1.4) The contract involved is purely for the purpose of research,
        ///     experiment, study or development under the conditions stated in
        ///     the Directive
        /// </summary>
        [CorrigendumLabel("d_manuf_for_research", "AD3.1")]
        public bool ProductsManufacturedForResearch { get; set; }

        /// <summary>
        ///     AD3.1.5) How is this different form AD3.1.1, nobody knows, but then again,
        ///     such knowledge would be heresy.
        /// </summary>
        [CorrigendumLabel("d_all_tenders", "AD3.1")]
        public bool AllTenders { get; set; }

        /// <summary>
        ///     AD3.1.6) The solution is only provided by particular economic operator.
        ///     Not sent to TED, only to toggle visibility of AD1.1.7
        /// </summary>
        public bool ProvidedByOnlyParticularOperator { get; set; }

        /// <summary>
        ///     AD3.1.7) The reason why There was no competition in section AD3.1.6
        /// </summary>
        [CorrigendumLabel("d_can_provided_only", "AD3.1")]
        public ReasonForNoCompetition ReasonForNoCompetition { get; set; }

        /// <summary>
        ///     AD3.1.8) Extreme urgency brought about by events unforeseeable for the contracting
        ///     authority and in accordance with the strict conditions stated in the Directive
        /// </summary>
        [CorrigendumLabel("d_extreme_urgency_ca", "AD3.1")]
        public bool CrisisUrgency { get; set; }

        /// <summary>
        ///     AD3.1.9) Extreme urgency brought about by events unforeseeable for the contracting
        ///     authority and in accordance with the strict conditions stated in the Directive
        /// </summary>
        [CorrigendumLabel("d_extreme_urgency_ca", "AD3.1")]
        public bool ExtremeUrgency { get; set; }

        /// <summary>
        ///     AD3.1.10) Additional deliveries by the original supplier ordered under the strict
        ///     conditions stated in the Directive
        /// </summary>
        [CorrigendumLabel("d_addit_ordered", "AD3.1")]
        public bool AdditionalDeliveries { get; set; }

        /// <summary>
        ///     AD3.1.11) New works / services, constituting a repetition of existing works / services
        ///     and ordered in accordance with the strict conditions stated in the Directive
        /// </summary>
        [CorrigendumLabel("d_repetition_existing", "AD3.1")]
        public bool RepetitionExisting { get; set; }

        /// <summary>
        ///     AD1.1.12) Procurement of supplies quoted and purchased on a commodity market
        /// </summary>
        [CorrigendumLabel("d_commodity_market", "AD3.1")]
        public bool CommodityMarket { get; set; }

        /// <summary>
        ///     AD3.1.13) Purchase of supplies or services on particularly advantageous terms
        ///     Not sent to ted, toggles visibility of  AD3.1.14
        /// </summary>
        public bool AdvantageousTerms { get; set; }

        /// <summary>
        ///     AD3.1.14) Why there is advantageous reasons to procure directly
        /// </summary>
        [CorrigendumLabel("d_advantageous_terms", "AD3.1")]
        public AdvantageousPurchaseReason AdvantageousPurchaseReason { get; set; }

        /// <summary>
        /// AD3.1.15) Contract related to the provision of air and maritime transport
        /// services for the armed forces of a Member State deployed or to be deployed
        /// abroad, under the strict conditions stated in the directive
        /// </summary>
        [CorrigendumLabel("d_maritime_services", "AD3.1")]
        public bool MaritimeService { get; set; }

        /// <summary>
        /// Other justification
        /// D.13/D.14
        /// </summary>
        [CorrigendumLabel("d_just_other_defence", "AD3.1")]
        public D3OtherJustificationOptions OtherJustification { get; set; }

        /// <summary>
        ///     AD3.3.1) Please explain in a clear and comprehensive manner why the award of the
        ///     contract without prior publication in the Official Journal of the European
        ///     Union is lawful
        /// </summary>
        [CorrigendumLabel("d_explain", "AD3.3")]
        public string[] Justification { get; set; }
    }
}
