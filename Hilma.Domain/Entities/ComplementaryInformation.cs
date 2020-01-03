using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     Section VI: Complementary information
    /// </summary>
    [Contract]
    public class ComplementaryInformation
    {
        /// <summary>
        ///     Appears only for <see cref="NoticeType" />=2.
        ///     Specified if the procurement is recurring
        /// </summary>
        [CorrigendumLabel("recurrent_procurement", "VI.1")]
        public bool IsRecurringProcurement { get; set; }

        /// <summary>
        ///     Appears and is required if <see cref="IsRecurringProcurement"/> is true.
        ///     Free text to specify information when further contract notices will be published.
        /// </summary>
        [CorrigendumLabel("further_notices_timing", "VI.1")]
        //[StringMaxLength(400)]
        public string[] EstimatedTimingForFurtherNoticePublish { get; set; } = new string[0];

    /// <summary>
        ///     Appears only for <see cref="NoticeType" />=2.
        ///     Specified if electronic ordering is used for the procurement.
        /// </summary>
        [CorrigendumLabel("eordering_used", "VI.2")]
        public bool ElectronicOrderingUsed { get; set; }

        /// <summary>
        ///     Appears only for <see cref="NoticeType" />=2.
        ///     Specified if electronic invoicing is used for the procurement.
        /// </summary>
        [CorrigendumLabel("einvoicing_used", "VI.2")]
        public bool ElectronicInvoicingUsed { get; set; }

        /// <summary>
        ///     Appears only for <see cref="NoticeType" />=2.
        ///     Specified if electronic payment is used for the procurement.
        /// </summary>
        [CorrigendumLabel("epayment_used", "VI.2")]
        public bool ElectronicPaymentUsed { get; set; }

        /// <summary>
        ///     Additional information about how the participants should proceed in order
        ///     to attend to the procurements process
        /// </summary>
        [CorrigendumLabel("info_additional", "VI.3")]
        //[StringMaxLength(10000)]
        public string[] AdditionalInformation { get; set; } = new string[0];

        /// <summary>
        ///     Directive 2009/81/EC (Defence notices) 
        ///     Additional fields related to defence notices
        /// </summary>
        [CorrigendumLabel("eu_progr_info", "VI.2")]
        public ComplementaryInformationDefence Defence { get; set; } 

        /// <summary>
        ///     Vuejs application form validation sate for corresponding section.
        /// </summary>
        public ValidationState ValidationState { get; set; }

    }
}
