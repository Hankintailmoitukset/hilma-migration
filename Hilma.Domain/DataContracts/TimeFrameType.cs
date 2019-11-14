using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Types of time frames user can input from the UI.
    /// </summary>
    [EnumContract]
    public enum TimeFrameType
    {
        /// <summary>
        ///     Default value, error state.
        /// </summary>
        Undefined = 0,
        /// <summary>
        ///     Time frame inputted in number of days.
        /// </summary>
        Days = 1,
        /// <summary>
        ///     Time frame inputted in number of months
        /// </summary>
        Months = 2,
        /// <summary>
        ///     Time frame inputted by picking start and end dates.
        /// </summary>
        BeginAndEndDate = 3,

        /// <summary>
        ///     Directive 2009/81/EC (Defence notices)
        ///     Time frame inputted in number of years
        /// </summary>
        Years = 4
    }
}
