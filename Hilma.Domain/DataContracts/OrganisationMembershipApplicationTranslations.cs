namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Describes localized strings used in an approval mail. The object
    ///     is generated from a factory in desired language.
    /// </summary>
    public class OrganisationMembershipApplicationTranslations
    {
        /// <summary>
        ///     Subject field in the approval email.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        ///     Text body of the approval mail.
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        ///     In-message header in the approval mail.
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        ///     Sub-header before the buttons in approval mail.
        /// </summary>
        public string SelectionHeader { get; set; }
        /// <summary>
        ///     Label for approve-button.
        /// </summary>
        public string Approve { get; set; }
        /// <summary>
        ///     Label for reject-button.
        /// </summary>
        public string Reject { get; set; }
        /// <summary>
        ///     Label for block-button.
        /// </summary>
        public string Block { get; set; }
    }
}
