using System;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Contains approval flow response from office 365 approval mail
    /// </summary>
    [Contract]
    public class ApplicationHandlingContract
    {
        /// <summary>
        ///     Chosen option by the handler. Returns the exact text in the label.
        /// </summary>
        public string ApproverReply { get; set; }
        /// <summary>
        ///     Id of the user who handled the request.
        /// </summary>
        public Guid HandlerId { get; set; }
    }
}
