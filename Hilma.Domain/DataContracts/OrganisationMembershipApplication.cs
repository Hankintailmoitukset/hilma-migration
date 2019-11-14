using System;
using System.Collections.Generic;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Describes a membership application sent to approval flow. Localization
    ///     is handled when creating this object.
    /// </summary>
    [Contract]
    public class OrganisationMembershipApplicationContract
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
        ///     ApplicationId to be passed back to the application from the approval flow.
        /// </summary>
        public Guid ApplicationId { get; set; }
        /// <summary>
        ///     Organisation to be passed back to the application from the approval flow.
        /// </summary>
        public Guid OrganisationId { get; set; }
        /// <summary>
        ///     List of handlers the approval mail is to be sent.
        /// </summary>
        public List<HandlerContract> Handlers { get; set; }
        /// <summary>
        ///     Options as a comma separated list. The options will be exactly the
        ///     text that appears in the email buttons. The selected option is
        ///     send exactly as it is back, and then mapped to approve/reject/block
        ///     in the application.
        ///
        ///     The option values are stored to each application so we can avoid the
        ///     translations changing braking application currently pending handling.
        /// </summary>
        public string ApproverOptions { get; set; }
    }
}
