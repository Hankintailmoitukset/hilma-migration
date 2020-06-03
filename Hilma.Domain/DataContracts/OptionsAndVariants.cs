using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Describes what options and variants offers may include.
    /// </summary>
    [Contract]
    public class OptionsAndVariants
    {
        /// <summary>
        ///     If other solution than the one described in the notice can be accepted.
        /// </summary>
        [CorrigendumLabel("variants_accepted", "II.2.10")]
        public bool VariantsWillBeAccepted { get; set; }

        /// <summary>
        /// Partial offers for national agriculture notice contracts
        /// </summary>
        [CorrigendumLabel("partial_offers_accepted", "II.2.10")]
        public bool PartialOffersWillBeAccepted { get; set; }

        /// <summary>
        ///     Will there be option for additional purchases.
        /// </summary>
        [CorrigendumLabel("options", "II.2.11")]
        public bool Options { get; set; }

        /// <summary>
        ///     Description of options in free text. Only valid if Options=true.
        /// </summary>
        [CorrigendumLabel("options_description", "II.2.11")]
        //[StringMaxLength(6000)]

        public string[] OptionsDescription { get; set; }

        /// <summary>
        /// Directive 2009/81/EC (Defence contracts)
        /// Determines if options are set in days or months.
        /// </summary>
        [CorrigendumLabel("options_timeframe", "II.2.2")]
        public TimeFrameType OptionType { get; set; }

        /// <summary>
        /// Directive 2009/81/EC (Defence contracts)
        /// Duration of time frame in days, if used has selected to insert the time frame in days.
        /// </summary>
        [CorrigendumLabel("indays", "II.2.2")]
        public int? OptionsDays { get; set; }

        /// <summary>
        /// Directive 2009/81/EC (Defence contracts)
        /// Duration of time frame in months, if used has selected to insert the time frame in months.
        /// </summary>
        [CorrigendumLabel("duration_months", "II.2.2")]
        public int? OptionsMonths { get; set; }

        /// <summary>
        /// Trims the component optional fields based on selections.
        /// </summary>
        public void Trim()
        {
            if (!Options)
            {
                OptionType = default;
                OptionsDescription = new string[0];
            }

            if (OptionType != TimeFrameType.Days)
            {
                OptionsDays = default;
            }

            if (OptionType != TimeFrameType.Months)
            {
                OptionsMonths = default;
            }
        }
    }
}
