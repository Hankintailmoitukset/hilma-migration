namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Describes localized strings used in a generic email.
    /// </summary>
    public class EmailTranslations
    {
        /// <summary>
        ///     Subject field in the approval email.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        ///     Text body of the approval mail.
        /// </summary>
        public string Body { get; set; }
    }
}
