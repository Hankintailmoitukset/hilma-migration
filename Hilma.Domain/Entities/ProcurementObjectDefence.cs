using Hilma.Domain.Attributes;
using Hilma.Domain.DataContracts;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hilma.Domain.Entities
{
    /// <summary>
    /// Directive 2009/81/EC
    /// Section II: Object of the contract
    /// Procurement object for defence contracts
    /// </summary>
    [Owned]
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
        /// II.1.3 - II.1.4
        /// Information about a framework agreement or a dynamic purchasing systemInformation about 
        /// </summary>
        public FrameworkAgreementInformation FrameworkAgreement { get; set; }

        /// <summary>
        /// II.1.6
        /// </summary>
        public CpvCode[] AdditionalCpvCodes { get; set; }

        /// <summary>
        /// II.1.7) Information about subcontracting (if applicable)
        /// </summary>
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
        public ValueRangeContract TotalQuantityOrScope { get; set; } = new ValueRangeContract();

        /// <summary>
        ///  Total quantity or scope
        /// </summary>
        [CorrigendumLabel("currency", "II.2.1")]
        public string[] TotalQuantity { get; set; }

        /// <summary>
        /// II.2.3) Information about renewals 
        /// </summary>
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
        public string[] AdditionalInformation { get; set; }
    }
}
