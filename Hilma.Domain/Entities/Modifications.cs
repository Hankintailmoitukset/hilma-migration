using Hilma.Domain.Attributes;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Enums;
using System.Collections.Generic;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     Section VII: Modifications to the contract/concession
    /// </summary>
    [Contract]
    public class Modifications
    {
        /// <summary>
        /// VII.1.1) Main CPV code
        /// </summary>
        [CorrigendumLabel("cpv_main", "VII.1.1")]
        public CpvCode MainCpvCode { get; set; }

        /// <summary>
        /// VII.1.2) Additional CPV code(s)
        /// </summary>
        [CorrigendumLabel("cpv_supplem", "VII.1.2")]
        public CpvCode[] AdditionalCpvCodes { get; set; } = new CpvCode[0];

        /// <summary>
        ///     VII.1.3) Place of performance
        /// </summary>
        [CorrigendumLabel("nutscode", "VII.1.3")]
        public string[] NutsCodes { get; set; } = new string[0];

        /// <summary>
        ///     VII.1.3) Place of performance
        /// </summary>
        [CorrigendumLabel("mainsiteplace_works_delivery", "VII.1.3")]
        public string[] MainsiteplaceWorksDelivery { get; set; }

        /// <summary>
        ///     VII.1.4) Description of the procurement:
        ///     (nature and quantity of works, supplies or services)
        /// </summary>
        [CorrigendumLabel("descr_procurement", "VII.1.4")]
        public string[] DescrProcurement { get; set; }

        /// <summary>
        ///     VII.1.5) Duration of the contract, framework agreement, dynamic purchasing system or concession
        /// </summary>
        [CorrigendumLabel("duration_contract_framework_dps", "VII.1.5")]
        public TimeFrame TimeFrame { get; set; } = new TimeFrame();

        /// <summary>
        /// Directive 2014/24/EU – In the case of framework agreements, provide justification for any duration exceeding 4 years
        /// </summary>
        [CorrigendumLabel("framework_just_four", "VII.1.5")]
        public string[] JustificationForDurationOverFourYears { get; set; }

        /// <summary>
        /// Directive 2014/25/EU – In the case of framework agreements, provide justification for any duration exceeding 8 years:
        /// </summary>
        [CorrigendumLabel("framework_just_eight", "VII.1.5")]
        public string[] JustificationForDurationOverEightYears { get; set; }

        /// <summary>
        ///     VII.1.6) Information on value of the contract/lot/concession (excluding VAT)
        /// </summary>
        [CorrigendumLabel("value_total_final_contract_concess", "VII.1.6")]
        public ValueContract TotalValue { get; set; } = new ValueContract();

        /// <summary>
        /// The contract/concession has been awarded to a group of economic operators ◯ yes ◯ no
        /// </summary>
        [CorrigendumLabel("awarded_to_group_of_economic_operators", "VII.1.7")]
        public bool AwardedToGroupOfEconomicOperators { get; set; }

        /// <summary>
        ///     VII.1.7) Name and address of the contractor/concessionaire 
        /// </summary>
        public List<ContractorContactInformation> Contractors { get; set; } = new List<ContractorContactInformation>();

        /// <summary>
        /// Description of the modifications
        /// Nature and extent of the modifications (with indication of possible earlier changes to the contract)
        /// </summary>
        [CorrigendumLabel("mod_descr_mod", "VII.2.1")]
        public string[] Description { get; set; }

        /// <summary>
        /// Reasons for modification
        /// </summary>
        [CorrigendumLabel("mod_reason", "VII.2.2" )]
        public ModificationReason Reason { get; set; }

        /// <summary>
        /// Description of the economic or technical reasons and the inconvenience or duplication of cost preventing a change of contractor
        /// </summary>
        [CorrigendumLabel("mod_descr_prevent_change", "VII.2.2")]
        public string[] ReasonDescriptionEconomic { get; set; }

        /// <summary>
        /// Description of the circumstances which rendered the modification necessary and explanation of the unforeseen nature of these circumstances
        /// </summary>
        [CorrigendumLabel("mod_descr_circums", "VII.2.2")]
        public string[] ReasonDescriptionCircumstances { get; set; }

        /// <summary>
        /// VII.2.3) Increase in price
        /// Updated total contract value before the modifications
        /// </summary>
        [CorrigendumLabel("mod_reason", "VII.2.3")]
        public ValueContract IncreaseBeforeModifications { get; set; } = new ValueContract();

        /// <summary>
        /// VII.2.3) Increase in price
        /// Total contract value after the modifications
        /// </summary>
        [CorrigendumLabel("mod_reason", "VII.2.3")]
        public ValueContract IncreaseAfterModifications { get; set; } = new ValueContract();

        public ValidationState ValidationState { get; set; }
    }
}
