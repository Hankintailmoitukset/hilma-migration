using Hilma.Domain.Attributes;
using Hilma.Domain.DataContracts;

namespace Hilma.Domain.Entities
{
    /// <summary>
    /// Directive 2009/81/EC
    /// Section II: Object of the contract
    /// Procurement object for defence contracts
    /// </summary>
    //[Owned]
    [Contract]
    public class ProcurementObjectDefence
    {
        /// <summary>
        /// Main site or location of works, place of delivery or of performance
        /// </summary>
        [CorrigendumLabel("mainsiteplace_works_delivery", "II.1.2")]
        public string[] MainsiteplaceWorksDelivery { get; set; }

        /// <summary>
        ///     Location specifiers for the object.
        /// </summary>
        [CorrigendumLabel("nutscode", "II.1.2")]
        public string[] NutsCodes { get; set; } = new string[0];

        /// <summary>
        /// II.3 for prior info and award
        /// II.1.3 - II.1.4 for contracts
        /// Information about a framework agreement or a dynamic purchasing systemInformation about 
        /// </summary>
        [CorrigendumLabel("framework_info", "II.3")]
        public FrameworkAgreementInformation FrameworkAgreement { get; set; }

        /// <summary>
        /// II.5 for prior
        /// II.1.6 for contract
        /// II.1.5 for award
        /// </summary>
        [CorrigendumLabel("framework_info", "II.5")]
        public CpvCode[] AdditionalCpvCodes { get; set; }

        /// <summary>
        /// II.1.7) Information about subcontracting (if applicable)
        /// </summary>
        [CorrigendumLabel("subcontr_info", "II.1.7")]
        public SubcontractingInformation Subcontract { get; set; } = new SubcontractingInformation();

        /// <summary>
        /// II.1.9) Information about variants
        /// and
        /// II.2.2) Information about options
        /// </summary>
        [CorrigendumLabel("options_info", "II.2.11")]
        public OptionsAndVariants OptionsAndVariants { get; set; } = new OptionsAndVariants();

        /// <summary>
        /// II.2.1) Total quantity or scope
        /// </summary>
        [CorrigendumLabel("quantity_scope", "II.2.1")]
        public ValueRangeContract TotalQuantityOrScope { get; set; } = new ValueRangeContract();

        /// <summary>
        ///  Total quantity or scope (text)
        /// </summary>
        [CorrigendumLabel("quantity_scope", "II.2.1")]
        public string[] TotalQuantity { get; set; }

        /// <summary>
        /// II.2.3) Information about renewals 
        /// </summary>
        [CorrigendumLabel("renewals_info", "II.2.3")]
        public DefenceRenewals Renewals { get; set; } = new DefenceRenewals();

        /// <summary>
        ///     Duration of the contract or time limit for completion
        /// </summary>
        [CorrigendumLabel("contract_duration", "II.3")]
        public TimeFrame TimeFrame { get; set; } = new TimeFrame();

        /// <summary>
        /// Prior information!
        /// II.7) Additional information
        /// </summary>
        [CorrigendumLabel("additional_information", "II.7")]
        public string[] AdditionalInformation { get; set; }
    }
}
