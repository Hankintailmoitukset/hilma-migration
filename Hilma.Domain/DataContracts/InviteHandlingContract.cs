using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;

namespace Hilma.Domain.DataContracts {
    /// <summary>
    /// Wraps user choice regarding organisation invitation.
    /// </summary>
    [Contract]
    public class InviteHandlingContract
    {
        /// <summary>
        /// The user choice
        /// </summary>
        public InviteReply Action { get; set; }
    }
}
