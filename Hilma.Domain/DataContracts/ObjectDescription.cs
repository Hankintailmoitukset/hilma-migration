using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;
using Hilma.Domain.Enums;
using Hilma.Domain.Validators;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     II.2) Description (lot)
    ///     Describes target of the tender described by the notice.
    /// </summary>
    [Contract]
    public class ObjectDescription
    {
        /// <summary>
        ///     Name of the object. Omitted if the notice is not split into lots.
        /// </summary>
        [CorrigendumLabel("title_official", "II.2.1")]
        [StringMaxLength(400)]
        public string Title { get; set; }

        /// <summary>
        /// Lot number. Needed for corrigendums
        /// </summary>
        [CorrigendumLabel("lot_number", "II.2.1")]
        public int LotNumber { get; set; }

        /// <summary>
        /// Directive 2009/81/EC
        /// Annex B
        /// 2) Common procurement vocabulary (CPV)
        /// Main object
        /// </summary>
        public CpvCode MainCpvCode { get; set; } = new CpvCode();

        /// <summary>
        /// Directive 2009/81/EC
        /// Annex B
        /// 3) Quantity or scope
        /// </summary>
        public string[] QuantityOrScope { get; set; }

        /// <summary>
        ///     Specifiers for the target of the procurement described by this object.
        /// </summary>
        [CorrigendumLabel("cpv_supplem", "II.2.2")]
        public CpvCode[] AdditionalCpvCodes { get; set; } = new CpvCode[0];

        /// <summary>
        ///     Location specifiers for the object.
        /// </summary>
        // [MinLength(1)]
        [CorrigendumLabel("nutscode", "II.2.3")]
        public string[] NutsCodes { get; set; } = new string[0];

        /// <summary>
        ///     Describes the location in more granular manner than the nuts codes in free text.
        /// </summary>
        [CorrigendumLabel("mainsiteplace_works_delivery", "II.2.3")]
        [StringMaxLength(200)]
        public string[] MainsiteplaceWorksDelivery { get; set; }

        /// <summary>
        ///     Describes the object in free text.
        /// </summary>
        [CorrigendumLabel("descr_procurement", "II.2.4")]
        [StringMaxLength(10000)]
        public string[] DescrProcurement { get; set; }

        /// <summary>
        ///     Agree to publish
        /// </summary>
        [CorrigendumLabel("H_disagree_to_publish", "II.2.5")]
        public bool DisagreeAwardCriteriaToBePublished { get; set; }

        /// <summary>
        ///     Describes how this object is awarded in the bidding
        /// </summary>
        [CorrigendumLabel("award_criteria", "II.2.5")]
        public AwardCriteria AwardCriteria { get; set; } = new AwardCriteria();

        /// <summary>
        ///     Describes the estimated monetary value of this object.
        /// </summary>
        [CorrigendumLabel("value_magnitude_estimated", "II.2.6")]
        public ValueRangeContract EstimatedValue { get; set; } = new ValueRangeContract();

        /// <summary>
        ///     Describes the time frame this object is valid once awarded.
        /// </summary>
        [CorrigendumLabel("duration_contract_framework_dps", "II.2.7")]
        public TimeFrame TimeFrame { get; set; } = new TimeFrame();

        /// <summary>
        ///     Describes restrictions to number of bidders to be selected to win.
        /// </summary>
        [CorrigendumLabel("limit_operators", "II.2.9")]
        public CandidateNumberRestrictions CandidateNumberRestrictions { get; set; } = new CandidateNumberRestrictions();

        /// <summary>
        ///     Describes what options and variants are accepted by the procuring organisation.
        /// </summary>
        [CorrigendumLabel("options_info", "II.2.11")]
        public OptionsAndVariants OptionsAndVariants { get; set; } = new OptionsAndVariants();

        /// <summary>
        ///     If the offers must be presented as electronic catalogs.
        /// </summary>
        [CorrigendumLabel("electronic_catalogue_required", "II.2.12")]
        public bool TendersMustBePresentedAsElectronicCatalogs { get; set; }

        /// <summary>
        ///     If this objects purchase is funded by EU project.
        /// </summary>
        [CorrigendumLabel("eu_progr_info", "II.2.13")]
        public EuFunds EuFunds { get; set; } = new EuFunds();

        /// <summary>
        ///     More free text information about the project.
        /// </summary>
        [CorrigendumLabel("info_additional", "II.2.14")]
        [StringMaxLength(6000)]

        public string[] AdditionalInformation { get; set; }

        /// <summary>
        ///     The award contract for this lot. Used in contract award notices.
        /// </summary>
        public Award AwardContract { get; set; }

        /// <summary>
        ///     Validation state 
        /// </summary>
        public ValidationState ValidationState { get; set; }
    }
}
