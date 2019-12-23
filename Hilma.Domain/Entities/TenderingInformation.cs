using Hilma.Domain.Attributes;
using Hilma.Domain.DataContracts;
using Hilma.Domain.Enums;
using System;

namespace Hilma.Domain.Entities
{
    /// <summary>
    /// IV.2) Administrative information
    /// </summary>
    [Contract]
    public class TenderingInformation   
    {
        /// <summary>
        /// Time limit (date and time) for receipt of tenders or requests to participate
        /// </summary>
        [CorrigendumLabel("limit_receipt_tenders_req_part", "IV.2.2")]
        public DateTime? TendersOrRequestsToParticipateDueDateTime { get; set; }

        /// <summary>
        /// Estimated date of dispatch of invitations to tender or to participate to selected candidates.
        /// Only if not open procedure 
        /// </summary>
        [CorrigendumLabel("date_dispatch_tender_participate", "IV.2.3")]
        public DateTime? EstimatedDateOfInvitations { get; set; }

        /// <summary>
        /// Languages in which tenders or requests to participate may be submitted
        /// </summary>
        [CorrigendumLabel("languages_allowed", "IV.2.4")]
        public string[] Languages { get; set; } = new string[0];

        /// <summary>
        ///     Defines how the minimum time tenders need to be valid is given.
        /// </summary>
        public TendersMustBeValidOption TendersMustBeValidOption { get; set; }
        
        /// <summary>
        /// Date for minimum time frame during which the tenderer must maintain the tender. Only if TendersMustBeValidOption.Date is selected. 
        /// </summary>
        [CorrigendumLabel("date_tender_valid", "IV.2.6")]
        public DateTime? TendersMustBeValidUntil { get; set; }

        /// <summary>
        /// Number of months for minimum time frame during which the tenderer must maintain the tender. Only if TendersMustBeValidOption.Months is selected. 
        /// </summary>
        [CorrigendumLabel("duration_months", "IV.2.6")]
        public int? TendersMustBeValidForMonths { get; set; }

        /// <summary>
        ///     Appears only for prior information notices. The estimated date of publishing
        ///     followup for the prior information notice.
        /// </summary>
        [CorrigendumLabel("date_dispatch", "II.3")]
        public DateTime? EstimatedDateOfContractNoticePublication { get; set; }

        /// <summary>
        /// Conditions for opening of tenders, only if procedure is open procedure
        /// </summary>
        public TenderOpeningConditions TenderOpeningConditions { get; set; } = new TenderOpeningConditions();

        /// <summary>
        /// In case of defence procurement (Directive 2009/81/EC)
        /// </summary>
        public DefenceAdministrativeInformation Defence { get; set; }

        /// <summary>
        /// Estimated execution timeframe for national agriculture contract notices
        /// </summary>
        [CorrigendumLabel("estimated_execution_timeframe", "")]
        public TimeFrame EstimatedExecutionTimeFrame { get; set; } = new TimeFrame() { Type = TimeFrameType.BeginAndEndDate };

        /// <summary>
        /// Scheduled date for start of award procedures
        /// </summary>
        [CorrigendumLabel("award_scheduled", "IV.2.5")]
        public DateTime? ScheduledStartDateOfAwardProcedures { get; set; }

        public ValidationState ValidationState { get; set; } 
    }

}
