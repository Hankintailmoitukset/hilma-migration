using Hilma.Domain.Attributes;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Validators;

namespace Hilma.Domain.Entities
{
    /// <summary>
    /// V.1.3) Information about a framework agreement or a dynamic purchasing system
    /// </summary>
    [Contract]
    public class FrameworkAgreementInformation  
    {
        /// <summary>
        /// The procurement involves the establishment of a framework agreement
        /// </summary>
        [CorrigendumLabel("notice_involves_framework", "IV.1.3")]
        public bool IncludesFrameworkAgreement { get; set; }

        /// <summary>
        /// Directive 2009/81/EC
        /// The procurement involves the establishment of a framework agreement (incorrect in TED form - correct value in Excel)
        /// </summary>
        [CorrigendumLabel("notice_involves_framework_conclusion", "IV.1.3")]
        public bool IncludesConclusionOfFrameworkAgreement { get; set; }

        /// <summary>
        /// Defines if framework agreement for single or several providers
        /// </summary>
        [CorrigendumLabel("notice_involves_framework", "IV.1.3")]
        public FrameworkAgreementType FrameworkAgreementType { get; set; }

        /// <summary>
        /// Envisaged maximum number of participants to the framework agreement
        /// </summary>
        [CorrigendumLabel("framework_particip_envis", "IV.1.3")]
        public int? EnvisagedNumberOfParticipants { get; set; }

        /// <summary>
        /// Directive 2009/81/EC
        /// II.1.4) Information on framework agreement 
        /// Framework agreement with several operators -> Number exact or max
        /// </summary>
        [CorrigendumLabel("number", "II.1.4")]
        public FrameworkEnvisagedType FrameworkEnvisagedType { get; set; }

        /// <summary>
        /// The procurement involves the setting up of a dynamic purchasing system
        /// </summary>
        [CorrigendumLabel("notice_involves_dps", "IV.1.3")]
        public bool IncludesDynamicPurchasingSystem { get; set; }

        /// <summary>
        /// The dynamic purchasing system might be used by additional purchasers
        /// </summary>
        [CorrigendumLabel("dps_purchasers", "IV.1.3")]
        public bool DynamicPurchasingSystemInvolvesAdditionalPurchasers { get; set; }

        /// <summary>
        /// In the case of framework agreements, provide justification for any duration exceeding 4 years
        /// </summary>
        [CorrigendumLabel("framework_just_four", "IV.1.3")]
        //[StringMaxLength(2000)]
        public string[] JustificationForDurationOverFourYears { get; set; }

        /// <summary>
        /// Directive 2009/81/EC (Defence contracts)
        /// In the case of framework agreements, provide justification for any duration exceeding 7 years
        /// </summary>
        [CorrigendumLabel("framework_just_seven", "IV.1.3")]
        //[StringMaxLength(2000)]
        public string[] JustificationForDurationOverSevenYears { get; set; }

        /// <summary>
        /// In the case of framework agreements, provide justification for any duration exceeding 8 years
        /// </summary>
        [CorrigendumLabel("framework_just_eight", "IV.1.3")]
        //[StringMaxLength(2000)]
        public string[] JustificationForDurationOverEightYears { get; set; }

        /// <summary>
        ///  If DPS was terminated. Used when creating a contract award.
        /// </summary>
        [CorrigendumLabel("termination_dps", "IV.2.8")]
        public bool DynamicPurchasingSystemWasTerminated { get; set; }

        /// <summary>
        /// Directive 2009/81/EC (Defence contracts)
        /// II.1.4) Estimated total value of purchases for the entire duration of the framework agreement
        /// </summary>
        public ValueRangeContract EstimatedTotalValue { get; set; } = new ValueRangeContract();


        /// <summary>
        /// Directive 2009/81/EC (Defence contracts)
        /// II.1.4) Duration of the framework agreement
        /// </summary>
        public TimeFrame Duration { get; set; } = new TimeFrame();

        /// <summary>
        /// Directive 2009/81/EC
        /// Frequency and value of the contracts to be awarded
        /// </summary>
        [CorrigendumLabel("H_framework_freq", "II.1.4")]
        public string[] FrequencyAndValue { get; set; }
    }
}
