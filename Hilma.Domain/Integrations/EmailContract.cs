namespace Hilma.Domain.Integrations
{
    /// <summary>
    /// Describes an email to be sent with O365l
    /// </summary>
    public class EmailContract
    {
        /// <summary>
        ///     Email message body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        ///     Email subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        ///     List of emails in to-field.
        /// </summary>
        public string[] To { get; set; }

        /// <summary>
        ///     List of emails in bcc-field (hidden copy).
        /// </summary>
        public string[] Bcc { get; set; }

        /// <summary>
        ///     List of emails in cc-field (public copy).
        /// </summary>
        public string[] Cc { get; set; }
    }
}
