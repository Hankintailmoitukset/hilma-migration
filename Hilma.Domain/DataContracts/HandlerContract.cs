using System;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Describes handler of a approval request.
    /// </summary>
    [Contract]
    public class HandlerContract
    {
        /// <summary>
        ///     UserId of the handler.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        ///     Email of the handler.
        /// </summary>
        public string Email { get; set; }
    }
}
