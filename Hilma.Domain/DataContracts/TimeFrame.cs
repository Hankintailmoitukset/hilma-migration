using System;
using Hilma.Domain.Attributes;
using Hilma.Domain.Validators;
using Newtonsoft.Json;
namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Describes time frame selection from vuejs application.
    /// </summary>
    [Contract]
    public class TimeFrame
    {
        /// <summary>
        ///     Type of time frame user wishes to select.
        /// </summary>
        public TimeFrameType Type { get; set; }

        /// <summary>
        ///     Duration of time frame in days, if used has selected to insert the time frame in days.
        /// </summary>
        [CorrigendumLabel("indays", "II.2.7")]
        public int? Days { get; set; }

        /// <summary>
        /// Duration of time frame in months, if used has selected to insert the time frame in months.
        /// </summary>
        [CorrigendumLabel("duration_months", "II.2.7")]
        public int? Months { get; set; }

        /// <summary>
        /// Directive 2009/81/EC
        /// Duration of time frame in yaers, if used has selected to insert the time frame in years.
        /// </summary>
        [CorrigendumLabel("duration_years", "II.2.7")]
        public int? Years { get; set; }

        /// <summary>
        ///     Start date if user has opted for start and end date.
        /// </summary>
        [CorrigendumLabel("starting", "II.2.7")]
        public DateTime? BeginDate { get; set; }

        /// <summary>
        ///     End date if user has opted for start and end date.
        /// </summary>
        [CorrigendumLabel("end", "II.2.7")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        ///     If the notice can be renewed after the duration.
        /// </summary>
        [CorrigendumLabel("renewals_subject", "II.2.7")]
        public bool CanBeRenewed { get; set; }

        /// <summary>
        ///     Free text description for the renewal option, if it is selected.
        /// </summary>
        [CorrigendumLabel("renewals_descr", "II.2.7")]
        [StringMaxLength(1000)]
        public string[] RenewalDescription { get; set; }

        /// <summary>
        /// Directive 2009/81/EC (Defence prior information)
        /// Scheduled date for start of award procedures
        /// </summary>
        [CorrigendumLabel("award_start", "II.6")]
        public DateTime? ScheduledStartDateOfAwardProcedures { get; set; }

        [JsonIgnore]
        public bool IsOverFourYears{
            get {
                double years = 0;

                switch( Type ) {
                    case TimeFrameType.BeginAndEndDate:
                        years = (EndDate - BeginDate ).GetValueOrDefault().TotalDays / 365d;
                        break;
                    case TimeFrameType.Months:
                        years = Months.GetValueOrDefault() / 12d;
                        break;
                    case TimeFrameType.Years:
                        years = Years.GetValueOrDefault();
                        break;
                }
                return years > 4d;
            }
         }

        [JsonIgnore]
        public bool IsOverEightYears{
            get {
                double years = 0;

                switch( Type ) {
                    case TimeFrameType.BeginAndEndDate:
                        years = (EndDate - BeginDate ).Value.TotalDays / 365d;
                        break;
                    case TimeFrameType.Months:
                        years = Months.GetValueOrDefault() / 12d;
                        break;
                    case TimeFrameType.Years:
                        years = Years.GetValueOrDefault();
                        break;
                }
                return years > 8d;
            }
        }

        public void Trim()
        {
            if (Type != TimeFrameType.BeginAndEndDate)
            {
                BeginDate = default;
                EndDate = default;
            }

            if (Type != TimeFrameType.Days)
            {
                Days = default;
            }

            if (Type != TimeFrameType.Months)
            {
                Months = default;
            }

            if (Type != TimeFrameType.Years)
            {
                Years = default;
            }

            if (!CanBeRenewed)
            {
                RenewalDescription = new string[0];
            }
        }
    }
}
